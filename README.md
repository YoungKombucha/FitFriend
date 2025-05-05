## Known Issue: ExerciseType Discriminator

Due to Entity Framework Core’s Table-Per-Hierarchy (TPH) inheritance, newly created exercises may sometimes not appear in the list if the `ExerciseType` discriminator value is missing or null in the database.

**How to Fix:**
Run this SQL command on your database to set the discriminator for existing records:
UPDATE dbo.Exercises
SET ExerciseType = 'Strength'
WHERE ExerciseType IS NULL OR ExerciseType = '';

After running this command, new exercises created as either `StrengthExercise` or `CardioExercise` will display correctly.
