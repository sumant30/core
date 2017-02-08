/*site.js*/

(function () {

    var $sidebarandWrapper = $("#sidebar,#wrapper");
    var $icon = $("#sidebarToggle i.fa")

    $("#sidebarToggle").on("click", function () {
        $sidebarandWrapper.toggleClass("hide-sidebar");
        if ($sidebarandWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa fa-angle-left");
            $icon.addClass("fa fa-angle-right");
        }
        else {
            $icon.removeClass("fa fa-angle-right");
            $icon.addClass("fa fa-angle-left");
         }
    });
    

})();