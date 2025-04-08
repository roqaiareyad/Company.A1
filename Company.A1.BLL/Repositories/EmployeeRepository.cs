using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.A1.BLL.Interfaces;
using Company.A1.DAL.Data.Contexts;
using Company.A1.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.A1.BLL.Repositories
{

    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Employee>> GetByNameAsync(string input)
        {
            return await _context.Employees.Where(E => E.Name.ToLower().Contains(input.ToLower())).ToListAsync();
        }



    }
}
