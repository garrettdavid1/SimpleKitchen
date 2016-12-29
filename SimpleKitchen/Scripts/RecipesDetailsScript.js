$(document).ready(function () {
    $(function(){
         if ($(".screen-size-indicator").css("font-size") === "2px") {
             $(".yields-buffer").addClass("hidden");
         } else {
             $(".yields-buffer").removeClass("hidden");
         }
     })
})