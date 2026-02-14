using Microsoft.AspNetCore.Mvc;
using DataLayer.Contexts;
using System.Security.Claims;
using System.Diagnostics;
using MVC.Models;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TaskActivityContext _taskContext;

        public HomeController(ILogger<HomeController> logger, TaskActivityContext taskContext)
        {
            _logger = logger;
            _taskContext = taskContext;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var allTasks = await _taskContext.ReadAllAsync();

                ViewBag.PendingTasksCount = allTasks.Count(t => t.UserId == userId && !t.IsCompleted);
            }
            else
            {
                ViewBag.PendingTasksCount = 0;
            }
            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
