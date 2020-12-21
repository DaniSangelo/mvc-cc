using Model.Registrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IES.Data
{
    public class IESContext : DbContext
    {
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public IESContext(DbContextOptions<IESContext> options) : base(options) { }


        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server(localdb)\\mssqllocaldb;Database=IESMVCCC; Trusted_Connection=True;MultipleActiveResultSet=true");
        }*/
    }
}
