$(document).ready(function () {
    $(".remove-recipe").remove();
    $(".delete-recipe").remove();
    $(".add-to-menu").remove();
    $(".individual-recipe-container").removeClass("hidden");
    $(".hide-individual-recipe").click(function () {
        $(this).parent().parent().addClass("hidden");
    })
    var baseUrl = "/api/RecipeTransfers/"
    $(".save-recipe").click(function () {
        var buttonId = $(this).attr("id");
        var data = { "id": buttonId }
        $.ajax({
            url: baseUrl + buttonId,
            type: "Put",
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            data: data,
            success: function (result) {
                $("#" + buttonId).hide();
                alert(result);
            }
        });
    })
})