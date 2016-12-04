$(document).ready(function () {
    $(".remove-recipe").click(function () {
        $(this).addClass("hidden");
        $(this).siblings(".delete-message").removeClass("hidden");
        $(this).siblings(".confirm-delete").removeClass("hidden");
        $(this).siblings(".decline-delete").removeClass("hidden");
        $(".decline-delete").click(function () {
            $(".delete-message").addClass("hidden");
            $(".confirm-delete").addClass("hidden");
            $(".remove-recipe").removeClass("hidden");
            $(this).addClass("hidden");
        })
    })
});