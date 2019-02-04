using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace KvantCard.Model
{
    public class Db : DbContext
    {
        private ILogger<Db> _logger;

        public Db()
        {

        }

        public Db(DbContextOptions<Db> options, ILoggerFactory loggerFactory) : base(options)
        {
            _logger = loggerFactory.CreateLogger<Db>();
            //_logger.LogDebug($"New database context created {GetHashCode()}");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            if (_logger == null)
            {
                var dbFileName = AppStarter.ContentRoot;
                dbFileName = Path.GetFullPath(Path.Combine(dbFileName, "db.sqlite"));
                optionsBuilder.UseSqlite("Data Source=" + dbFileName);
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
