$(document).ready(function () {
    AOS.init();
    if (location.pathname == "/Account/RegisterAsBuyer" || location.pathname == "/Account/RegisterAsSeller") {
        $(".container").hide();
    }

    $(".login-button").click(function () {
        $(".login-modal").css("display", "flex");
        setTimeout(function () {
            $(".login-popup").addClass("active");
            $(".login-popup-blur").css("opacity", "1");
        }, 10);
    });

    $(".close-modal").click(function () {
        $(".login-popup").removeClass("active");
        $(".login-popup-blur").css("opacity", "0");

        setTimeout(function() {
            $(".login-modal").css("display", "none");
        },1000);

    });

    $(".menu").click(function() {
        $(".mobile-nav-wrapper").css("display", "flex");
        $(".mobile-nav-wrapper").css("opacity", "1");

        $(".mobile-nav-body").css("display", "flex");
        setTimeout(function () {
            $(".mobile-nav-body").css("transform", "scale(1)");
        }, 1);
    });

    $(".close-mobile").click(function () {
        $(".mobile-nav-wrapper").css("opacity", "0");

        $(".mobile-nav-body").css("transform", "scale(0)");
        setTimeout(function () {
            $(".mobile-nav-body").hide();
            $(".mobile-nav-wrapper").hide();
        }, 1000);
    });

    var totalHeight = document.body.scrollHeight - window.innerHeight;
    $(window).scroll(function() {
        var progressHeight = ($(window).scrollTop() / totalHeight) * 100;
        $(".progressbar").css("height", progressHeight + "%");
    });

});

$(".login-button").hover(function () {
    $(this).css("cursor", "pointer");
});

$(".login-button").mouseover(function () {
    $(".wheat-login-left").css("left", "-28px");
    $(".wheat-login-right").css("right", "-25px");

    $(".login-button").css("transform", "scale(1.05)");


    $(".wheat-login-left").css("opacity", "1");
    $(".wheat-login-right").css("opacity", "1");
});

$(".login-button").mouseleave(function () {
    $(".wheat-login-left").css("left", "0");
    $(".wheat-login-right").css("right", "0");

    $(".login-button").css("transform", "scale(1)");


    $(".wheat-login-left").css("opacity", "0");
    $(".wheat-login-right").css("opacity", "0");
});

$(".reg-button").mouseover(function() {
    $(".proceed-register").css("background-color", "#07c26a");
    $(".proceed-register").css("transform", "scale(1.05)");
    $(".proceed-register").css("color", "white");

    $(".reg-button-wheat-right").css("opacity", "1");
    $(".reg-button-wheat-left").css("opacity", "1");

    $(".reg-button-wheat-left").css("left", "480px");
    $(".reg-button-wheat-right").css("right", "480px");

    $(".reg-button-wheat-left").css("transform", "rotate(-180deg);");
    $(".reg-button-wheat-right").css("transform", "rotate(-180deg);");
});

$(".reg-button").mouseleave(function () {
    $(".proceed-register").css("background-color", "white");
    $(".proceed-register").css("transform", "scale(1)");
    $(".proceed-register").css("color", "black");
    $(".reg-button-wheat-right").css("opacity", "0");
    $(".reg-button-wheat-left").css("opacity", "0");

    $(".reg-button-wheat-left").css("left", "520px");
    $(".reg-button-wheat-right").css("right", "520px");

    $(".reg-button-wheat-left").css("transform", "rotate(-45deg);");
    $(".reg-button-wheat-lerightft").css("transform", "rotate(-45deg);");





});

$(".reg-button").hover(function () {
    $(this).css("cursor", "pointer");
});