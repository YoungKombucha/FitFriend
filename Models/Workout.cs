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
        public string WorkoutType { get; set; } = string.Empty;

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        // Navigation properties  
        public virtual Users? User { get; set; }

        // Initialize the collection to avoid null reference issues  
        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();

        // Methods  
        public void AddExercise() { /* Implementation */ }
        public double GetTotalCaloriesBurned() { /* Implementation */ return 0; }
        public void EditWorkoutDetails() { /* Implementation */ }
    }
}
