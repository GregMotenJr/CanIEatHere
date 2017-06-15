//jQuery for 'Home' page:
$(document).ready(function () {
    //TEXT FADE-IN:
    $("#text1").delay(1000).fadeIn(1000);
    $("#text2").delay(1300).fadeIn(1500);
    //ANIMATED LINE:
    $("#box2").delay(1700).fadeIn(100).animate({ width: "50%" }, 1500);
    //ANIMATED BACKGROUND:
    $('#back').animate({ opacity: 0 }, 0).css({ 'background-image': 'url(https://preview.ibb.co/hONP3Q/home_about_artichokes.jpg)' }).animate({ opacity: 1 }, 2500);
});