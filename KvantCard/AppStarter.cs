using KvantCard.View;
using KvantShared;
using KvantShared.Repos;
using KvantShared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;

namespace KvantCard
{
    public class AppStarter : IAppStarter, IDisposable
    {
        private bool _migrate;
        private static readonly object Lock = new object();
        private static string _projectName;
        public static string _contentRoot;

        private string[] _args;

        private string _dbProvider;
        private string _dbConnectionStr;
        public const string ProjectName = @"\KvantCard";
        public const string DbFileName = "db.sqlite";
        public const string DbDefaultProvider = "sqlite";
        public const string LogsFolder = "Logs";

        private IConfiguration _configuration;
        private IServiceCollection _services;
        private ServiceProvider _provider;

        public IConfiguration Configuration => _configuration;
        public ServiceProvider Provider => _provider;
        public string ContentRoot => _contentRoot;

        public AppStarter(string[] args, string projectName = ProjectName) : this(args, null, null, projectName)
        {
        }

        public AppStarter(string[] args, string dbProvider, string dbConnectionStr, string projectName,
            bool migrate = true)
        {
            lock (Lock)
            {
                _args = args;
                _migrate = migrate;
                _projectName = projectName;
                _dbConnectionStr = dbConnectionStr;
                _dbProvider = dbProvider;

                Init();
            }
        }

        private void Init()
        {
            GetContentPath(_projectName);

            var config = new XmlLoggingConfiguration("NLog.config");
            var logPath = Path.GetFullPath(Path.Combine(ContentRoot, LogsFolder));
            config.Variables["logDirectory"] = logPath;
            LogManager.Configuration = config;

            _services = new ServiceCollection();
            _services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                builder.AddNLog(new NLogProviderOptions
                {
                    CaptureMessageTemplates = true,
                    CaptureMessageProperties = true
                });
            });

            _configuration = LoadConfig();
            if (_dbProvider == null)
                _dbProvider = _configuration["Provider"]?.ToLower();
            if (_dbProvider == null)
                _dbProvider = DbDefaultProvider;

            ConfigureDb(_services, _dbProvider, _dbConnectionStr);

            RegisterServices(_services);

            _provider = _services.BuildServiceProvider();

            //var logger = NLog.LogManager.GetCurrentClassLogger();
            var logger = _provider.GetRequiredService<ILogger<App>>();
            logger.LogDebug("Program started...");

            PrepareStart(_provider);
        }


        public IConfiguration LoadConfig()
        {
            var builder = new ConfigurationBuilder()
                // .SetBasePath(ContentRoot)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.private.json", true, true)
                .AddEnvironmentVariables()
                .AddCommandLine((_args ?? new string[0]).Where(e => string.Compare("--console", e.ToLower(), StringComparison.Ordinal) != 0).ToArray());

            return builder.Build();
        }

        public static bool IsConsole(string[] args = null)
        {
            bool isConsole;
            if (Debugger.IsAttached
                || (args != null && args.Contains("--console"))
                || string.Equals(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName).ToUpperInvariant(), "EF.EXE", StringComparison.Ordinal)
                || string.Equals(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName).ToUpperInvariant(), "RESHARPERTESTRUNNER32.EXE", StringComparison.Ordinal)
                || string.Equals(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName).ToUpperInvariant(), "RESHARPERTESTRUNNER64.EXE", StringComparison.Ordinal)
                || string.Equals(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName).ToUpperInvariant(), "TESTHOST.X86.EXE", StringComparison.Ordinal)
                || string.Equals(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName).ToUpperInvariant(), "TESTHOST.EXE", StringComparison.Ordinal)
                )
                isConsole = true;
            else
                isConsole = false;
            return isConsole;
        }

        public static string GetContentPath(string projectName, string[] args = null)
        {
            lock (Lock)
            {
                if (_contentRoot != null)
                    return _contentRoot;
                //set ContenRoot directory
                var root = IsConsole(args)
                    ? Directory.GetCurrentDirectory()
                    : Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

                if (root.LastIndexOf(projectName, StringComparison.Ordinal) >= 0)
                {
                    root = root.Substring(0,
                        root.LastIndexOf(projectName, StringComparison.Ordinal) + projectName.Length);
                    root = Path.GetFullPath(Path.Combine(root, "..")); // TODO А нужно ли? Мы "перепрыгиваем" в папку с проектом. Хорошо в отладке, но в работе?..
                }

                _contentRoot = root;
            }
            //contentPath = Directory.GetCurrentDirectory();
            //Console.WriteLine(contentPath);
            //Console.WriteLine(Process.GetCurrentProcess().MainModule.FileName);
            //Console.WriteLine(Directory.GetCurrentDirectory());
            //Console.WriteLine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));
            return _contentRoot;
        }

        public void ConfigureWithDbBuilder(DbContextOptionsBuilder options, string provider, string connectionStr)
        {
            options.EnableSensitiveDataLogging();
            if (provider == "sqlite")
            {
                if (connectionStr == null)
                {
                    var dbName = _configuration["ConnectionStrings:SQLite"] ?? DbFileName;
                    var dbFileName = Path.GetFullPath(Path.Combine(ContentRoot, dbName));
                    connectionStr = $"Data Source={dbFileName};";
                }

                options.UseSqlite(connectionStr, b => { b.MigrationsAssembly("KvantCard"); });
            }
            else if (provider == "mysql")
            {
                options.UseMySql(connectionStr ?? _configuration["ConnectionStrings:MySQL"]);
            }
            else if (provider == "mssql")
            {
                options.UseSqlServer(connectionStr ?? _configuration["ConnectionStrings:MSSQL"]);
            }
            else if (provider == "postgresql")
            {
                options.UseNpgsql(connectionStr ?? _configuration["ConnectionStrings:PostgreSQL"]);
                //}
                //else if (sqlType == "oracle")
                //{L
                //        options.UseOracle(_configuration["Oracle"]));
            }
            else
            {
                throw new ArgumentException("Not a valid database type");
            };
        }


        private void ConfigureDb(IServiceCollection services, string provider, string connectionStr)
        {
            // Check Provider and get ConnectionString
            services.AddDbContext<Db>(options => { ConfigureWithDbBuilder(options, provider, connectionStr); }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);
        }

        protected virtual void PrepareStart(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            using (var db = scope.ServiceProvider.GetRequiredService<Db>())
            {
                db.Database.OpenConnection();
                if (_migrate)
                    db.Database.Migrate();
            }
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IAppStarter>(this);

            // Configure AutoMapper
            services.AddAutoMapper();

            // Repos
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

            // Services
            this.RegisterSharedServices(services);

            // Windows
            services.AddSingleton<MainWindow>();
        }

        public void Dispose()
        {
            _provider?.Dispose();
        }

        string IAppStarter.ContentRoot => ContentRoot;
    }
}
