function toggleOrganizationTree()
{
    $('.map-toggle > span').click(function () {
        $('.map-left').animate({ width: 'toggle' }, function () {
            if ($('.organization-map').hasClass("open")) {
                $('.organization-map').removeClass("open").addClass("closed");
                $('body').removeClass("map-open");
            } else {
                $('.organization-map').removeClass("closed").addClass("open");
                $('body').addClass("map-open");
            }
        });
    });
}

function scrollView()
{
    $(".scroll-button").click(function () {
        var step = $(this).data("goStep");
        $("html, body").animate({
            scrollTop: $(step).position().top
        }, 1000);
    });
}
