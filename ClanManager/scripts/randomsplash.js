$(document).ready(function () {
    var images = ['../images/splash1.jpg', '../images/splash2.jpg', '../images/splash3.jpg', 
        '../images/splash4.jpg', '../images/splash5.jpg', '../images/splash6.jpg'];
    $('.container-splash').css({ 'background-image': 'url(' + images[Math.floor(Math.random() * images.length)] + ')' });
});