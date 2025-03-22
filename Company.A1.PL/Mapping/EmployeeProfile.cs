﻿using AutoMapper;
using Company.A1.DAL.Models;
using Company.A1.PL.Dtos;

namespace Company.A1.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>();

        }
    }
}
