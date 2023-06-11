$(document).ready(function ($) {
    if (localStorage.getItem("toast") !== null) {
        data = localStorage.getItem("toast");
        $('#toastbody').text(data)
        $('#toast').toast('show');
        localStorage.removeItem("toast");
    }
})