Campervibe = {};
Campervibe.Layout = {};
Campervibe.Customer = {};
Campervibe.Booking = {};
Campervibe.Shared = {};

Campervibe.Shared.Layout = function () {

    var initialize = function () {
        $("form").each(function (index, element) {
            var validators = $(element).data("validator");

            if (validators != undefined) {
                validators.settings.ignore = ":hidden:not(.date-picker-container:visible .date-picker-validator)";
            }
        });
    };

    return {
        initialize: initialize,
    };
} ();

$(document).ready(function () {
    Campervibe.Shared.Layout.initialize();
});
