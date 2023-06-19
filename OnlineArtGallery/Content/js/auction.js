$('#artworkList').IconSelectBox(true);

$('.end-date').each(function () {
    var time_to = $(this).text();
    var time_from = $(this).prev('.start-date').text();

    var countDownFrom = Date.parse(time_from);
    var countDownTo = Date.parse(time_to);
    $this = $(this).next('span');

    var now = new Date().getTime();

    if (now > countDownTo) {
        $(this).next().next().children().css("width", "100%")
        $(this).next().next().children().removeClass("bg-danger").addClass('bg-success')
        $(this).next('span').text("Finished");
        $(this).next('span').removeClass("bg-danger-light text-danger").addClass('bg-success-light text-success');
        return;
    }
    if (now < countDownFrom) {
        $(this).next().next().children().css("width", "0%")
        return;
    } else {
        var total = countDownTo - countDownFrom;
        var percent = now - countDownFrom;
        var result = percent / (total / 100) + "%"
        $(this).next().next().children().css("width", result)
        interVal($this, countDownTo);
    }
})

function interVal(element, countDownTo) {
    setInterval(function () {
        var now = new Date().getTime();
        var distance = countDownTo - now;
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);
        if (days == 0) {
            if (hours == 0) {
                if (minutes == 0) {
                    element.text(seconds + "s ");
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

var formatDate = function (date) {
    var formattedDate = moment(date).format('MM/DD/YYYY h:mm:ss');
    return formattedDate;
}

function validateNumber(e) {
    var charCode = (e.which) ? e.which : e.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

$(document).ready(function ($) {

    $('#auctionForm').submit(function (event) {
        event.preventDefault();

        var artwork_id = $('#artworkList').val();
        var auction_reserve_price = $('#auction_price').val();
        var auction_start_date = $('#date_from').val();
        var auction_end_date = $('#date_to').val();

        $.ajax({
            method: "POST",
            url: "/BEAuction/Create",
            data: {
                artwork_id: artwork_id,
                auction_reserve_price: auction_reserve_price,
                auction_start_date: auction_start_date,
                auction_end_date: auction_end_date
            },
        }).done(function (res) {
            localStorage.setItem('toast', res);
            window.location.reload();
        })
    })

    $('#date_from').change(function (event) {

        if (!$('#date_to').val()) {
            return;
        }
        var date_from = formatDate($(this).val());
        var date_to = formatDate($('#date_to').val());
        var element = $('#real_time');

        new_date_from = Date.parse(date_from);
        new_date_to = Date.parse(date_to);

        var distance = new_date_to - new_date_from;
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        if (days == 0) {
            if (hours == 0) {
                if (minutes == 0) {
                    element.val(seconds + " s ");
                } else {
                    element.val(minutes + " m " + seconds + " s ");
                }
            } else {
                element.val(hours + " h " + minutes + "m " + seconds + " s ");
            }
        } else {
            if (days == 1) {
                element.val(days + " day " + hours + " h " + minutes + "m " + seconds + "s ");
            } else {
                element.val(days + " days " + hours + " h " + minutes + "m " + seconds + "s ");
            }
        }
    })

    $('#date_to').change(function (event) {

        if (!$('#date_from').val()) {
            return;
        }
        var date_to = formatDate($(this).val());
        var date_from = formatDate($('#date_from').val());
        var element = $('#real_time');

        new_date_from = Date.parse(date_from);
        new_date_to = Date.parse(date_to);

        var distance = new_date_to - new_date_from;
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        if (days == 0) {
            if (hours == 0) {
                if (minutes == 0) {
                    element.val(seconds + "s ");
                } else {
                    element.val(minutes + "m " + seconds + "s ");
                }
            } else {
                element.val(hours + "h " + minutes + "m " + seconds + "s ");
            }
        } else {
            element.val(days + "days " + hours + "h " + minutes + "m " + seconds + "s ");
        }
    })
})