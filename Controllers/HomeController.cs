using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using FitFriend.Models;
using FitFriend.Data;
using FitFriend.Models.ViewModels;
using System.Security.Claims;

namespace FitFriend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get the current logged-in user's ID
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Find the corresponding fitness user
            var user = await _context.FitnessUsers
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);

            if (user == null)
            {
                // If no fitness profile exists for this identity user
                return View(new DashboardViewModel());
            }

            DailyLog? latestDailyLog = await _context.DailyLogs
                    .Where(d => d.UserId == user.UserId)
                    .OrderByDescending(d => d.Date)
                    .FirstOrDefaultAsync();

            var dashboard = new DashboardViewModel
            {
                RecentWorkouts = await _context.Workouts
                    .Where(w => w.UserId == user.UserId)
                    .OrderByDescending(w => w.Date)
                    .Take(5)
                    .ToListAsync(),

                ActiveGoals = await _context.Goals
                    .Where(g => g.UserId == user.UserId && g.EndDate >= DateTime.Now)
                    .ToListAsync(),

                LatestDailyLog = latestDailyLog ?? new DailyLog()
            };

            return View(dashboard);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
