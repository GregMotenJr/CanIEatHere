// THE FILESTACK CLIENT
//var client = filestack.init('AbbxbmtG7Qzmx3N6F8qgmz');
var client = filestack.init('AjOmEuqLOQsmThwy9glYKz');
var imageSrc;
function showPicker() {
    client.pick({
    }).then(function (result) {
        console.log(JSON.stringify(result.filesUploaded));
        imageSrc = result.filesUploaded[0].url;
        $('#fileStackButton').remove();
        $('#addAnother').remove();
        show();
    });
}

// PRINTING THE URL AFTER UPLOAD INSIDE ITS OWN DIV
function show() {
    //document.getElementById(uploadedImage).src = event.fpfile.url;
    document.getElementById('image-container').innerHTML += '<div class="image-thumbnail">' + '<img class="uploadedImage" src="' + imageSrc + '"/></div>'
    $('#image-container').append("<input type='button' value='Add Another Image' id='addAnother' />");

    var deleteCount = $('.deleteImage').length;

    if (deleteCount === 0) {
        $('#image-container div:nth-child(1)').append("<input type='button' value='Delete Image' class='deleteImage' />");
    }
    else if (deleteCount === 1) {
        $('#image-container div:nth-child(2)').append("<input type='button' value='Delete Image' class='deleteImage' />");
    }
    else if (deleteCount === 2) {
        $('#image-container div:nth-child(3)').append("<input type='button' value='Delete Image' class='deleteImage' />");
    }
    //$('.image-thumbnail').append("<input type='button' value='Delete Image' class='deleteImage' />");
}

function addMoreImages() {
    var count = $('.uploadedImage').length;

    if (count > 2) {
        $('#addAnother').val("Sorry, there is a maximum of 3 images.");
    }
    else {
        showPicker();
    }
}

function deleteSelectedImage() {
    var deleteButton = $(this).index('.deleteImage');

    if (deleteButton === 0) {
        $('#image-container div:nth-child(1)').remove();
        $(this).remove();
    }
    else if (deleteButton === 1) {
        $('#image-container div:nth-child(2)').remove();
        $(this).remove();
    }
    else if (deleteButton === 2) {
        var stuff = $('#image-container div:nth-child(3)').remove();
        $(this).remove();
        $('#addAnother').val("Add Another Image");
    }
}

function storeImagesInReview() {
    var firstImageUrl = $('#image-container div:nth-child(1)').children('img').attr('src');
    var secondImageUrl = $('#image-container div:nth-child(2)').children('img').attr('src');
    var thirdImageUrl = $('#image-container div:nth-child(3)').children('img').attr('src');

    if (firstImageUrl != null) {
        $('#firstImage').val(firstImageUrl);
    }
    if (secondImageUrl != null) {
        $('#secondImage').val(secondImageUrl);
    }
    if (thirdImageUrl != null) {
        $('#thirdImage').val(thirdImageUrl);
    }
}

function displayImages() {
    var imageSrc1 = $('#firstImage').val();
    var imageSrc2 = $('#secondImage').val();
    var imageSrc3 = $('#thirdImage').val();

    if (imageSrc1 != null) {
        document.getElementById('image-container').innerHTML += '<div class="image-thumbnail">' + '<img class="uploadedImage" src="' + imageSrc1 + '"/><input type="button" value="Delete Image" class="deleteImage" /></div>'
    }
    if (imageSrc2 != null) {
        document.getElementById('image-container').innerHTML += '<div class="image-thumbnail">' + '<img class="uploadedImage" src="' + imageSrc2 + '"/><input type="button" value="Delete Image" class="deleteImage" /></div>'
    }
    if (imageSrc3 != null) {
        document.getElementById('image-container').innerHTML += '<div class="image-thumbnail">' + '<img class="uploadedImage" src="' + imageSrc3 + '"/><input type="button" value="Delete Image" class="deleteImage" /></div>'
    }

    var deleteCount = $('.deleteImage').length;

    if (deleteCount < 3) {
        $('#image-container').append("<input type='button' value='Add Another Image' id='addAnother' />");
    }


}

