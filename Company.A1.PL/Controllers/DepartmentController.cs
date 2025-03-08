using Microsoft.AspNetCore.Mvc;
using Company.A1.BLL.Repositories;
using Company.A1.BLL.Interfaces;
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
    }
}
