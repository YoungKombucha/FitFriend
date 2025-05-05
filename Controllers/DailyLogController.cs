using FitFriend.Data;
using FitFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitFriend.Controllers
{
    public class DailyLogController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DailyLogController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: DailyLog
        public async Task<IActionResult> Index()
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.FitnessUsers
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);

            if (user == null)
            {
                return RedirectToAction("Create", "User");
            }

            var logs = await _context.DailyLogs
                .Where(dl => dl.UserId == user.UserId)
                .OrderByDescending(dl => dl.Date)
                .ToListAsync();

            return View(logs); // Pass a collection to the Index view
        }

        // GET: DailyLog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyLog = await _context.DailyLogs
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (dailyLog == null)
            {
                return NotFound();
            }

            return View(dailyLog);
        }

        // GET: DailyLog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyLog = await _context.DailyLogs.FindAsync(id);
            if (dailyLog == null)
            {
                return NotFound();
            }
            return View(dailyLog);
        }

        // POST: DailyLog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DailyLog dailyLog)
        {
            if (id != dailyLog.LogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyLogExists(dailyLog.LogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dailyLog);
        }

        // GET: DailyLog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyLog = await _context.DailyLogs
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (dailyLog == null)
            {
                return NotFound();
            }

            return View(dailyLog);
        }

        // POST: DailyLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailyLog = await _context.DailyLogs.FindAsync(id);
            _context.DailyLogs.Remove(dailyLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyLogExists(int id)
        {
            return _context.DailyLogs.Any(e => e.LogId == id);
        }



        // GET: DailyLog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DailyLog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DailyLog dailyLog)
        {
            // Set UserId here if needed
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.FitnessUsers
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);
            if (user == null)
                return RedirectToAction("Create", "User");

            dailyLog.UserId = user.UserId;

            if (ModelState.IsValid)
            {
                _context.Add(dailyLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dailyLog);
        }


    }
}
