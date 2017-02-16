var recipeCopyCount = 1;
function allowDrop(ev) {
    ev.preventDefault();
}
function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}
function drop(ev) {
    if(!$(ev.target).hasClass("daily-recipes")){
        ev.preventDefault();
    }else{
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    var nodeCopy = document.getElementById(data).cloneNode(true);
    var nodeSelector = "#" + nodeCopy.id;
    var newId = "copied-recipe-" + recipeCopyCount.toString() + "-id-" + data;
    var recipeName = $(nodeSelector)
        .children(".individual-recipe")
        .children(".recipe-name")
        .children(".recipe-text")
        .text();
    var dailyRecipeContainer = document.createElement("div");
    $(dailyRecipeContainer).css({"word-wrap": "normal", "font-size": "12px" });

    var node = document.createElement("div")
    $(node).addClass("col-xs-12 daily-recipe text-center").text(recipeName);
    node.id = newId;
    dailyRecipeContainer.appendChild(node);

    var removeDailyRecipeButton = document.createElement("button");
    $(removeDailyRecipeButton).addClass("col-xs-12 btn btn-danger remove-daily-recipe text-center").text("^ Remove ^").css("font-size", "10px");
    dailyRecipeContainer.appendChild(removeDailyRecipeButton);

    var lineBreak = document.createElement("br");
    dailyRecipeContainer.appendChild(lineBreak);
    ev.target.appendChild(dailyRecipeContainer);

    $(".remove-daily-recipe").click(function () {
        $(this).parent("div").remove();
    })

    recipeCopyCount += 1;
    }
}
$(document).ready(function () {
    function getRecipeId(idString) {
        return parseInt(idString.split("").pop());
    }
    $("footer").remove();
    $(".footer-divider").remove();
    
    $("#generate-list").click(function () {
        var recipeHTMLCollection = document.getElementsByClassName("daily-recipe");
        var recipeArray = Array.prototype.slice.call(recipeHTMLCollection);
        var recipeIdArray = [recipeArray.length];
        for (var i = 0; i < recipeHTMLCollection.length; i++) {
            recipeIdArray[i] = getRecipeId(recipeArray[i].id);
            console.log(recipeIdArray[i]);
        }
        var baseUrl = "/api/RecipeTransfers/"
        $.ajax({
            url: baseUrl + "GenShoppingList",
            type: "Post",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(recipeIdArray),
            success: function (ingredients) {
                alert(ingredients);
            }
        });
    });
})