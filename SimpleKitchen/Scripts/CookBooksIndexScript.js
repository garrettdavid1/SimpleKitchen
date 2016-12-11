$(document).ready(function () {
    $(".cookbook-jumbotron").click(function () {
        $(this).siblings(".recipe-container").toggleClass("hidden");
        $(this).parent().closest("div").siblings(".cookbook-container").toggleClass("hidden");
    });
});