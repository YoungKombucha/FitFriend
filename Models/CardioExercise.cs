using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitFriend.Models
{
    public class CardioExercise : Exercise
    {
        public double Distance { get; set; }
        public int Duration { get; set; }
    }
    
}
