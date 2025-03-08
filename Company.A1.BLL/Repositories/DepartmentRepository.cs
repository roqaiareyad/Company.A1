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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _db;  //NULL 
        
        public DepartmentRepository ()
        {
            _db = new CompanyDbContext();
        }

        public IEnumerable<Department> GetAll()
        {
            return _db.Departments.ToList();

        }

        public Department? Get(int id)
        {
         
            return _db.Departments.Find(id);
        }

        public int Add(Department model)
        {
           
            _db.Departments.Add(model);
            return _db.SaveChanges();
        }

        public int Update(Department model)
        {
          
            _db.Departments.Update(model);
            return _db.SaveChanges();
        }

        public int Delete(Department model)
        {
            _db.Departments.Remove(model);
            return _db.SaveChanges();
        }

    

    }
}
