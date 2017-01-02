$(document).ready(function () {
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