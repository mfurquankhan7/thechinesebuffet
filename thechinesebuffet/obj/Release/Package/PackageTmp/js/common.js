$(document).ready(function () {
    $('.responsive-btn').click(function () {
        $('.responsive-menu').css('display', 'block');
        $('.responsive-menu').animate({
            'right': 0
        }, 500, function () {
            //
        });
    });
    $('.resclose').click(function () {
        $('.responsive-menu').animate({
            'right': -300
        }, 500, function () {
            $('.responsive-menu').css('display', 'none');
        });
    });
});