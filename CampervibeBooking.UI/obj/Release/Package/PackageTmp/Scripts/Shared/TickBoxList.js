Campervibe.Shared.TickBoxList = function () {

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
    Campervibe.Shared.TickBoxList.setupValidators();
} (jQuery));

$(document).ready(function () {
    Campervibe.Shared.TickBoxList.initialize();
});