using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.Models;
using DataLayer.Contexts;
using System.Security.Claims;

namespace MVC.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly TaskActivityContext _taskContext;

        public TasksController(TaskActivityContext taskContext)
        {
            _taskContext = taskContext;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allTasks = await _taskContext.ReadAllAsync();

            var userTasks = allTasks.Where(t => t.UserId == userId).OrderBy(t => t.DueDate);

            return View(userTasks);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, DateOnly date, string description, string color, Activity.RecurrenceType recurrence, DateOnly dueDate)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var newTask = new TaskActivity(name, userId, date, description, color, recurrence, dueDate);

                await _taskContext.CreateAsync(newTask);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Complete(int id)
        {
            var task = await _taskContext.ReadAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (task != null && task.UserId == userId)
            {
                task.MarkAsCompleted();
                await _taskContext.UpdateAsync(task);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskContext.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
