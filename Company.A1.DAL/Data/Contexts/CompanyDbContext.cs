using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Company.A1.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.A1.DAL.Data.Contexts
{
    public class CompanyDbContext : DbContext
    {
        //CLR

        public CompanyDbContext (DbContextOptions<CompanyDbContext> options ): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  


            base.OnModelCreating(modelBuilder);
        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = . ; Database = CompanyA1 ; Trusted_Connection = True ; TrustServerCertificate = True ");
        //}

        public DbSet<Department> Departments { get; set; }   
    }
}
