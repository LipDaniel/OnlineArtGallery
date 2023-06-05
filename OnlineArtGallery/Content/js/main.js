$(document).ready(function ($) {
    if (localStorage.getItem("toast") !== null) {
        data = localStorage.getItem("toast");
        $('#toast-body').text(data)
        $('#toast').toast('show');
        localStorage.removeItem("toast");
    }

    $("#SignUpForm").submit(function (e) {
        
        e.preventDefault();

        var pwd = $("#register_password").val();
        var confirmpwd = $("#confirm_password").val();

        if (pwd != confirmpwd) { 
            $('.toast-body').text("Confirm password not match");
            $('#toast').removeClass('bg-primary');
            $('#toast').addClass("bg-danger");
            $('#toast').toast('show');
            return;
        }
        $('#spinnerRegis').addClass('spinner-border spinner-border-sm');
        $('#btnRegis').prop('disabled', true)
        $.ajax({
            url: "/FEAuth/SignUp",
            method: "Post",
            data: {
                user_lname: $('#singin-lname').val(),
                user_fname: $('#singin-fname').val(),
                user_email: $('#register-email').val(),
                user_password: $('#register_password').val(),
            },
        }).done(function (res) {
            if (res == "Successfully") {
                $('.toast-body').text("Register Successfully");
                $('#toast').addClass("bg-success").removeClass('bg-danger');
                $('#toast').toast('show');
                $('#SignUpForm').trigger('reset');

                $('#spinnerRegis').removeClass('spinner-border spinner-border-sm');
                $('#btnRegis').prop('disabled', false)
            } else {
                $('.toast-body').text(res);
                $('#toast').removeClass('bg-primary').addClass("bg-danger");
                $('#toast').toast('show');

                $('#spinnerRegis').removeClass('spinner-border spinner-border-sm');
                $('#btnRegis').prop('disabled', false)
            }
        });
    });


    $("#signInForm").submit(function (e) {

        e.preventDefault();

        var email = $("#singin-email").val();
        var password = $("#singin-password").val();

        
        $('#spinnerLogin').addClass('spinner-border spinner-border-sm');
        $('#btnLogin').prop('disabled', true)

        $.ajax({
            url: "/FEAuth/SignIn",
            method: "Post",
            data: {
                user_email: email,
                user_password: password,
            },
        }).done(function (res) {
            if (res == "Admin") {
                window.location.href = "/BEIndex/Index"
            } else if (res == "User") {
                window.location.href = "/FEHome/Index"
            } else {
                $('.toast-body').text(res);
                $('#toast').removeClass('bg-primary').addClass("bg-danger");
                $('#toast').toast('show');

                $('#spinnerLogin').removeClass('spinner-border spinner-border-sm');
                $('#btnLogin').prop('disabled', false)
            }
        });
    });
})