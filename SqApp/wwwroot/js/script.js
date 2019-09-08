$(document).ready(function(){
    $('.right-user').hover(function() {
        $('.user-menu').slideToggle(300);
    });
    $('.right-user > .user').click(function(e) {
        e.preventDefault();
    });
    $('i.fa-bars').on('click', function (e) {
        e.preventDefault();
        $('.mob-menu').toggleClass('act-mob-menu');
        $('.main').toggleClass('act-main');
        $('body').toggleClass('act-body');
    });
});