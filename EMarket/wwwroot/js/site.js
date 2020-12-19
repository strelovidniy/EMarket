$(document).ready(function() {
    if (location.pathname == "/buyer/register") {
        $(".container").hide();
    }
});

$(".login-button").hover(function () {
    $(this).css("cursor", "pointer");
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