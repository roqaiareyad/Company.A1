﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.A1.DAL.Models;

namespace Company.A1.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        List<Employee> GetByName(string name);


        //IEnumerable<Emplyee> GetAll();

        //Emplyee? Get(int id);

        //int Add(Emplyee model);
        //int Update(Emplyee model);
        //int Delete(Emplyee model);

    }
}
