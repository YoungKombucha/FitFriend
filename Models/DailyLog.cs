using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitFriend.Models
{
    public class DailyLog
    {
        [Key]
        public int LogId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public int Steps { get; set; }

        public double CaloriesBurned { get; set; }

        public double WaterIntake { get; set; }

        public double SleepHours { get; set; }

        // Navigation properties
        public virtual Users? User { get; set; }

        // Methods
        public void UpdateDailyLog() { /* Implementation */ }
        public double CalculateTotalCalories() { /* Implementation */ return 0; }
    }
}
