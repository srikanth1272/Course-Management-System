function validateEmail(email) {
    var emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailPattern.test(email);
}
function validatePassword(password) {
    var passwordPattern = /^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    return passwordPattern.test(password);
}

$(document).ready(function () {

    $('#username').blur(function () {
        var username = $("#username").val();
        if (username === '')
            $('#userNameError').text('Username is Required');
        else if (username.length > 20)
            $('#userNameError').text('Username length should be less than 20');
    });

    $('#email').blur(function () {
        var email = $(this).val();
        if (email === '')
            $('#emailError').text('Email is required');
        else if (!validateEmail(email))
            $('#emailError').text("Enter a valid Email");
    });

    $('#password').blur(function () {
        var password = $("#password").val();
        if (password === '')
            $('#passwordError').text('Password is required');
        else if (password.length < 8) 
            $('#passwordError').text('password length must be above 8');
        else if (!validatePassword(password)) 
            $('#passwordError').text("password must contain at least one uppercase letter, one special character, and one number");
    });
    $('#RegisterForm').on('submit', function (event) {
        event.preventDefault();
        $('.error').text('');
        var isValid = true;

        var username = $("#username").val();
        if (username === '') {
            $('#userNameError').text('Username is Required');
            isValid = false;
        }
        var email = $("#email").val();
        if (email === '') {
            $('#emailError').text('Email is required');
            isValid = false;
        } 
        var password = $("#password").val();
        if (password === '') {
            $('#passwordError').text('password is required');
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
    $('#email').blur(function () {
        var email = $(this).val();
        if (email === '')
            $('#emailError').text('Email is required');
    });
    $('#password').blur(function () {
        var password = $("#password").val();
        if (password === '')
            $('#passwordError').text('Password is required');
    });

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

