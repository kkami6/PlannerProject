using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.Models;
using DataLayer.Contexts;
using System.Security.Claims;

namespace MVC.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly AppointmentActivityContext _appointmentContext;

        public AppointmentsController(AppointmentActivityContext appointmentContext)
        {
            _appointmentContext = appointmentContext;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointments = await _appointmentContext.ReadAllAsync();

            var userAppointments = appointments
                .Where(a => a.UserId == userId)
                .OrderBy(a => a.Date)
                .ThenBy(a => a.StartTime);

            return View(userAppointments);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, DateOnly date, string description, string color, TimeOnly startTime, TimeOnly endTime)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                try
                {
                    var newAppointment = new AppointmentActivity(
                        name, userId, date, description, color,
                        Activity.RecurrenceType.None, startTime, endTime
                    );

                    newAppointment.ValidateTime();

                    await _appointmentContext.CreateAsync(newAppointment);

                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("TimeError", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An unexpected error occurred: " + ex.Message);
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _appointmentContext.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
