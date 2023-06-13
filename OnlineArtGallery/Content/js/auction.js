$('#example').IconSelectBox(true);

$('.date_to').each(function () {
    var time_to = $(this).data('countdown') + " 23:59:59";
    var countDownDate = Date.parse(time_to);
    $this = $(this);
    interVal($this, countDownDate);
})

function interVal(element, countDownDate) {
    setInterval(function () {
        var now = new Date().getTime();
        var distance = countDownDate - now;
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
            element.text(days + " ngày " + hours + "h " + minutes + "m " + seconds + "s ");
        }
    }, 1000);
}

var formatDate = function (date) {
    var formattedDate = moment(date).format('MM/DD/YYYY h:mm:ss');
    return formattedDate;
}

$(document).ready(function ($) {
    $('#date_from').change(function (event) {

        if (!$('#date_to').val()) {
            return;
        }
        var date_from = formatDate($(this).val() + ":00");
        var date_to = formatDate($('#date_to').val() + ":00");
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
            element.val(days + " ngày " + hours + "h " + minutes + "m " + seconds + "s ");
        }
    })

    $('#date_to').change(function (event) {

        if (!$('#date_from').val()) {
            return;
        }
        var date_to = formatDate($(this).val()+":00");
        var date_from = formatDate($('#date_from').val()+":00");
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
            element.val(days + " ngày " + hours + "h " + minutes + "m " + seconds + "s ");
        }
    })
})