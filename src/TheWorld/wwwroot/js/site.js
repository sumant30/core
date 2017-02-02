/*site.js*/

(function () {

    var $sidebarandWrapper = $("#sidebar,#wrapper");

    $("sidebarToggle").on("click", function () {
        $sidebarandWrapper.toggleClass("hide-sidebar");
    });
    

})();