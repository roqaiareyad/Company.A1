﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.A1.DAL.Models;

namespace Company.A1.BLL.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee? Get(int id);    
        int Add(Employee model);
        int Update(Employee model);
        int Delete(Employee model);

    }
}
