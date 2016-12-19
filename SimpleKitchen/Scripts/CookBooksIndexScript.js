$(document).ready(function () {
    $(".cookbook-name-button").click(function () {
        var selector = ".cb-" + $(this).val().toString();
        $(selector).toggleClass("hidden");
        $(this).toggleClass("btn-primary");
        $(function(){
            if (!($(this).hasClass("btn-primary"))) { //Case: This class is hidden
                $("#show-all-recipes").removeClass("btn-primary");
            };
            if (!($(".cookbook-name-button").hasClass("btn-primary"))) { //Case: All buttons are hidden
                $("#hide-all-recipes").addClass("btn-primary");
            } else {//Case: At least one button is visible
                $("#hide-all-recipes").removeClass("btn-primary");
            };
        })
    });
    $("#show-all-recipes").click(function () {
        $(".individual-recipe-container").removeClass("hidden");
        $(".cookbook-name-button").addClass("btn-primary");
        $("#hide-all-recipes").removeClass("btn-primary");
        $(".cookbook-name-display").removeClass("hidden");
    });
    $("#hide-all-recipes").click(function () {
        $(".individual-recipe-container").addClass("hidden");
        $(".cookbook-name-button").removeClass("btn-primary");
        $(this).addClass("btn-primary");
        $(".cookbook-name-display").addClass("hidden");
    });
    $(".hide-individual-recipe").click(function () {
        var cookBookId = $(this).parent().parent().attr("class").split(" ").pop();
        var cookBookIdForButton = $(this).attr("class").split(" ").pop();
        var container = $(this).parent().parent();
        $(container).addClass("hidden");
        var numOfContainers = $(this).parent().parent().siblings(".individual-recipe-container." + cookBookId).length;
        var numOfHiddenContainers = $(this).parent().parent().siblings(".individual-recipe-container.hidden." + cookBookId).length;
        if(numOfContainers == numOfHiddenContainers) {
            $('#' + cookBookIdForButton).removeClass("btn-primary");
            $(cookBookId).addClass("hidden");
            $("#show-all-recipes").removeClass("btn-primary");
            $(".cookbook-name-display." + cookBookId).addClass("hidden");
        }
    });
    $(function () {
        $(window).bind("resize", function () {
            if ($(this).width() < 770) {
                $(".create-recipe-container").addClass("hidden");
                $(".cookbook-button-wrapper").removeClass("col-xs-6").addClass("col-xs-12");
                $(".button-divider").addClass("hidden");
                $(".unnecessary-nav").removeClass("hidden");
                $(".cookbook-button-container").css("margin-left", "0px");
                $(".title").addClass("hidden");

            } else if ($(this).width() < 992) {
                $(".unnecessary-nav").addClass("hidden");
                $(".button-divider").removeClass("hidden");
                $(".create-recipe-container").removeClass("hidden");
                $(".title").removeClass("hidden");
            }
            else {
                $(".unnecessary-nav").removeClass("hidden");
                $(".create-recipe-container").removeClass("hidden");
                $(".cookbook-button-wrapper").removeClass("col-xs-12").addClass("col-xs-6");
                $(".button-divider").removeClass("hidden");
                $(".title").removeClass("hidden");
            }
        })
    })
});