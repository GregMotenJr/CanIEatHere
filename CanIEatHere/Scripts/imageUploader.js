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
        console.log(imageSrc);
        show();
    });
}

// PRINTING THE URL AFTER UPLOAD INSIDE ITS OWN DIV
function show() {
    //document.getElementById(uploadedImage).src = event.fpfile.url;
    document.getElementById('image-container').innerHTML += '<div class="thumbnail image-thumbnail col-md-4">' + '<img class="img-responsive uploadedImage" src="' + imageSrc + '"/></div>'
    $('#containerForImageControls').append("<input type='button' value='Add Another Image' id='addAnother' class='btn btn-primary' />");

    var deleteCount = $('.deleteImage').length;

    if (deleteCount === 0) {
        $('#image-container .image-thumbnail:nth-child(1)').append("<div class='caption'><input type='button' value='Delete Image' class='btn btn-primary deleteImage' /></div>");
    }
    else if (deleteCount === 1) {
        $('#image-container .image-thumbnail:nth-child(2)').append("<div class='caption'><input type='button' value='Delete Image' class='btn btn-primary deleteImage' /></div>");
    }
    else if (deleteCount === 2) {
        $('#image-container .image-thumbnail:nth-child(3)').append("<div class='caption'><input type='button' value='Delete Image' class='btn btn-primary deleteImage' /></div>");
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
        $('#addAnother').remove();
        $('#image-container').append("<input type='button' value='Add Another Image' id='addAnother' class='btn btn-primary' />");
    }
    else if (deleteButton === 1) {
        $('#image-container div:nth-child(2)').remove();
        $(this).remove();
        $('#addAnother').remove();
        $('#image-container').append("<input type='button' value='Add Another Image' id='addAnother' class='btn btn-primary'/>");
    }
    else if (deleteButton === 2) {
        $('#image-container div:nth-child(3)').remove();
        $(this).remove();
        $('#addAnother').remove();
        $('#image-container').append("<input type='button' value='Add Another Image' id='addAnother' class='btn btn-primary'/>");
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

function displayImagesForEdit() {
    var imageSrc1 = $('#firstImage').val();
    var imageSrc2 = $('#secondImage').val();
    var imageSrc3 = $('#thirdImage').val();

    if (imageSrc1 != "") {
        document.getElementById('image-container').innerHTML += '<div class="thumbnail image-thumbnail col-md-4 col-sm-12">' + '<img class="img-responsive uploadedImage" src="' + imageSrc1 + '"/><input type="button" value="Delete Image" class="deleteImage btn" /></div>';
    }
    if (imageSrc2 != "") {
        document.getElementById('image-container').innerHTML += '<div class="thumbnail image-thumbnail col-md-4 col-sm-12">' + '<img class="img-responsive uploadedImage" src="' + imageSrc2 + '"/><input type="button" value="Delete Image" class="deleteImage btn" /></div>';
    }
    if (imageSrc3 != "") {
        document.getElementById('image-container').innerHTML += '<div class="thumbnail image-thumbnail col-md-4 col-sm-12">' + '<img class="img-responsive uploadedImage" src="' + imageSrc3 + '"/><input type="button" value="Delete Image" class="deleteImage btn" /></div>';
    }
}

function addAddImagesButton() {
    var deleteCount = $('.deleteImage').length;

    if (deleteCount < 3) {
        $('#image-container').append("<input type='button' value='Add Image' id='addAnother' class='btn'/>");
    }
}

