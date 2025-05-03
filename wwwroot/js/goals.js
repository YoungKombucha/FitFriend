// goals.js - For handling goal tracking and visualization
$(function () {
    // Load goal progress data
    $('.goal-progress').each(function () {
        var goalId = $(this).data('goal-id');
        var progressElement = $(this);

        $.ajax({
            url: '/api/GoalsApi/' + goalId,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var progressPercent = (data.currentValue / data.targetValue) * 100;
                progressPercent = Math.min(progressPercent, 100);

                progressElement.find('.progress-bar').css('width', progressPercent + '%');
                progressElement.find('.progress-bar').attr('aria-valuenow', progressPercent);
                progressElement.find('.progress-text').text(progressPercent.toFixed(0) + '%');

                // Update goal status
                if (progressPercent >= 100) {
                    progressElement.find('.goal-status').html('<span class="badge bg-success">Achieved</span>');
                }
            }
        });
    });

    // Update goal progress
    $('#updateGoalBtn').on("click", function () {
        var goalId = $('#goalId').val();
        var currentValue = $('#currentValue').val();

        $.ajax({
            url: '/api/GoalsApi/' + goalId,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify({
                goalId: goalId,
                userId: $('#userId').val(),
                goalType: $('#goalType').val(),
                targetValue: $('#targetValue').val(),
                startDate: $('#startDate').val(),
                endDate: $('#endDate').val(),
                currentValue: currentValue
            }),
            success: function () {
                alert('Goal progress updated successfully!');
                location.reload();
            },
            error: function () {
                alert('Error updating goal progress');
            }
        });
    });
});
