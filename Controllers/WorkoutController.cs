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
    public class WorkoutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Workout
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var workouts = await _context.Workouts
                .Include(w => w.User)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return View(workouts);
        }

        // GET: Workout/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts
                .Include(w => w.User)
                .Include(w => w.WorkoutExercises)
                    .ThenInclude(we => we.Exercise)
                .FirstOrDefaultAsync(m => m.WorkoutId == id);

            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // GET: Workout/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingUser = await _context.FitnessUsers
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);

            if (existingUser == null)
            {
                // Redirect to create user only if the user doesn't exist
                return RedirectToAction("Create", "User");
            }

            // If user exists, show the workout creation form
            return View();
        }

        // POST: Workout/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkoutId,Date,Duration,WorkoutType,Notes")] Workout workout)
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Find the custom user for this Identity user
            var user = await _context.FitnessUsers
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);

            if (user == null)
            {
                // No profile yet, redirect to create user profile
                return RedirectToAction("Create", "User");
            }

            workout.UserId = user.UserId;

            if (ModelState.IsValid)
            {
                _context.Workouts.Add(workout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }


        // GET: Workout/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts
                .Include(w => w.User)
                .Include(w => w.WorkoutExercises)
                    .ThenInclude(we => we.Exercise)
                .FirstOrDefaultAsync(m => m.WorkoutId == id);

            if (workout == null)
            {
                return NotFound();
            }

            // Make sure to populate any ViewData needed by the view
            ViewData["UserId"] = new SelectList(_context.FitnessUsers, "UserId", "FirstName", workout.UserId);
            ViewData["Exercises"] = new SelectList(_context.Exercises, "ExerciseId", "Name");

            return View(workout);
        }

        // POST: Workout/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkoutId,UserId,Date,Duration,WorkoutType,Notes")] Workout workout)
        {
            if (id != workout.WorkoutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutExists(workout.WorkoutId))
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
            ViewData["UserId"] = new SelectList(_context.FitnessUsers, "UserId", "FullName", workout.UserId);
            return View(workout);
        }

        // GET: Workout/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.WorkoutId == id);

            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // Updated code to handle potential null reference for 'workout' in DeleteConfirmed method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
            {
                return NotFound(); // Handle the case where the workout is not found
            }

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Workout/AddExercise
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExercise(int workoutId, int exerciseId, int sets, int reps, double weight)
        {
            var workoutExercise = new WorkoutExercise
            {
                WorkoutId = workoutId,
                ExerciseId = exerciseId,
                Sets = sets,
                Reps = reps,
                Weight = weight
            };

            _context.WorkoutExercises.Add(workoutExercise);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = workoutId });
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.WorkoutId == id);
        }
    }
}
