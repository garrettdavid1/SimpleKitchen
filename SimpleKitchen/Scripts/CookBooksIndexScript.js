$(document).ready(function () {
    $(".cookbook-name-button").click(function () {
        var selector = ".cb-" + $(this).val().toString();
        $(selector).toggleClass("hidden");
    });
    $("#show-all-recipes").click(function () {
        $(".individual-recipe").removeClass("hidden");
    });
    $("#hide-all-recipes").click(function () {
        $(".individual-recipe").addClass("hidden");
    });
});