$(document).ready(function () {
    $(".cookbook-name-button").click(function () {
        var selector = ".cb-" + $(this).val().toString();
        $(selector).toggleClass("hidden");
        $(this).toggleClass("btn-primary");
        $(function(){
            if (!($(this).hasClass("btn-primary"))) {
                $("#show-all-recipes").removeClass("btn-primary");
            };
            if (!($(".cookbook-name-button").hasClass("btn-primary"))) {
                $("#hide-all-recipes").addClass("btn-primary");
            } else {
                $("#hide-all-recipes").removeClass("btn-primary");
            };
        })
    });
    $("#show-all-recipes").click(function () {
        $(".individual-recipe-container").removeClass("hidden");
        $(".cookbook-name-button").addClass("btn-primary");
        $("#hide-all-recipes").removeClass("btn-primary");
    });
    $("#hide-all-recipes").click(function () {
        $(".individual-recipe-container").addClass("hidden");
        $(".cookbook-name-button").removeClass("btn-primary");
        $(this).addClass("btn-primary");
    });
    $(function () {
        $(window).bind("resize", function () {
            if ($(this).width() < 770) {
                $(".create-recipe-container").addClass("hidden").css("margin-left", "0px");
                $(".cookbook-button-wrapper").removeClass("col-xs-6").addClass("col-xs-12");
                $(".button-divider").addClass("hidden");
                $(".unnecessary-nav").removeClass("hidden");
                $(".cookbook-button-container").css({"border-radius": "7%", "margin-left": "0px"});

            } else if ($(this).width() < 992) {
                $(".unnecessary-nav").addClass("hidden");
                $(".cookbook-button-container").css({ "border-top-right-radius": "0%", "border-bottom-right-radius": "0%" });
            }
            else {
                $(".unnecessary-nav").removeClass("hidden");
                $(".create-recipe-container").removeClass("hidden").css("margin-left", "-10px");
                $(".cookbook-button-wrapper").removeClass("col-xs-12").addClass("col-xs-6");
                $(".button-divider").removeClass("hidden");
                $(".cookbook-button-container").css({ "border-top-right-radius": "0%", "border-bottom-right-radius": "0%" });
            }
        })
    })
});