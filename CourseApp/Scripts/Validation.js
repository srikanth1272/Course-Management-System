function validateEmail(email) {
    var emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailPattern.test(email);
}
function validatePassword(password) {
    var passwordPattern = /^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    return passwordPattern.test(password);
}
$(document).ready(function () {
    $('#RegisterForm').on('submit', function (event) {
        event.preventDefault();
        $('.error').text('');
        var isValid = true;

        var username = $("#username").val();
        if (username === '') {
            $('#userNameError').text('Username is Required');
            isValid = false;
        }
        else if (username.length > 20) {
            $('#userNameError').text('Username length should be less than 20');
            isValid = false;
        }
        var email = $("#email").val();
        if (email === '') {
            $('#emailError').text('Email is required');
            isValid = false;
        } else if (!validateEmail(email)){
            $('#emailError').text("Enter a valid Email");
            isValid = false;
        }
        var password = $("#password").val();
        if (password === '') {
            $('#passwordError').text('password is required');
            isValid = false;
        } else if (password.length < 8) {
            $('#passwordError').text('password length must be above 8');
            isValid = false;
        }
        else if (!validatePassword(password)) {
            $('#passwordError').text("password must contain at least one uppercase letter, one special character, and one number");
            isValid = false;
        }
        var confirmPassword = $('#confirmPassword').val();
        if (confirmPassword != password) {
            $('#confirmPasswordError').text('Password Does not Match. Please Check');
            isValid = false;
        }
        if (isValid) {
            $.ajax({
                type: "POST",
                url: "http://localhost:5299/api/User/",
                data: JSON.stringify({ "email": email, "username": username, "password": password }),
                contentType: 'application/json',
                success: function (response) {
                    $.ajax({
                        type: "POST",
                        url: "/User/Authenticate",
                        data: JSON.stringify({ email: email }),
                        contentType: 'application/json',
                        success: function () {
                            toastr.success("Registered Successfully and Logged In.")
                           
                            setTimeout(function () { window.location.href = "/home"; }, 1000);
                        }
                    });
                },
                error: function (jqXHR) { 
                    errorMessage = jqXHR.responseText;
                    toastr.error(errorMessage);
                }
            });
        }
    });
});

$(document).ready(function () {
    $('#LoginForm').on('submit', function (event) {
        event.preventDefault();  

        $('.error').text('');
        var isValid = true;

        var email = $("#email").val();
        if (email === '') {
            $('#emailError').text('Email is required');
            isValid = false;
        }

        var password = $("#password").val();
        if (password === '') {
            $('#passwordError').text('Password is required');
            isValid = false;
        }
        if (isValid) {
            $.ajax({
                type: "POST",
                url: "http://localhost:5299/api/User/Login",
                data: JSON.stringify({  "email": email,"username": "", "password": password }),
                contentType: 'application/json',
                success: function (response) {
                    $.ajax({
                        type: "POST",
                        url: "/User/Authenticate",  
                        data: JSON.stringify({ email: email }),
                        contentType: 'application/json',
                        success: function () {
                            toastr.success("Logged In Successfully")
                            setTimeout(function () { window.location.href = "/home"; }, 1000);
                        }
                    });
                },
                error: function (jqXHR) {
                 
                    errorMessage = jqXHR.responseText;
                    toastr.error(errorMessage);
                }
            });
        }
    });
});


$(document).ready(function () {
    $("#SubjectForm").on('submit', function (event) {
        event.preventDefault();
        $('.error').text('');
        var isValid = true;

        var SubjectId = $("#SubjectId").val();
        if (SubjectId === '') {
            $('#idError').text('Please enter SubjectId');
            isValid = false;
        }
        else if (SubjectId.length != 6) {
            $('#idError').text('SubjectId must be 6 characters');
            isValid = false;
        }
        var Title = $("#Title").val();
        if (Title === '') {
            $('#titleError').text('Please enter Subject Title');
            isValid = false;
        }
        else if (Title.length > 40) {
            $('#titleError').text('title must be less than 40');
            isValid = false;
        }
        var TotalClasses = $("#TotalClasses").val();
        if (TotalClasses === '') {
            $('#classError').text('Please enter Total Classes');
            isValid = false;
        }
        else if (TotalClasses <= 0) {
            $('#classError').text('Total Classes must be greater than 0');
            isValid = false;
        }
        var Credits = $("#Credits").val();
        if (Credits === '') {
            $('#creditError').text('Please enter Credits.');
            isValid = false;
        }
        else if (Credits <= 0) {
            $('#creditError').text('Credits must be greater than 0');
            isValid = false;
        }

        if (isValid) {
            toastr.success('Created Successful!');
            this.submit();
        }

    });
})