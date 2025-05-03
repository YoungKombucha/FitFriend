using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitFriend.Models
{
    public class Goal
    {
        [Key]
        public int GoalId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [StringLength(50)]
        public string GoalType { get; set; }

        public double TargetValue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double CurrentValue { get; set; }

        // Navigation properties
        public virtual Users User { get; set; }

        // Methods
        public double TrackProgress() { /* Implementation */ return 0; }
        public bool IsGoalAchieved() { /* Implementation */ return false; }
    }
}
