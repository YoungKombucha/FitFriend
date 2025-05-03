// workout.js - For handling workout-related operations
$(function () {
    // Load exercises for dropdown
    $.ajax({
        url: '/api/ExercisesApi',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var dropdown = $('#exerciseSelect');
            dropdown.empty();
            dropdown.append('<option selected="true" disabled>Choose Exercise</option>');
            dropdown.prop('selectedIndex', 0);

            $.each(data, function (key, entry) {
                dropdown.append($('<option></option>').attr('value', entry.exerciseId).text(entry.name));
            });
        }
    });

    // Handle adding exercise to workout
    $('#addExerciseBtn').on(function () {
        var exerciseId = $('#exerciseSelect').val();
        var exerciseName = $('#exerciseSelect option:selected').text();
        var sets = $('#sets').val();
        var reps = $('#reps').val();
        var weight = $('#weight').val();

        if (!exerciseId || !sets || !reps) {
            alert('Please fill in all required fields');
            return;
        }

        var newRow = '<tr>' +
            '<td><input type="hidden" name="WorkoutExercises[' + exerciseCounter + '].ExerciseId" value="' + exerciseId + '" />' + exerciseName + '</td>' +
            '<td><input type="hidden" name="WorkoutExercises[' + exerciseCounter + '].Sets" value="' + sets + '" />' + sets + '</td>' +
            '<td><input type="hidden" name="WorkoutExercises[' + exerciseCounter + '].Reps" value="' + reps + '" />' + reps + '</td>' +
            '<td><input type="hidden" name="WorkoutExercises[' + exerciseCounter + '].Weight" value="' + weight + '" />' + weight + '</td>' +
            '<td><button type="button" class="btn btn-sm btn-danger remove-exercise">Remove</button></td>' +
            '</tr>';

        $('#exercisesTable tbody').append(newRow);
        exerciseCounter++;

        // Clear inputs
        $('#sets').val('');
        $('#reps').val('');
        $('#weight').val('');
    });

    // Handle removing exercise from table
    $(document).on('click', '.remove-exercise', function () {
        $(this).closest('tr').remove();
    });
});

var exerciseCounter = 0;
