$(document).ready(function () {
    $('#SubjectId').blur(function () {
        $('#idError').text('');
        var SubjectId = $("#SubjectId").val();
        if (SubjectId.length != 6)
            $('#idError').text('SubjectId must be 6 characters');
    });
    $('#Title').blur(function () {
        $('#titleError').text('');
        var Title = $("#Title").val();
        if (Title.length > 40)
            $('#titleError').text('title must be less than 40');
    });
    $('#TotalClasses').blur(function () {
        $('#classError').text('');
        var TotalClasses = $("#TotalClasses").val();
        if (TotalClasses <= 0)
            $('#classError').text('Total Classes must be greater than 0');
    });
    $('#Credits').blur(function () {
        $('#creditError').text('');
        var Credits = $("#Credits").val();
        if (Credits <= 0)
            $('#creditError').text('Credits must be greater than 0');

    });
    $("#SubjectForm").on('submit', function (event) {
        event.preventDefault();
        $('.error').text('');
        var isValid = true;

        var SubjectId = $("#SubjectId").val();
        if (SubjectId === '') {
            $('#idError').text('Please enter SubjectId');
            isValid = false;
        }

        var Title = $("#Title").val();
        if (Title === '') {
            $('#titleError').text('Please enter Subject Title');
            isValid = false;
        }

        var TotalClasses = $("#TotalClasses").val();
        if (TotalClasses === '') {
            $('#classError').text('Please enter Total Classes');
            isValid = false;
        }

        var Credits = $("#Credits").val();
        if (Credits === '') {
            $('#creditError').text('Please enter Credits.');
            isValid = false;
        }

        if (isValid) {
            var requestType = $('#edit').val() === 'Save' ? "PUT" : "POST";
            var url = $('#edit').val() === 'Save'
                ? "http://localhost:5112/api/Subject/" + SubjectId
                : "http://localhost:5112/api/Subject/";
            $.ajax({
                type: requestType,
                url: url,
                data: JSON.stringify({ "SubjectId": SubjectId, "Title": Title, "TotalClasses": TotalClasses, "Credits": Credits }),
                contentType: 'application/json',
                success: function (response) {
                    var successMessage = $('#edit').val() === 'Save'
                        ? "Subject Updated Successfully."
                        : "Subject Created Successfully.";
                    toastr.success(successMessage)
                    setTimeout(function () { window.location.href = "/Subject/Index"; }, 1000);
                },
                error: function (jqXHR) {
                    errorMessage = jqXHR.responseText;
                    toastr.error(errorMessage);
                }
            });
        }

    });
});


