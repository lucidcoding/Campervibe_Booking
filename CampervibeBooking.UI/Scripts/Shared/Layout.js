Campervibe = {};
CampervibeBooking.Layout = {};
CampervibeBooking.Customer = {};
CampervibeBooking.Booking = {};
CampervibeBooking.Shared = {};

CampervibeBooking.Shared.Layout = function () {

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
    CampervibeBooking.Shared.Layout.initialize();
});
