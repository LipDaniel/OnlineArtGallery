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
            if (res == "Successfully") {
                localStorage.setItem('toast', res);
                window.location.reload();
            };
        })

    })
})
