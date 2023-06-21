$(document).ready(function ($) {

    $('#checkout').submit(function (event) {
        event.preventDefault();
        $('#checkoutspinner').addClass('spinner-border spinner-border-sm');
        $('#submitCheckout').prop('disabled', true);

        var fname = $('#fname').val().trim();
        var lname = $('#lname').val().trim();
        var address = $('#address').val().trim();
        var email = $('#checkoutemail').val().trim();
        var phone = $('#checkoutphone').val().trim();
        var total = $('#submitCheckout').data('total');


        if (fname == '' || lname == '' || email == '' || phone == '' || address == '') {
            $('.toast-body').text("Shipping Information is Require !!!");
            $('#toast').toast('show');
            $('#toast').removeClass('bg-primary').addClass('bg-danger');
            $('#checkoutspinner').removeClass('spinner-border spinner-border-sm');
            $('#submitCheckout').prop('disabled', false);
            return;
        }

        $.ajax({
            method: "POST",
            url: "/FEOrder/CreateBill",
            data: {
                order_phone: phone,
                order_address: address,
                order_email: email,
                order_fname: fname,
                order_lname: lname,
                order_total: total
            }
        }).done(function (data) {
            console.log('Success');
        })
    })
})