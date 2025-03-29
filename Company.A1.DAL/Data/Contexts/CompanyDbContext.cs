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


        public DbSet<Department> Departments { get; set; }   
        public DbSet<Employee> Employees { get; set; }
    }
}
