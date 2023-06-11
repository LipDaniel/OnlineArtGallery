$(document).ready(function ($) {
    if (localStorage.getItem("toast") !== null) {
        data = localStorage.getItem("toast");
        $('#toast-body').text(data)
        $('#toast').toast('show');
        localStorage.removeItem("toast");
    }
})