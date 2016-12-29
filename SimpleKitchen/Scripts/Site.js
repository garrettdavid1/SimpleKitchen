var shrinkElement = function (element, glyphicon) {
    $(element).text("");
    $(element).addClass("glyphicon glyphicon-" + glyphicon);
};
var expandElement = function (element, newText, glyphicon) {
    $(element).text(newText);
    $(element).removeClass("glyphicon glyphicon-" + glyphicon);
};

$(document).ready(function () {
    if (!($("#logoutForm")[0])) {
        $(".requires-authentication").hide();
    } else {
        $(".requires-authentication").show();
    }
    $(function(){
        if($(".navbar-brand").css("font-size") === "20px") {
            shrinkElement(".navbar-brand", "home");
        } else{
            expandElement(".navbar-brand", "Simple Kitchen", "home");
        }
    })

    $(function () {
        $(window).bind("resize", function () {
            if ($(this).width() < 300) {
                shrinkElement(".navbar-brand", "home");
            } else {
                expandElement(".navbar-brand", "Simple Kitchen", "home");
            }
        })
    });
});