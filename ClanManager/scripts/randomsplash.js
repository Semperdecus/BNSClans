$(document).ready(function () {
    var images = [location.protocol + "//" + location.host + "/images/splash1.jpg", location.protocol + "//" + location.host + '../images/splash2.jpg',
        location.protocol + "//" + location.host + '../images/splash3.jpg', location.protocol + "//" + location.host + '../images/splash4.jpg',
        location.protocol + "//" + location.host + '../images/splash5.jpg', location.protocol + "//" + location.host + '../images/splash6.jpg'];
    $('.container-splash').css({ 'background-image': 'url(' + images[Math.floor(Math.random() * images.length)] + ')' });
});