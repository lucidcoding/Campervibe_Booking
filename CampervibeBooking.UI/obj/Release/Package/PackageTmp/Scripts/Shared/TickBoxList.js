CampervibeBooking.Shared.TickBoxList = function () {

    var initialize = function () {

        $(".tick-box-list-container input[type='checkbox']").change(function () {
            var tickbox = $(this);
            var parent = tickbox.closest(".tick-box-list-container");
            var multilist = parent.find("select[multiple=multiple]");

            if (multilist.length > 0) {
                if (tickbox.is(":checked")) {
                    multilist.find("option[value=" + tickbox.attr("value") + "]").attr("selected", "selected");
                } else {
                    multilist.find("option[value=" + tickbox.attr("value") + "]").removeAttr("selected");
                }
            }
        });
    };

    var setupValidators = function () {

    };

    return {
        initialize: initialize,
        setupValidators: setupValidators
    };
} ();

(function ($) {
    CampervibeBooking.Shared.TickBoxList.setupValidators();
} (jQuery));

$(document).ready(function () {
    CampervibeBooking.Shared.TickBoxList.initialize();
});