using Microsoft.EntityFrameworkCore;
using NetCoreStudy.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStudy.Data
{
    public class StudyDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public StudyDbContext(DbContextOptions<StudyDbContext> options)
            :base(options)
        {

        }
    }
}
