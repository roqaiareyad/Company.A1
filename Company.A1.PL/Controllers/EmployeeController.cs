using Company.A1.BLL.Interfaces;
using Company.A1.BLL.Repositories;
using Company.A1.DAL.Models;
using Company.A1.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.A1.PL.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        //ASK CLR  to Create Object From DepartmentRepository

        public EmployeeController(IEmployeeRepository employeeRepository , IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;   
        }

        [HttpGet] // Get: /Department/Index
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _employeeRepository.GetAll();
            }
            else
            {
                employees = _employeeRepository.GetByName(SearchInput);
            }

            // Dictionary :
            // 1. ViewData : transfer extra information from controller to view
            // ViewData["Message"] = " Hello From View Data ";

            // 2. ViewBag  : transfer extra information from controller to view
            //ViewBag.Message = " Hello From View Bag";


            // 3. TempData

            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
             var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            if (ViewData["departments"] == null)
            {
                Console.WriteLine("ViewData['departments'] is NULL!");
            }
            else
            {
                Console.WriteLine("ViewData['departments'] has data!");
            }
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {

            if (ModelState.IsValid) //Server Side Validation
            {
                try
                {
                    var employee = new Employee()
                    {
                        Name = model.Name,
                        Address = model.Address,
                        Age = model.Age,
                        CreateAt = model.CreateAt,
                        HiringDate = model.HiringDate,
                        Email = model.Email,
                        IsActive = model.IsActive,
                        IsADeleted = model.IsADeleted,
                        Phone = model.Phone,
                        Salary = model.Salary,
                        DepartmentId = model.DepartmentId,  

                    };

                    var count = _employeeRepository.Add(employee);
                    if (count > 0)
                    {
                        TempData["Message"] = "Employee is Created";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("",ex.Message);
                }
            

            }
            return View(model);
        }



        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var employee = _employeeRepository.Get(id.Value);
            if (employee == null) return NotFound(new { statusCode = 404, message = $"employee with Id :{id} is not Found" });
            return View(viewName, employee);
        }



        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            if (id is null) return BadRequest("Invalid Id"); // 400
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { statusCode = 404, message = $"Department with id : {id} is not found" });
            var employeeDto = new CreateEmployeeDto()
            {

                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                CreateAt = employee.CreateAt,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                IsActive = employee.IsActive,
                IsADeleted = employee.IsADeleted,
                Phone = employee.Phone,
                Salary = employee.Salary
            };
            return View(employeeDto);

        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model)
        {

            if (ModelState.IsValid)
            {
                // if (id != employee.Id) return BadRequest(); // 400
                var employee = new Employee()
                {
                    Id = id,
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    IsADeleted = model.IsADeleted,
                    Phone = model.Phone,
                    Salary = model.Salary
                };
                var count = _employeeRepository.Update(employee);
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

            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {

            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest(); // 400
                var count = _employeeRepository.Delete(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(employee);
        }



    }
}
