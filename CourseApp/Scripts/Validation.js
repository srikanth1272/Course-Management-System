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

        if (isValid) {
            this.submit(); 
        }
    });
});

//$(document).ready(function () {
//    $('#loginForm').on('submit', function (event) {
//        event.preventDefault();
//        $('.error').text('');
//        var isValid = true;
//        var email = $("#email").val();
//        if (email === '') {
//            $('#emailError').text('Email is required');
//            isValid = false;
//        } else if (!validateEmail(email)) {
//            $('#emailError').text("Enter a valid Email");
//            isValid = false;
//        }
//        var password = $("#password").val();
        //if (password === '') {
        //    $('#passwordError').text('password is required');
        //    isValid = false;
        //} else if (password.length < 8) {
        //    $('#passwordError').text('password length must be above 8');
        //    isValid = false;
        //}
        //else if (!validatePassword(password)) {
        //    $('#passwordError').text("password must contain at least one uppercase letter, one special character, and one number");
        //    isValid = false;
        //}
    });
});


$(document).ready(function () {
    $('#loginform').on('submit', function (event) {
        event.preventdefault(); 
        var formdata = $(this).serialize(); 
        $.ajax({
            url: $(this).attr('action'),
            type: 'post',
            data: formdata,
            success: function (response) {
                window.location.href = response.redirecturl;
            },
            error: function (xhr) {
                var response = json.parse(xhr.responsetext);
                $('#formerror').text(response.message); 
            }
        });
    });
});
