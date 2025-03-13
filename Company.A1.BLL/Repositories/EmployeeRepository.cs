using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.A1.BLL.Interfaces;
using Company.A1.DAL.Data.Contexts;
using Company.A1.DAL.Models;

namespace Company.A1.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
     {
        public EmployeeRepository(CompanyDbContext context) :base(context)     //ASK CLR TO Create Object From CompanyDbContext
        { 

        }

    }
}
