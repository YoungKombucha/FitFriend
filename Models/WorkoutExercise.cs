using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitFriend.Models
{
    public class WorkoutExercise
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Workout")]
        public int WorkoutId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }

        public double Weight { get; set; }

        // Navigation properties  
        public virtual Workout Workout { get; set; } = null!;
        public virtual Exercise Exercise { get; set; } = null!;
    }
}
