using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitFriend.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        [StringLength(10)]
        public string Gender {  get; set; } = string.Empty;

        public double Height { get; set; }

        public double Weight { get; set; }

        //Nav properties
        public virtual ICollection<Workout> Workout { get; set; }
        public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<DailyLog> DailyLogs { get; set; }

        // Methods
        public void Register() { /* Implementation */ }
        public bool Login() { /* Implementation */ return true; }
        public void UpdateProfile() { /* Implementation */ }
        public List<Workout> GetWorkouts() { /* Implementation */ return new List<Workout>(); }
    }
}
