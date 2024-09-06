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
        else if (username.length > 10) {
            $('#userNameError').text('Username length should be less than 10');
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
            this.submit(); 
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
            //$.ajax({
            //    url: 'http://localhost:5299/api/User/login/',
            //    type: 'POST',
            //    dataType: 'json',
            //    data:
            //    {
            //        Email: email,
            //        Password: password
            //    },
            //    success: function () {
            //        alert('success');
            //    },
            //    error: function () {
            //        alert('error');
            //    }
            //});
            this.submit();
        }
    });
});
