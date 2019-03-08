$(document).ready(function(){
    var array = ['~/Content/Images/background-2.jpg', '~/Content/Images/background.jpg', '~/Content/Images/background-3.jpg', '~/Content/Images/background-4.jpg'];
var select_BG = array[Math.floor(Math.random()*array.length)] ;
$('body').css('background-image','url('+select_BG+')')
});