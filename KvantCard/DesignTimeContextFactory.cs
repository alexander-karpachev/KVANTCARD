using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KvantShared;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace KvantCard
{
    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<Db>
    {
        public Db CreateDbContext(string[] args)
        {
            var contentRoot = AppStarter.GetContentPath(AppStarter.ProjectName, args);
            var dbFileName = Path.GetFullPath(Path.Combine(contentRoot, AppStarter.DbFileName));
            var conStr = $"Data Source={dbFileName};";
            var starter = new AppStarter(args, AppStarter.DbDefaultProvider, conStr, AppStarter.ProjectName);
            var db = AppStarter.Provider.GetRequiredService<Db>();
            return db;
        }
    }
}
