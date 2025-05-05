using FitFriend.Data;
using FitFriend.Models;
using FitFriend.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FitFriend.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExerciseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exercise
        public async Task<IActionResult> Index()
        {
            // Query each concrete type separately
            var strengthExercises = await _context.Set<StrengthExercise>()
                .AsNoTracking()
                .ToListAsync();

            var cardioExercises = await _context.Set<CardioExercise>()
                .AsNoTracking()
                .ToListAsync();

            // Combine the results into one list of the base type
            var allExercises = strengthExercises.Cast<Exercise>()
                .Concat(cardioExercises.Cast<Exercise>())
                .ToList();

            System.Diagnostics.Debug.WriteLine($"Found {allExercises.Count} exercises");

            return View(allExercises);
        }


        // GET: Exercise/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(m => m.ExerciseId == id); // Updated from 'm.Id' to 'm.ExerciseId'
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercise/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercise/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExerciseViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Exercise exercise;
                    if (model.ExerciseType == "Cardio")
                    {
                        exercise = new CardioExercise
                        {
                            Name = model.Name,
                            Description = model.Description,
                            Distance = model.Distance ?? 0,
                            Duration = model.Duration ?? 0
                        };
                    }
                    else
                    {
                        exercise = new StrengthExercise
                        {
                            Name = model.Name,
                            Description = model.Description,
                            Sets = model.Sets ?? 0,
                            Reps = model.Reps ?? 0,
                            Weight = model.Weight ?? 0
                        };
                    }

                    _context.Exercises.Add(exercise);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Log the error
                System.Diagnostics.Debug.WriteLine($"Error creating exercise: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving.");
            }
            return View(model);
        }





        // GET: Exercise/Edit/5
        [HttpGet]
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        // POST: Exercise/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExerciseViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // First remove the existing exercise
                    var existingExercise = await _context.Exercises.FindAsync(id);
                    if (existingExercise == null)
                    {
                        return NotFound();
                    }
                    _context.Exercises.Remove(existingExercise);

                    // Then add a new concrete exercise of the correct type
                    Exercise exercise;
                    if (model.ExerciseType == "Cardio")
                    {
                        exercise = new CardioExercise
                        {
                            ExerciseId = id,
                            Name = model.Name,
                            Description = model.Description,
                            Distance = model.Distance ?? 0,
                            Duration = model.Duration ?? 0
                        };
                    }
                    else
                    {
                        exercise = new StrengthExercise
                        {
                            ExerciseId = id,
                            Name = model.Name,
                            Description = model.Description,
                            Sets = model.Sets ?? 0,
                            Reps = model.Reps ?? 0,
                            Weight = model.Weight ?? 0
                        };
                    }

                    _context.Exercises.Add(exercise);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the error
                    System.Diagnostics.Debug.WriteLine($"Error updating exercise: {ex.Message}");
                    if (!_context.Exercises.Any(e => e.ExerciseId == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "An error occurred while saving.");
                    }
                }
            }
            return View(model);
        }





        // GET: Exercise/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(m => m.ExerciseId == id); // Updated from 'm.Id' to 'm.ExerciseId'
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.ExerciseId == id); // Updated from 'e.Id' to 'e.ExerciseId'
        }
    }
}
