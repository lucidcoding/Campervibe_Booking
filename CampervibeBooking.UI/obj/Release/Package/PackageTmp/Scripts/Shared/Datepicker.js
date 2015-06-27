Campervibe.Shared.Datepicker = function () {

    var calculateFormattedDate = function (selectedDayValue, selectedMonthValue, selectedYearValue) {

        //Check whether a full date has been supplied
        var completeDateEntryPresent = selectedDayValue != null && selectedDayValue != "" && selectedDayValue != undefined
                                       && selectedMonthValue != null && selectedMonthValue != "" && selectedMonthValue != undefined
                                       && selectedYearValue != null && selectedYearValue != "" && selectedYearValue != undefined;

        //If a full date is present invoke client-side validation on the date picker device
        if (completeDateEntryPresent) {

            //Construct a month offset in recognition of differences between MVC & JS dates starting at 0 and 1, respectively
            var monthOffset = selectedMonthValue - 1;

            //Construct a javascript date object using the offset value for comparison purposes 
            var userSelectedDate = new Date(selectedYearValue, monthOffset, selectedDayValue);

            //Set value in hidden validation field, so unobtrusive validation can be done on it.
            selectedMonthValue = ("0" + selectedMonthValue);
            selectedMonthValue = selectedMonthValue.substr(selectedMonthValue.length - 2);
            selectedDayValue = ("0" + selectedDayValue);
            selectedDayValue = selectedDayValue.substr(selectedDayValue.length - 2);
            var dateFormat = selectedYearValue + "-" + selectedMonthValue + "-" + selectedDayValue;
            return dateFormat;

        } else {
            return "";
        }
    };

    var initialize = function () {

        //Gather all datepicker instances
        //var datepickerComponent = $(".date-picker-component");

        //Attach function to the change event
        $(".date-picker-container select").change(function () {

            //Navigate to parent container
            //var datePickerContainer = $(this).parent().parent();
            var datePickerContainer = $(this).closest(".date-picker-container");

            //Get supplied values from components
            var selectedDayValue = $(datePickerContainer).find(".date-picker-day").val();
            var selectedMonthValue = $(datePickerContainer).find(".date-picker-month").val();
            var selectedYearValue = $(datePickerContainer).find(".date-picker-year").val();

            //Find hidden control as unobtrusive validation is being done on this.
            var hiddenControlToValidate = $(datePickerContainer).find(".date-picker-validator").first();
            var dateFormat = calculateFormattedDate(selectedDayValue, selectedMonthValue, selectedYearValue);
            hiddenControlToValidate.val(dateFormat);
        });
    };

    var setupValidators = function () {

        $.validator.addMethod("validdate", function (currVal, element) {

            //Do not attempt to validate if not set.
            if (currVal == '' || currVal == null || currVal == undefined)
                return true;

            //Declare Regex 
            var rxDatePattern = /^(\d{4})(-)(\d{2})(-)(\d{2})$/;
            var dtArray = currVal.match(rxDatePattern); // is format OK?

            if (dtArray == null)
                return false;

            //Checks for yyyy-mm-dd format.
            dtDay = dtArray[5];
            dtMonth = dtArray[3];
            dtYear = dtArray[1];

            if (dtMonth < 1 || dtMonth > 12)
                return false;
            else if (dtDay < 1 || dtDay > 31)
                return false;
            else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
                return false;
            else if (dtMonth == 2) {
                var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));

                if (dtDay > 29 || (dtDay == 29 && !isleap))
                    return false;
            }

            return true;
        });

        $.validator.unobtrusive.adapters.addBool("validdate");

        $.validator.addMethod("notinpast", function (value, element, params) {
            var today = new Date().setHours(0, 0, 0, 0);
            return Date.parse(value) >= today;
        });

        $.validator.unobtrusive.adapters.addBool("notinpast");

        $.validator.addMethod("notlaterthan", function (value, element, params) {
            return Date.parse(value) <= Date.parse(params);
        });

        $.validator.unobtrusive.adapters.add("notlaterthan", ["date"], function (options) {
            options.rules["notlaterthan"] = options.params.date;
            options.messages["notlaterthan"] = options.message;
        });
    };

    return {
        calculateFormattedDate : calculateFormattedDate,
        initialize: initialize,
        setupValidators: setupValidators
    };
} ();

(function ($) {
    Campervibe.Shared.Datepicker.setupValidators();
} (jQuery));

$(document).ready(function () {
    Campervibe.Shared.Datepicker.initialize();
});