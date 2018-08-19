$(function () {
    function toggleSubmenu(menu) {
        var submenu = $("#" + menu.attr("name"));
        if (submenu) {
            var show = submenu.hasClass('d-block');
            submenu.attr("class", show ? "d-none" : "d-block");
            menu.toggleClass("downarrow");
        }
    }

    $(".submenu").click(function (e) {
        e.preventDefault();
        toggleSubmenu($(this));
    });
});