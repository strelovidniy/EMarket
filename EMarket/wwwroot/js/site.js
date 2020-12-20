$(document).ready(function() {
    if (location.pathname == "/Account/RegisterAsBuyer") {
        $(".container").hide();
    }

    $(".login-button").click(function () {
        $(".login-modal").css("display", "flex");
        $(".login-popup").addClass("active");
        $(".login-popup-blur").css("opacity", "1");
    });

    $(".close-modal").click(function () {
        $(".login-popup").removeClass("active");
        $(".login-popup-blur").css("opacity", "0");

        setTimeout(function() {
            $(".login-modal").css("display", "none");
        },1000);

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

    $(".reg-button-wheat-left").css("left", "618px");
    $(".reg-button-wheat-right").css("right", "618px");

    $(".reg-button-wheat-left").css("transform", "rotate(-180deg);");
    $(".reg-button-wheat-right").css("transform", "rotate(-180deg);");
});

$(".reg-button").mouseleave(function () {
    $(".proceed-register").css("background-color", "white");
    $(".proceed-register").css("transform", "scale(1)");
    $(".proceed-register").css("color", "black");
    $(".reg-button-wheat-right").css("opacity", "0");
    $(".reg-button-wheat-left").css("opacity", "0");

    $(".reg-button-wheat-left").css("left", "644px");
    $(".reg-button-wheat-right").css("right", "644px");

    $(".reg-button-wheat-left").css("transform", "rotate(-45deg);");
    $(".reg-button-wheat-lerightft").css("transform", "rotate(-45deg);");





});

$(".reg-button").hover(function () {
    $(this).css("cursor", "pointer");
});