using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitFriend.Models
{
    public class Workout
    {
        [Key]
        public int WorkoutId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }

        [StringLength(50)]
        public string WorkoutType { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        // Navigation properties
        public virtual Users User { get; set; }
        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }

        // Methods
        public void AddExercise() { /* Implementation */ }
        public double GetTotalCaloriesBurned() { /* Implementation */ return 0; }
        public void EditWorkoutDetails() { /* Implementation */ }
    }
}
