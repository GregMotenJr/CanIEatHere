//jQuery for 'About' page:
$(document).ready(function () {
    //ANIMATED LINE:
    $("#box2").delay(2200).fadeIn(100).animate({ width: "50%" }, 3000);
    //TEXT FADE-IN:
    $("#text1").fadeIn(1000);
    $("#text2").delay(500).fadeIn(1200);
    //BACKGROUND FADE-IN:
    $('#back').animate({ opacity: 0 }, 0).css({ 'background-image': 'url (https://cdn.pixabay.com/photo/2017/04/11/17/54/appetizers-2222361_960_720.jpg)' }).animate({ opacity: 1 }, 2500);
});
