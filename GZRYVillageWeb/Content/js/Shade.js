var w, h, className;
function getSrceenWH() {
    w = $(window).width();
    h = $(window).height();
    $('#dialogBg').width(w).height(h);
   
}

window.onresize = function () {
    getSrceenWH();
}
$(window).resize();

$(function () {
    getSrceenWH();

    $('.box a').click(function () {
        className = $(this).attr('class');
        $('.box #dialogBg').fadeIn(300);
        $('.box #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
       
    });
    $('.box .claseDialogBtn').click(function () {
        $('.box #dialogBg').fadeOut(300, function () {
            $('.box #dialog').addClass('bounceOutUp').fadeOut();
        });
      
    });
    $('.box1 a').click(function () {
        className = $(this).attr('class');
        $('.box1 #dialogBg').fadeIn(300);
        $('.box1 #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();

    });
    $('.box1 .claseDialogBtn').click(function () {
        $('.box1 #dialogBg').fadeOut(300, function () {
            $('.box1 #dialog').addClass('bounceOutUp').fadeOut();
        });

    });
});