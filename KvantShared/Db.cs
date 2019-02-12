using System;
using System.IO;
using KvantShared.Model;
using KvantShared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace KvantShared
{
    public class Db : DbContext
    {
        private readonly IAppStarter _appStarter;
        private ILogger<Db> _logger;

        //public Db()
        //{

        //}

        public Db(DbContextOptions<Db> options, IAppStarter appStarter, ILoggerFactory loggerFactory) : base(options)
        {
            _appStarter = appStarter;
            _logger = loggerFactory.CreateLogger<Db>();
            //_logger.LogDebug($"New database context created {GetHashCode()}");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            if (_logger == null)
            {
                throw new Exception("Please use DI to instantiate DbContext");
                //var dbFileName = _appStarter.ContentRoot;
                //dbFileName = Path.GetFullPath(Path.Combine(dbFileName, "db.sqlite"));
                //_appStarter.ConfigureWithDbBuilder(optionsBuilder, "sqlite", "Data Source=" + dbFileName);
            }

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<DictionaryItem> DictionaryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>(etb =>
            {
                etb.HasKey(e => e.Id);
                etb.HasMany<Parent>(e => e.Parents);
            });
        }

        public IDbContextTransaction BeginTransaction()
        {
            var tr = Database.BeginTransaction();
            //_logger.LogDebug($"New transaction started {GetHashCode()}");
            return tr;
        }

        public void Commit()
        {
            Database.CommitTransaction();
            //_logger.LogDebug($"Transaction commited {GetHashCode()}");
        }

        public void Rollback()
        {
            Database.RollbackTransaction();
            //_logger.LogDebug($"Transaction rolled back {GetHashCode()}");
        }

    }
}
