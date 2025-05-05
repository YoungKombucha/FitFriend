using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitFriend.Models
{
    public enum GoalType
    {
        [Display(Name = "Weight Loss")]
        WeightLoss,
        [Display(Name = "Muscle Gain")]
        MuscleGain,
        [Display(Name = "Step Count")]
        StepCount
    }
    public class Goal
    {

        [Key]
        public int GoalId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [StringLength(50)]
        public string GoalType { get; set; } = string.Empty;

        [Range(1, double.MaxValue, ErrorMessage = "Target value must be a positive number.")]
        public double TargetValue { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7); 

        [Range(0, int.MaxValue, ErrorMessage = "Current value must be a positive number.")]
        public double CurrentValue { get; set; } = 0;

        // Navigation properties
        public virtual Users? User { get; set; }
        [Display(Name = "Goal Type")]

        public GoalType GT { get; set; }

        // Methods
        public double TrackProgress => TargetValue > 0 ? (int)Math.Round((CurrentValue / TargetValue) * 100) : 0;
        public bool IsGoalAchieved() { /* Implementation */ return false; }
    }
}
