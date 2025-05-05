UPDATE dbo.Exercises 
SET ExerciseType = 'Strength' 
WHERE ExerciseType IS NULL OR ExerciseType = '';
