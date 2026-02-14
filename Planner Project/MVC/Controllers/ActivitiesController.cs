using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; 
using BusinessLayer.Models;
using DataLayer.Contexts;
using System.Security.Claims;

namespace MVC.Controllers
{
    [Authorize] 
    public class ActivitiesController : Controller
    {
        private readonly ActivityContext _context;
        public ActivitiesController(ActivityContext context) => _context = context;

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> CreateBirthday(string name, string person, DateOnly date, string desc, string color)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(person))
            {
                ModelState.AddModelError("", "Name and date of birth are required!");
                return View();
            }

            var bday = new BirthdayActivity(name, userId, person, date, desc, color);

            try
            {
                await _context.CreateAsync(bday);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Write error: " + ex.Message);
                return View();
            }
        }
    }
}


