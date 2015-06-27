Campervibe = {};
Campervibe.Booking = {};
Campervibe.Invoice = {};

Campervibe.Layout = function () {

    var documentReadyInitialize = function () {

    };

    var configureDateValidationForGb = function () {
        $.validator.addMethod('date', function (value, element) {
            if (this.optional(element)) {
                return true;
            }

            var ok = true;
            try {
                $.datepicker.parseDate('dd/mm/yyyy', value);
            }
            catch (err) {
                ok = false;
            }
            return ok;
        });
    };

    var executeImmediatelyInitialize = function () {
        configureDateValidationForGb();
    };

    return {
        documentReadyInitialize: documentReadyInitialize,
        executeImmediatelyInitialize: executeImmediatelyInitialize
    };
} ();

(function ($) {
    Campervibe.Layout.executeImmediatelyInitialize();
} (jQuery));

$(document).ready(function () {
    Campervibe.Layout.documentReadyInitialize();
});