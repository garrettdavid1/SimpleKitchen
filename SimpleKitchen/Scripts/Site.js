$(document).ready(function () {
    if (!($("#logoutForm")[0])) {
        $(".requires-authentication").hide();
    } else {
        $(".requires-authentication").show();
    }
});