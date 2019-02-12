﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;

namespace KvantCard
{
    public class AppStarter : IAppStarter, IDisposable
    {
        private static AppStarter _instance;
        private static readonly object Lock = new object();
        private static string _projectName;
        public static string _contentRoot;

        private string[] _args;

        private string _dbProvider;
        private string _dbConnectionStr;
        private const string ProjectName = @"\KvantCard";
        private const string DbFileName = "db.sqlite";
        public const string LogsFolder = "Logs";

        private IConfiguration _configuration;
        private IServiceCollection _services;
        private ServiceProvider _provider;

        public static IConfiguration Configuration => _instance._configuration;
        public static ServiceProvider Provider => _instance._provider;
        public static string ContentRoot => _contentRoot;

        public AppStarter(string[] args, string projectName = ProjectName) : this(args, null, null, projectName)
        {
        }

        public AppStarter(string[] args, string dbProvider, string dbConnectionStr, string projectName)
        {
            lock (Lock)
            {
                if (_instance != null) return;
                _instance = this;
                _instance._args = args;
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
//                .SetBasePath(ContentRoot)
                .AddJsonFile("appsettings.json",
                    optional: true,
                    reloadOnChange: true);

            return builder.Build();
        }

        public static bool IsConsole()
        {
            bool isConsole;
            if (Debugger.IsAttached 
                || (_instance?._args != null && _instance._args.Contains("--console"))
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

        public static string GetContentPath(string projectName)
        {
            lock (Lock)
            {
                if (_contentRoot != null)
                    return _contentRoot;
                //set ContenRoot directory
                var root = IsConsole() 
                    ? Directory.GetCurrentDirectory() 
                    : Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

                if (root.LastIndexOf(projectName, StringComparison.Ordinal) >= 0)
                {
                    root = root.Substring(0,
                        root.LastIndexOf(projectName, StringComparison.Ordinal) + projectName.Length);
                    root = Path.GetFullPath(Path.Combine(root, ".."));
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

        public void ConfigureDb(DbContextOptionsBuilder options, string provider, string connectionStr)
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

                options.UseSqlite(connectionStr);
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
                //{
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
            services.AddDbContext<Db>(options => { ConfigureDb(options, provider, connectionStr); }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);
        }

        

        private void PrepareStart(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            using (var db = scope.ServiceProvider.GetRequiredService<Db>())
            {
                db.Database.OpenConnection();
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
