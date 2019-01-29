using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KvantCard.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KvantCard.Repos
{
    public class StudentRepo : BaseRepo<Student, Student>
    {
        public StudentRepo(Db db, ILoggerFactory loggerFactory, IMapper mapper) : base(db, loggerFactory, mapper, e => e.Students)
        {
        }

        protected override IQueryable<Student> AddInclude(DbSet<Student> dataSet)
        {
            return dataSet.Include(e => e.Parents);
        }
    }
}
