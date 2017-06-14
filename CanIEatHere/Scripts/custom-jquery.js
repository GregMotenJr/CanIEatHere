//jQuery for 'Home' page:
$(document).ready(function () {
    //ANIMATED LINE:
    $("#box2").delay(2200).fadeIn(100).animate({ width: "50%" }, 3000);
    //TEXT FADE-IN:
    $("#text1").fadeIn(1000);
    $("#text2").delay(500).fadeIn(1200);

    $('#back').animate({ opacity: 0 }, 0).css({ 'background-image': 'url(https://cdn.pixabay.com/photo/2016/10/02/20/02/salad-1710328_960_720.jpg)' }).animate({ opacity: 1 }, 4000);
});