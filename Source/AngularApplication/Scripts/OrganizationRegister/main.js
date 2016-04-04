$(document).ready(function () {

    $('table.selectable tr').click(function () {
        $(this).find('td input[type=radio]').prop('checked', true);
        $(this).addClass('selected');
    });

    $('.edit-search').click(function (e) {

        $('.search-done, .edit-search').fadeOut('1600', function () {
            $('.do-search, .button.return-btn').fadeIn('1600');
        });
        $('.overlay').fadeIn();

    });

    $('.button.return-btn').click(function (e) {
        $('.do-search, .button.return-btn').fadeOut('1600',
        function () {
            $('.search-done, .edit-search').fadeIn('1600');
        });
        $('.overlay').fadeOut();
        /* $('.map-left').animate({"width":"380px"}, 1000); */
    });

});

$(document).ready(function () {
    $(".dropdown-layer").hide();
    $('.dropdown-open').click(function () {
        $(this).next('.dropdown-layer').toggle();
    });

});

$(document).ready(function () {
    $('.moreinfo-popup').click(function () {
        $(this).next('.moreinfo-layer').toggle();
    });
});

(function ($, undefined) {

    $('.dropdown-list-header').click(function () {
        $(this).toggleClass("open");
        $(this).next('.dropdown-list-content').toggle();
    });

    $('.info').click(function () {
        $(this).toggleClass("close");
        $(this).next('.infobox').slideToggle();
    });

    $("#page-header button").on('click', function () {
        var textSize = $("html").attr("data-text-size");
        var textSizeClass = "text-size-" + textSize;
        var newTextSizeClass = textSizeClass;
        if ($(this).hasClass('decrease-font-size') && (textSize > 1)) {
            $("#page-header .increase-font-size").removeClass("disabled");
            newTextSizeClass = "text-size-" + (textSize - 1);
            $("html").removeClass(textSizeClass).addClass(newTextSizeClass);
            $("html").attr("data-text-size", (textSize - 1));
            if (textSize < 3) {
                $(this).addClass("disabled");
            }
        } else {
            if ($(this).hasClass('increase-font-size') && (textSize < 5)) {
                $("#page-header .decrease-font-size").removeClass("disabled");
                newTextSizeClass = "text-size-" + (textSize * 1 + 1);
                $("html").removeClass(textSizeClass).addClass(newTextSizeClass);
                $("html").attr("data-text-size", (textSize * 1 + 1));
                if (textSize > 3) {
                    $(this).addClass("disabled");
                }
            }
        }
    });

    //$('.map-toggle > span').click(function ()... moved to ViewScript.js

})(jQuery);
