document.addEventListener('DOMContentLoaded', () => {
    "use strict";

    const navbarContent = $('#navbar-content');

    if (navbarContent.length) {
        const headerScrolled = () => {
            if ($(window).scrollTop() > 100) {
                navbarContent.addClass('sticked');
            } else {
                navbarContent.removeClass('sticked');
            }
        };

        $(window).on('scroll', headerScrolled);
    }
});