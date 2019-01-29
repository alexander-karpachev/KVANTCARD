using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using KvantCard.Model;
using KvantCard.Repos;
using KvantCard.Utils;
using KvantCard.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace KvantCard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string ProjectName = @"\KvantCard";
        private const string DbFileName = "db.sqlite";
        private const string LogsFolder = "Logs";

        public App()
        {

        }

        public static bool IsConsole(string[] args)
        {
            bool isConsole;
            if (Debugger.IsAttached || (args != null && args.Contains("--console")) ||
                string.Equals(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName).ToUpperInvariant(), "EF.EXE", StringComparison.Ordinal))
                isConsole = true;
            else
                isConsole = false;
            return isConsole;
        }

        public static string ContentPath { get; private set; }

        public static string GetContentPath(string[] args)
        {
            if (ContentPath != null)
                return ContentPath;

            string contentPath;

            //set ContenRoot directory
            if (IsConsole(args))
                contentPath = Directory.GetCurrentDirectory();
            else
                contentPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            if (contentPath.LastIndexOf(ProjectName, StringComparison.Ordinal) >= 0)
            {
                contentPath = contentPath.Substring(0,
                    contentPath.LastIndexOf(ProjectName, StringComparison.Ordinal) + ProjectName.Length);
                contentPath = Path.GetFullPath(Path.Combine(contentPath, ".."));
            }
            //contentPath = Directory.GetCurrentDirectory();
            //Console.WriteLine(contentPath);
            //Console.WriteLine(Process.GetCurrentProcess().MainModule.FileName);
            //Console.WriteLine(Directory.GetCurrentDirectory());
            //Console.WriteLine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));
            ContentPath = contentPath;
            return contentPath;
        }

        private IServiceCollection _services;
        private ServiceProvider _provider;

        protected override void OnStartup(StartupEventArgs e)
        {
            var isConsole = IsConsole(e.Args);
            var contentRoot = GetContentPath(e.Args);

            //var config = new NLog.Config.LoggingConfiguration();
            var config = new XmlLoggingConfiguration("NLog.config");
            var logPath = Path.GetFullPath(Path.Combine(contentRoot, LogsFolder));
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

            ConfigureDb(_services, contentRoot);

            RegisterServices(_services);

            _provider = _services.BuildServiceProvider();

            //var logger = NLog.LogManager.GetCurrentClassLogger();
            var logger = _provider.GetRequiredService<ILogger<App>>();
            logger.LogDebug("Program started...");

            PrepareStart(_provider);

            var workWindow = _provider.GetService<MainWindow>();
            workWindow.Show();
            base.OnStartup(e);
        }

        private void ConfigureDb(IServiceCollection services, string contentRoot)
        {
            var sqlType = ConfigurationManager.AppSettings["Provider"]?.ToLower();

            // Check Provider and get ConnectionString
            services.AddDbContext<Db>(options =>
            {
                options.EnableSensitiveDataLogging();
                if (sqlType == "sqlite")
                {
                    var dbName = ConfigurationManager.AppSettings["SQLite"] ?? DbFileName;
                    var dbFileName = Path.GetFullPath(Path.Combine(contentRoot, dbName));
                    var conStr = $"Data Source={dbFileName};";

                    options.UseSqlite(conStr);
                }
                else if (sqlType == "mysql")
                {
                    options.UseMySql(ConfigurationManager.AppSettings["MySQL"]);
                }
                else if (sqlType == "mssql")
                {
                    options.UseSqlServer(ConfigurationManager.AppSettings["MSSQL"]);
                }
                else if (sqlType == "postgresql")
                {
                    options.UseNpgsql(ConfigurationManager.AppSettings["PostgreSQL"]);
                //}
                //else if (sqlType == "oracle")
                //{
                //        options.UseOracle(ConfigurationManager.AppSettings["Oracle"]));
                }
                else
                {
                    throw new ArgumentException("Not a valid database type");
                }
            }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);
        }

        private void RegisterServices(IServiceCollection services)
        {
            // Configure AutoMapper
            _services.AddAutoMapper();

            // Repos
            _services.AddSingleton<StudentRepo>();

            // Windows
            _services.AddSingleton<MainWindow>();
        }

        private void PrepareStart(IServiceProvider provider)
        {
            using (var scope = _provider.CreateScope())
            using (var db = scope.ServiceProvider.GetRequiredService<Db>())
            {
                db.Database.OpenConnection();
                db.Database.Migrate();
            }

            using (var scope = _provider.CreateScope())
            using (var db = scope.ServiceProvider.GetRequiredService<Db>())
            {
                //db.Database.Log = msg => logger.Log(LogLevel.Debug, msg);
                //if (!db.Database.Exists())
                //    logger.Debug("Db doesn't exist");
                //db.Database.Migrate();
                var students = db.Students.ToList();
                students = db.Students.Include(p => p.Parents).ToList();
            }

        }

    }
}
