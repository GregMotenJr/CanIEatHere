//jQuery for 'About' page:
$(document).ready(function () {
    //BACKGROUND:
    $("#back").animate({ opacity: 0 }, 0).css({ "background-image": "url(https://preview.ibb.co/nvkmiQ/appetizers_2222361_1280.jpg)" }).animate({ opacity: 1 }, 2500);
    //GRID:
    $("#aboutUsGrid").delay(1000).fadeIn(3000);
});

$(document).ready(function () {
    //ABOUT US DIVS
    var effect = "slide";
    var options = { direction: "right" };
    var duration = 1000;
    $("#Megan").delay(1000).toggle(effect, options, duration);
    $("#Greg").delay(1100).toggle(effect, options, duration);
    $("#Anthony").delay(1200).toggle(effect, options, duration);
    $("#Liberty").delay(1300).toggle(effect, options, duration);
});




//$(".myButton").click(function () {

//    var effect = "slide";
//    var options = { direction: "left" }
//    var duration = 1000;

//    $("#Megan").toggle(effect, options, duration);
//    $("#Greg").delay(500).toggle(effect, options, duration);
//    $("#Anthony").delay(1000).toggle(effect, options, duration);
//    $("#Liberty").delay(1500).toggle(effect, options, duration);

//});

