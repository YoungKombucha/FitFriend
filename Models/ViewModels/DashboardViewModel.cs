using System.Collections.Generic;

namespace FitFriend.Models.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Workout> RecentWorkouts { get; set; } = new List<Workout>();
        public IEnumerable<Goal> ActiveGoals { get; set; } = new List<Goal>();
        public DailyLog LatestDailyLog { get; set; } = new DailyLog();
    }
}