using Microsoft.AspNetCore.Mvc;
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

        public DepartmentController(IDepartmentRepository departmentRepository )
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
    }
}
