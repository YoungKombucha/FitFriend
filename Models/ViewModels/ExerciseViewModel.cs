using System.ComponentModel.DataAnnotations;

public class ExerciseViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    public string ExerciseType { get; set; } = string.Empty;

    // Strength properties
    [Range(0, 1000, ErrorMessage = "Sets must be between 0 and 1000")]
    public int? Sets { get; set; }

    [Range(0, 1000, ErrorMessage = "Reps must be between 0 and 1000")]
    public int? Reps { get; set; }

    [Range(0, 1000, ErrorMessage = "Weight must be between 0 and 1000")]
    public double? Weight { get; set; }

    // Cardio properties
    [Range(0, 1000, ErrorMessage = "Distance must be between 0 and 1000")]
    public double? Distance { get; set; }

    [Range(0, 1000, ErrorMessage = "Duration must be between 0 and 1000")]
    public int? Duration { get; set; }
}
