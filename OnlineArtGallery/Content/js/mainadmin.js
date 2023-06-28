$(document).ready(function ($) {
    localStorage.removeItem('revenue2023');

    if (localStorage.getItem("toast") !== null) {
        data = localStorage.getItem("toast");
        $('#toastbody').text(data)
        $('#toast').toast('show');
        localStorage.removeItem("toast");
    }

    $.ajax({
        method: "GET",
        url: "/BEDashboard/GetRevenueEachYear",
    }).done(function (data) {
        var i = 1;
        var Revenue2022 = new Array();
        $.each(JSON.parse(data), function (key, value) {
            Revenue2022.push(value.revenues)
        })
        localStorage.setItem('revenue2022', JSON.stringify(Revenue2022));
    })

    $.ajax({
        method: "GET",
        url: "/BEDashboard/GetRevenueEachYear2023",
    }).done(function (data) {
        var i = 1;
        var Revenue2022 = new Array();
        $.each(JSON.parse(data), function (key, value) {
            Revenue2022.push(value.revenues)
        })
        localStorage.setItem('revenue2023', JSON.stringify(Revenue2022));
    })
})