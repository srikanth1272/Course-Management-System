function validateEmail(email) {
    var emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailPattern.test(email);
}

$(document).ready(function () { 
    $('#RollNo').blur(function () {
        $('#rollNoError').text('');
        var RollNo = $("#RollNo").val();
        if (RollNo.length != 10)
            $('#rollNoError').text('RollNo must be 10 characters');
    });
    $('#FirstName').blur(function () {
        $('#firstNameError').text('');
        var FirstName = $("#FirstName").val();
        if (FirstName.length > 20)
            $('#firstNameError').text('FirstName must be less than 20');
    });
    $('#LastName').blur(function () {
        $('#lastNameError').text('');
        var LastName = $("#LastName").val();
        if (LastName > 20)
            $('#lastNameError').text('LastName must be less than 20');
    });

    $('#Email').blur(function () {
        $('#emailError').text('');
        var Email = $("#Email").val();
        if (!validateEmail(Email))
            $('#emailError').text('Please enter  Valid Email');
    });
    $('#Phone').blur(function () {
        $('#phoneError').text('');
        var Phone = $("#Phone").val();
        if (Phone.length != 10)
            $('#phoneError').text('Phone Number must be 10 characters');
    });
    $('#Address').blur(function () {
        $('#addressError').text('');
        var Address = $("#Address").val();
        if (Address.length > 100)
            $('#addressError').text('Address must be less than 100 characters');
    });

    $("#StudentForm").on('submit', function (event) {
        event.preventDefault();
        $('.error').text('');
        var isValid = true;

        var RollNo = $("#RollNo").val();
        if (RollNo === '') {
            $('#rollNoError').text('Please enter RollNo');
            isValid = false;
        }

        var FirstName = $("#FirstName").val();
        if (FirstName === '') { 
            $('#firstNameError').text('Please enter FirstName');
            isValid = false;
        }


        var LastName = $("#LastName").val();
        if (LastName === '') { 
            $('#lastNameError').text('Please enter Last Name');
            isValid = false;
        }
        var DOB = $("#DOB").val();
        var dob = new Date(DOB);
        if (DOB === '') {
            $('#dobError').text('Please enter date of birth.'); 
            isValid = false;
        }

        var Email = $("#Email").val();
        if (Email === '') {
            $('#emailError').text('Please enter Email');
            isValid = false;
        }

        var Phone = $("#Phone").val();
        if (Phone === '') {
            $('#phoneError').text('Please enter Phone Number');
            isValid = false;
        }

        var Address = $("#Address").val();
        if (Address === '') {
            $('#addressError').text('Please enter Address');
            isValid = false;
        }

        if (isValid) {
            var requestType = $('#edit').val() === 'Save' ? "PUT" : "POST";
            var url = $('#edit').val() === 'Save'
                ? "http://localhost:5037/api/Student/" + RollNo
                : "http://localhost:5037/api/Student/";
            $.ajax({
                type: requestType,
                url: url,
                data: JSON.stringify({ "RollNo": RollNo, "FirstName": FirstName, "LastName": LastName, "DOB": dob, "Email": Email, "Phone": Phone, "Address": Address }),
                contentType: 'application/json',
                success: function (response) {
                    var successMessage = $('#edit').val() === 'Save'
                        ? "Student Updated Successfully."
                        : "Student Added Successfully.";
                    toastr.success(successMessage)
                    setTimeout(function () { window.location.href = "/Student/Index"; }, 1000);
                },
                error: function (jqXHR) {
                    errorMessage = jqXHR.responseText;
                    toastr.error(errorMessage);
                }
            });
        }

    });
});


