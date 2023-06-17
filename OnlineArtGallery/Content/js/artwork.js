artwork_image.onchange = evt => {
    var filePath = artwork_image.value;
    // Allowing file type
    var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    $('#artworkalert').text('')

    if (!allowedExtensions.exec(filePath)) {
        $('#artworkalert').text("* File isn't in the correct format")
        artwork_preview.src = ""
        artwork_image.value = '';
        return false;
    }

    const [file] = artwork_image.files
    if (file) {
        artwork_preview.src = URL.createObjectURL(file)
    }
}

edit_artwork_image.onchange = evt => {
    var filePath = edit_artwork_image.value;
    // Allowing file type
    var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    $('#edit_artworkalert').text('')

    if (!allowedExtensions.exec(filePath)) {
        $('#edit_artworkalert').text("* File isn't in the correct format")
        edit_artwork_preview.src = ""
        edit_artwork_image.value = '';
        return false;
    }

    const [file] = edit_artwork_image.files
    if (file) {
        edit_artwork_preview.src = URL.createObjectURL(file)
    }
}

function transferEditArtwork(id, name, price, dimensions, date, category, artist, image, description, status) {
    $('#edit_artwork_id').val(id)
    $('#edit_artwork_name').val(name)
    $('#edit_artist_id').val(artist)
    $('#edit_category_id').val(category)
    $('#edit_artwork_price').val(price)
    $('#edit_artwork_date').val(date)
    $('#edit_dimensions').val(dimensions)
    $('#edit_artwork_description').val(description)
    if (status == 1) {

        $('#artwork_status').attr('checked', true);

    } else if (status == 2) {

        $('#artwork_status').attr('checked', true)
        $('#artwork_status').prop('disabled', true)

    } else if (status == 3) {

        $('#artwork_status').attr('checked', false)
        $('#artwork_status').prop('disabled', true)

    }
    else if (status == 4) {

        $('#artwork_status').attr('checked', false)
        $('#artwork_status').prop('disabled', true)

    } else {

        $('#artwork_status').attr('checked', false)

    }

    edit_artwork_preview.src = "/Content/assets/images/artwork/" + image;
}

$(document).ready(function ($) {

    $('#artworkform').submit(function (event) {
        event.preventDefault();

        var artwork_name = $('#artwork_name').val();
        var artwork_price = $('#artwork_price').val();
        var artwork_dimensions = $('#width').val() + " x " + $('#height').val();
        var artwork_description = $('#artwork_description').val();
        var category_id = $('#category_id').val();
        var artist_id = $('#artist_id').val();
        var artwork_date = $('#artwork_date').val();
        var artwork_image = $('#artwork_image')[0].files[0];

        var formData = new FormData()
        formData.append("artwork_name", artwork_name)
        formData.append("artwork_price", artwork_price)
        formData.append("artwork_dimensions", artwork_dimensions)
        formData.append("artwork_description", artwork_description)
        formData.append("category_id", category_id)
        formData.append("artist_id", artist_id)
        formData.append("artwork_date", artwork_date)
        formData.append("artwork_image", artwork_image)
        
        $.ajax({
            method: "POST",
            url: "/BEArtwork/Create",
            data: formData,
            processData: false,
            contentType: false,
        }).done(function (res) {
            localStorage.setItem('toast', res);
            window.location.reload();
        })
    })

    $('#editartworkform').submit(function (event) {
        event.preventDefault();

        var artwork_id = $('#edit_artwork_id').val();
        var artwork_name = $('#edit_artwork_name').val();
        var artwork_price = $('#edit_artwork_price').val();
        var artwork_dimensions = $('#edit_dimensions').val();
        var artwork_description = $('#edit_artwork_description').val();
        var category_id = $('#edit_category_id').val();
        var artist_id = $('#edit_artist_id').val();
        var artwork_date = $('#edit_artwork_date').val();
        var artwork_image = $('#edit_artwork_image')[0].files[0];
        var artwork_status = $('#artwork_status').is(':checked') ? 1 : 0 

        var formData = new FormData()
        formData.append("artwork_id", artwork_id)
        formData.append("artwork_name", artwork_name)
        formData.append("artwork_price", artwork_price)
        formData.append("artwork_dimensions", artwork_dimensions)
        formData.append("artwork_description", artwork_description)
        formData.append("category_id", category_id)
        formData.append("artist_id", artist_id)
        formData.append("artwork_date", artwork_date)
        formData.append("artwork_image", artwork_image)
        formData.append("artwork_status", artwork_status)

        $.ajax({
            method: "POST",
            url: "/BEArtwork/Edit",
            data: formData,
            processData: false,
            contentType: false,
        }).done(function (res) {
            localStorage.setItem('toast', res);
            window.location.reload();
        })
    })

    $('.btn_approve').click(function () {
        var id = $(this).data('id');
        $('#contents').text("Are you sure to approve this artwork");
        $('#transferid').val(id)
        $('#transferstatus').val(1)
    })

    $('.btn_refuse').click(function () {
        var id = $(this).data('id');
        $('#contents').text("Are you sure to refuse this artwork");
        $('#transferid').val(id)
        $('#transferstatus').val(5)
    })

    $('#approveForm').submit(function (event) {
        event.preventDefault();

        var id = $('#transferid').val();
        var status = $('#transferstatus').val();

        $.ajax({
            method: "POST",
            url: "/BEArtwork/Approve",
            data: {
                artwork_id: id,
                artwork_status: status
            }
        }).done(function (data) {
            localStorage.setItem('toast', data);
            window.location.reload();
        })
    })
})
