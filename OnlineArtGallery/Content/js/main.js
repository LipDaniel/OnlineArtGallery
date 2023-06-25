$(".wishlist").click(function (e) {
    e.preventDefault();
    var id = $(this).data("wishlist-id");

    $.ajax({
        url: "/FEWishlist/Create",
        method: "Post",
        data: {
            id: id
        }
    }).done(function (res) {

        $('.toast-body').text(res.noti);
        $('#toast').toast('show');
        if (res.count != null) {
            $('.wishlist-count').html(res.count);
            console.log(res.count);
        }
        console.log(res.noti);
    });
});
$(".cart").click(function (e) {
    e.preventDefault();
    var id = $(this).data("cart-id");

    $.ajax({
        url: "/FEOrder/Create",
        method: "Post",
        data: {
            id: id
        }
    }).done(function (res) {
        $('.toast-body').text(res.noti);
        $('#toast').toast('show');
        if (res.count != null) {
            $('cart-count').html(res.count);
            console.log(res.count);
        }
    });
});

$('.countdown').each(function () {
    var time_from = $(this).data('start');
    var time_to = $(this).data('end');

    var now = new Date().getTime();
    var countDownFrom = Date.parse(time_from);
    var countDownTo = Date.parse(time_to);

    var $this = $(this).children();

    if (now > countDownTo) {
        $(this).text("Finished");
        return;
    } else if (now < countDownFrom) {
        interVal($this, countDownFrom);
        return;
    } else {
        interVal($this, countDownTo);
        return;
    }
})

function validateNumber(e) {
    var charCode = (e.which) ? e.which : e.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function interVal(element, Time) {
    var set = setInterval(function () {

        var now = new Date().getTime();
        if (Time > now) {
            var distance = Time - now;
        } else {
            var distance = now - Time;
        }
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);
        if (days == 0) {
            if (hours == 0) {
                if (minutes == 0) {
                    if (seconds == 0) {
                        element.text("Finished");
                        clearInterval(set);
                    } else {
                        element.text(seconds + "s ");
                    }
                } else {
                    element.text(minutes + "m " + seconds + "s ");
                }
            } else {
                element.text(hours + "h " + minutes + "m " + seconds + "s ");
            }
        } else {
            if (days == 1) {
                element.text(days + " day " + hours + " h " + minutes + "m " + seconds + "s ");
            } else {
                element.text(days + " days " + hours + " h " + minutes + "m " + seconds + "s ");
            }
        }
    }, 1000);
}
function checkAuction() {
 
}

$(document).ready(function ($) {
    if (localStorage.getItem("toast") !== null) {
        data = localStorage.getItem("toast");
        $('#toast-body').text(data)
        $('#toast').toast('show');
        localStorage.removeItem("toast");
    }

    $.ajax({
        url: "/BEAuction/GetAuctionSuccess",
        method: "GET",
    }).done(function (res) {
        console.log(res)
    });

    $("#SignUpForm").submit(function (e) {

        e.preventDefault();

        var pwd = $("#register-password").val();
        var confirmpwd = $("#confirm-password").val();

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
                user_password: $('#register-password').val(),
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

    $(".bid").click(function (e) {
        e.preventDefault();
        $(this).children('span').addClass('spinner-border spinner-border-sm');
        $(this).prop('disabled', true)

        var id = $(this).data('auctionid');
        var current_price = $(this).data('currentprice');
        var amount = $('.bid_price_'+id).val();
        var $this = $(this);

        if (amount <= current_price || (amount - current_price) % 50 != 0) {
            $('.toast-body').text("Invalid bid");
            $('#toast').toast('show');
            $(this).children('span').removeClass('spinner-border spinner-border-sm');
            $(this).prop('disabled', false)
            return;
        }
        $.ajax({
            url: "/BEAuction/CreateBid",
            method: "Post",
            data: {
                auction_amount: amount,
                id: id
            }
        }).done(function (res) {
            $('.toast-body').text(res.noti);

            $('#toast').toast('show');

            $this.children('span').removeClass('spinner-border spinner-border-sm');

            $('.bid_price_' + id).val("");

            $('.setup_current_' + id).text('$' + res.bid);

            $this.data('currentprice', res.bid);

            $this.prop('disabled', false)
        });
    });

    $('#searchBox').click(function () {
        $.ajax({
            url: '/BEIndex/Search',
            type: 'GET',
        }).done(function (data) {
            $('#searchList').empty();
            $('#searchList').html(data);
        })
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