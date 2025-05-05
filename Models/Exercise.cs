using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitFriend.Models
{
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [StringLength(50)]
        public string ExerciseType { get; set; } = string.Empty;

        // Navigation properties  
        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();

        // Methods  
        public string GetExerciseDetails() { /* Implementation */ return ""; }
    }
}
