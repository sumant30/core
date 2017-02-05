/*site.js*/

(function () {

    var $sidebarandWrapper = $("#sidebar,#wrapper");

    $("#sidebarToggle").on("click", function () {
        $sidebarandWrapper.toggleClass("hide-sidebar");
        if ($sidebarandWrapper.hasClass("hide-sidebar")) {
            $("#sidebarToggle").val("Show-Sidebar")
        }
        else {
            $("#sidebarToggle").val("Hide-Sidebar")
        }
    });
    

})();