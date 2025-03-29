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


        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        //ASK CLR  to Create Object From DepartmentRepository

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet] // Get: /Department/Index
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {

            if (ModelState.IsValid) //Server Side Validation
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreatedDate = model.CreatedDate
                };

                await _unitOfWork.DepartmentRepository.AddAsync(department);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id , string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var department =await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department == null) return NotFound(new { statusCode = 404, message = $"Department with Id :{id} is not Found" });
            return View( viewName , department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id is null) return BadRequest("Invalid Id");

            var department =await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
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
        public async Task<IActionResult> Edit([FromRoute] int id, CreateDepartmentDto model)
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
                _unitOfWork.DepartmentRepository.Update(department);
                var count =await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var department =await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department == null)
                return NotFound(new { statusCode = 404, message = $"Department with Id: {id} is not Found" });

            return View("Delete", department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department =await _unitOfWork.DepartmentRepository.GetAsync(id);
            if (department == null)
                return NotFound(new { statusCode = 404, message = $"Department with Id: {id} is not Found" });

             _unitOfWork.DepartmentRepository.Delete(department);
             var count =await _unitOfWork.CompleteAsync();

            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View("Delete", department);
        }
    }
}
