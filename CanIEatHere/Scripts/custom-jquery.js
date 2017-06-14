//jQuery for 'Home' page:
$(document).ready(function () {
    //ANIMATED LINE:
    $("#box2").delay(2200).fadeIn(100).animate({ width: "50%" }, 3000);
    //TEXT FADE-IN:
    $("#text1").fadeIn(1000);
    $("#text2").delay(500).fadeIn(1200);
    //BACKGROUND FADE-IN:
    $('#back').animate({ opacity: 0 }, 0).css({ 'background-image': 'url (https://cdn.pixabay.com/photo/2016/10/31/18/16/salad-1786313_960_720.jpg)' }).animate({ opacity: 1 }, 2500);
});