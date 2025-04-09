using Company.A1.DAL.Models;
using Company.A1.PL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Company.A1.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController(UserManager<AppUser> userManager) : Controller
    {
        private readonly UserManager<AppUser> _userManager = userManager;

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            var usersList = string.IsNullOrEmpty(SearchInput)
                ? await _userManager.Users.ToListAsync()
                : await _userManager.Users
                      .Where(u => u.FirstName.ToLower().Contains(SearchInput.ToLower()))
                      .ToListAsync();

            var users = new List<UserToReturn>();

            foreach (var user in usersList)
            {
                var roles = await _userManager.GetRolesAsync(user);
                users.Add(new UserToReturn
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = roles
                });
            }

            return View(users);
        
        }


        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Invalid Id");

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound(new { StatusCode = 404, message = $"User with id: {id} is not found" });

            var roles = await _userManager.GetRolesAsync(user); // ✅ await هنا

            var dto = new UserToReturn
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles
            };

            return View(viewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, UserToReturn model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operations");
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) return BadRequest("Invalid Operations");
                user.UserName = model.UserName!;
                user.Email = model.Email!;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded) return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return BadRequest("Invalid Operations");
            var result = await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}