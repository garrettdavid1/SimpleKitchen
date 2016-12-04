$(document).ready(function () {
    var removeAnchorNodeList = document.getElementsByClassName("remove-recipe");
    for (var i = 0; i < removeAnchorNodeList.length; i++) {
        var id = removeAnchorNodeList[i].id.toString();
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
    }
})

var createDynamicIdName = function (idName) {
    var idArray = idName.split('\'');
    return idArray[1].toString();
}