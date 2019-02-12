using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KvantCard;
using KvantShared.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;

namespace KvantTest
{
    public class TestAppFixture : IDisposable
    {
        private AppStarter _starter;
        public const string ProjectName = @"\KvantTest";
        public const string DbTestName = @"db-kvant-test-{0}.sqlite";


        public IServiceCollection Services { get; }

        public ServiceProvider Provider => AppStarter.Provider;

        public TestAppFixture()
        {
            var rnd = new Random();
            var dbName = string.Format(DbTestName, rnd.Next());
            var contentRoot = AppStarter.GetContentPath(ProjectName);
            var dbFileName = Path.GetFullPath(Path.Combine(contentRoot, dbName));
            var conStr = $"Data Source={dbFileName};";
            _starter = new AppStarter(null, "sqlite", conStr, ProjectName);

            var logger = Provider.GetRequiredService<ILogger<TestAppFixture>>();
            logger.LogDebug("Test started...");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TestAppFixture()
        {
            // Finalizer calls Dispose(false)  
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _starter?.Dispose();
            }
        }

    }
}
