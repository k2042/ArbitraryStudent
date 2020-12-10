using ArbitraryStudent.Service.Db.DataObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArbitraryStudent.Service.Db
{
    public class ArbitraryDbContext : DbContext
    {
        public DbSet<StudentDo> Students { get; set; }
        public DbSet<GradeDo> GradeDictionary { get; set; }

        public ArbitraryDbContext(DbContextOptions<ArbitraryDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    Database.Migrate();
        //}
    }
}
