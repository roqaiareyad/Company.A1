﻿using Microsoft.AspNetCore.Mvc;
using Company.A1.BLL.Repositories;
using Company.A1.BLL.Interfaces;
using Company.A1.PL.Dtos;
using Microsoft.Net.Http.Headers;
using Company.A1.DAL.Models;
namespace Company.A1.PL.Controllers
{
    //MVC Controller
    public class DepartmentController : Controller
    {


        private readonly IDepartmentRepository _departmentRepository;

        //ASK CLR  to Create Object From DepartmentRepository

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet] // Get: /Department/Index
        public IActionResult Index()
        {

            var departments = _departmentRepository.GetAll();


            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {

            if (ModelState.IsValid) //Server Side Validation
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreatedDate = model.CreatedDate
                };

                var count = _departmentRepository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(int? id , string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var department = _departmentRepository.Get(id.Value);
            if (department == null) return NotFound(new { statusCode = 404, message = $"Department with Id :{id} is not Found" });
            return View( viewName , department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id is null) return BadRequest("Invalid Id");

            var department = _departmentRepository.Get(id.Value);
            if (department == null)
            {
                return NotFound(new { statusCode = 404, message = $"Department with Id :{id} is not Found" });
            }

         
            var departmentDto = new CreateDepartmentDto
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreatedDate = department.CreatedDate
            };

            return View(departmentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest(); // 400

                var department = new Department
                {
                    Id = model.Id,
                    Code = model.Code,
                    Name = model.Name,
                    CreatedDate = model.CreatedDate
                };

                var count = _departmentRepository.Update(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var department = _departmentRepository.Get(id.Value);
            if (department == null)
                return NotFound(new { statusCode = 404, message = $"Department with Id: {id} is not Found" });

            return View("Delete", department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _departmentRepository.Get(id);
            if (department == null)
                return NotFound(new { statusCode = 404, message = $"Department with Id: {id} is not Found" });

            var count = _departmentRepository.Delete(department);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View("Delete", department);
        }
    }
}
