$(document).ready(function () {
    $(".individual-recipe-container").removeClass("hidden");
    $(".hide-individual-recipe").click(function () {
        $(this).parent().parent().addClass("hidden");
    })
})