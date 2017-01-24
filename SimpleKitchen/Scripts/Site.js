$(document).ready(function () {
    var shrinkElement = function (element, glyphicon) {
        $(element).text("");
        $(element).addClass("glyphicon glyphicon-" + glyphicon);
    };
    var expandElement = function (element, newText, glyphicon) {
        $(element).text(newText);
        $(element).removeClass("glyphicon glyphicon-" + glyphicon);
    };
    //Hide authentication-dependent elements when user is not logged in.
    $(function(){
        if (!($("#logoutForm")[0])) {
            $(".requires-authentication").hide();
        } else {
            $(".requires-authentication").show();
        }
    })
    //Navbar - Use glyphicon-home when window is small based on font-size changed by media query.
    $(function(){
        if($(".navbar-brand").css("font-size") === "20px") {
            shrinkElement(".navbar-brand", "home");
        } else{
            expandElement(".navbar-brand", "Simple Kitchen", "home");
        }
    })
    //Navbar - Toggle glyphicon-home when window is resized to/from 300px width.
    $(function () {
        $(window).bind("resize", function () {
            if ($(this).width() < 300) {
                shrinkElement(".navbar-brand", "home");
            } else {
                expandElement(".navbar-brand", "Simple Kitchen", "home");
            }
        })
    });
    //Remove recipe from cookbook & remove recipe-container from DOM
    $(".remove-recipe").click(function () {
        var baseUrl = "/api/RecipeTransfers/"
        var recipeId = $(this).attr("id");
        var cookbookId = $(this).attr("class").split(' ').pop();
        var data = JSON.stringify({ "id": recipeId, "cookbookId": cookbookId });
        $.ajax({
            url: baseUrl + "Remove/?rid=" + recipeId + '&cid=' + cookbookId,
            type: "Put",
            contentType: "application/json; charset=utf-8",
            data: data,
            success: function (result) {
                $(this).remove;
                alert(result);
            }
        });
        $(this).parent().parent().remove();
    })
});