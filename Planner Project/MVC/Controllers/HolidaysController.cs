using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; 
using BusinessLayer.Models;
using DataLayer.Contexts;

namespace MVC.Controllers
{
    public class HolidaysController : Controller
    {
        private readonly HolidayContext _context;
        public HolidaysController(HolidayContext context) => _context = context;

        public async Task<IActionResult> Index() => View(await _context.ReadAllAsync());

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, DateOnly date, string desc, string color)
        {
            if (await _context.HolidayExistsAsync(name, date))
            {
                ModelState.AddModelError("", "A holiday with this name on this date already exists!");
                return View(); 
            }

            try
            {
                await _context.CreateAsync(new Holiday(name, date, desc, color));
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Write error: " + ex.Message);
                return View();
            }
        }
    }
}

