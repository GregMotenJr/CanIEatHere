// THE FILESTACK CLIENT
var client = filestack.init('AbbxbmtG7Qzmx3N6F8qgmz');
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
}

