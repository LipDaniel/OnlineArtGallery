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
function fbLogin() {
    FB.login(function (response) {
        if (response.status == 'connected') {
            FB.api('/me', function (response) {
                const FacebookId = response.id
                const NameCustomer = response.name

                $.ajax({
                    type: "POST",
                    url: "/FEAuth/LoginFB",
                    data: {
                        facebook_id: FacebookId,
                        user_fname: NameCustomer,
                    },
                    success: function (rs) {
                        if (rs == "Successfully") {
                            window.location.href = "/FEHome/Index"
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        //do your own thing
                        alert("Something wrong in server.")
                    }
                });
            });
        }
    });
};

function fbLogout() {
    FB.logout(function (response) {
        console.log(response)
        // user is now logged out
    });
}

function decryptJwt(token) {
    return JSON.parse(atob(token.split('.')[1]));
}

function googleLogin(response) {
    const responsePayload = decryptJwt(response.credential);

    const GoogleId = responsePayload.sub
    const NameCustomer = responsePayload.name

    $.ajax({
        type: "POST",
        url: "/FEAuth/LoginGG",
        data: {
            google_id: GoogleId,
            user_fname: NameCustomer,
        },
        success: function (rs) {
            if (rs == "Successfully") {
                window.location.href = "/FEHome/Index"
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //do your own thing
            alert("Something wrong in server.")
        }
    });
}