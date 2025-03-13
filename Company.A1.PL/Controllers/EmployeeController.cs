using Company.A1.BLL.Interfaces;
using Company.A1.DAL.Models;
using Company.A1.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.A1.PL.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _employeeRepository;

        //ASK CLR  to Create Object From DepartmentRepository

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet] // Get: /Department/Index
        public IActionResult Index()
        {

            var employees = _employeeRepository.GetAll();


            return View(employees );
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {

            if (ModelState.IsValid) //Server Side Validation
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
                    
                };

                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var employee = _employeeRepository.Get(id.Value);
            if (employee == null) return NotFound(new { statusCode = 404, message = $"Department with Id :{id} is not Found" });
            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            //if (id is null) return BadRequest("Invalid Id"); //400
            //var department = _departmentRepository.Get(id.Value);
            //if (department == null) return NotFound(new { statusCode = 404, message = $"Department with Id :{id} is not Found" });
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest();    //400
                var count = _employeeRepository.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }


            return View(employee);
        }

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var department = new Department
        //        {
        //            Id = id,
        //            Name = model.Name,
        //            Code = model.Code,
        //            CreatedDate = model.CreatedDate,


        //        };
        //        var count = _departmentRepository.Update(department);
        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }

        //    }

        //    return View(model);
        //}


        [HttpGet]
        public IActionResult Delete(int? id)
        {

            //if (id is null) return BadRequest("Invalid Id"); //400
            //var department = _departmentRepository.Get(id.Value);
            //if (department == null) return NotFound(new { statusCode = 404, message = $"Department with Id :{id} is not Found" });
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest();    //400
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
