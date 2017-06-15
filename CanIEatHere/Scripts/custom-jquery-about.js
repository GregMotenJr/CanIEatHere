//jQuery for 'About' page:
$(document).ready(function () {
    //BACKGROUND:
    $('#back').animate({ opacity: 0 }, 0).css({ 'background-image': 'url(https://preview.ibb.co/nvkmiQ/appetizers_2222361_1280.jpg)' }).animate({ opacity: 1 }, 2500);
    //GRID:
    $('#aboutUsGrid').delay(1000).fadeIn(3000);
});