using FitFriend.Data;
using FitFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace FitFriend.Controllers
{
    public class GoalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GoalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Goal
        public async Task<IActionResult> Index()
        {
            var goals = await _context.Goals.ToListAsync();
            return View(goals);
        }

        // GET: Goal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goals
                .FirstOrDefaultAsync(m => m.GoalId == id);
            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }

        // GET: Goal/Create
        public IActionResult Create()
        {
            ViewBag.GoalTypes = new List<SelectListItem>
            {
                 new SelectListItem { Value = "Weight Loss", Text = "Weight Loss" },
                 new SelectListItem { Value = "Muscle Gain", Text = "Muscle Gain" },
                 new SelectListItem { Value = "Endurance", Text = "Endurance" },
                 new SelectListItem { Value = "Steps", Text = "Steps" }
            };

            // Initialize a new goal with today's date
            var goal = new Goal
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1)
            };

            return View(goal);
        }

        // POST: Goal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Goal goal)
        {
            // Get the current logged-in user
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.FitnessUsers
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);

            if (user == null)
            {
                return RedirectToAction("Create", "User");
            }

            // Set the UserId
            goal.UserId = user.UserId;

            if (ModelState.IsValid)
            {
                _context.Add(goal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If we got this far, something failed, redisplay form
            ViewBag.GoalTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "Weight Loss", Text = "Weight Loss" },
                new SelectListItem { Value = "Muscle Gain", Text = "Muscle Gain" },
                new SelectListItem { Value = "Endurance", Text = "Endurance" },
                new SelectListItem { Value = "Steps", Text = "Steps" }
            };

            return View(goal);
        }


        // GET: Goal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            return View(goal);
        }

        // POST: Goal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,TargetDate")] Goal goal)
        {
            if (id != goal.GoalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoalExists(goal.GoalId))
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
            return View(goal);
        }

        // GET: Goal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goals
                .FirstOrDefaultAsync(m => m.GoalId == id);
            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }

        // POST: Goal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }

            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoalExists(int id)
        {
            return _context.Goals.Any(e => e.GoalId == id);
        }
    }
}
