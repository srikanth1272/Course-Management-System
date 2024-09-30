$(document).ready(function () {
    
    $("#StdSubjectForm").on('submit', function (event) {
        event.preventDefault();
        $('.error').text('');
        var isValid = true;

        var RollNo = $("#RollNo").val();
        if (RollNo === '') {
            $('#rollNoError').text('Please enter RollNo');
            isValid = false;
        }

        var SubjectId = $("#SubjectId").val();
        if (SubjectId === '') {
            $('#subjectIdError').text('Please enter Subject SubjectId');
            isValid = false;
        }

        var Semister = $("#Semister").val();
        if (Semister === '') {
            $('#semisterError').text('Please enter Semister.');
            isValid = false;
        }

        if (isValid) {
            var requestType = $('#edit').val() === 'Save' ? "PUT" : "POST";
            var url = $('#edit').val() === 'Save'
                ? "http://localhost:5027/api/StdSubject/" + RollNo + "/" + SubjectId
                : "http://localhost:5027/api/StdSubject/";
            $.ajax({
                type: requestType,
                url: url,
                data: JSON.stringify({ "RollNo": RollNo, "SubjectId": SubjectId, "Semister": Semister,"studentdetails":" ","subjectdetails" :" "}),
                contentType: 'application/json',
                success: function (response) {
                    var successMessage = $('#edit').val() === 'Save'
                        ? "Updated Successfully."
                        : "Subject Added Successfully.";
                    toastr.success(successMessage)
                    setTimeout(function () { window.location.href = "/StdSubject/GetDetails"; }, 1000);
                },
                error: function (jqXHR) {
                    errorMessage = jqXHR.responseText;
                    toastr.error(errorMessage);
                }
            });
        }

    });
});
