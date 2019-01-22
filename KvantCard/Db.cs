using KvantCard.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard
{
    public class Db : DbContext
    {

        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            mb.Conventions.Remove<PluralizingTableNameConvention>();

            mb.Entity<Student>()
                .HasKey(e => e.ID)
                .HasMany<Parent>(e => e.Parents)
                ;
        }
    }
}
