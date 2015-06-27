function setUpHtmlFixture() {
    jasmine.getFixtures().set(
        '<div id="dpContainer" class="date-picker-container"> ' +
            '<select class="date-picker-day">' +
                '<option value="" >Day</option>' +
                '<option value="1" >1</option>' +
                '<option value="2" >2</option>' +
            '</select>' +
            '<select class="date-picker-month">' +
                '<option value="" >Month</option>' +
                '<option value="1" >January</option>' +
                '<option value="2" >February</option>' +
            '</select>' +
            '<select class="date-picker-year">' +
                '<option value="" >Year</option>' +
                '<option value="2015" >2015</option>' +
                '<option value="2016" >2016</option>' +
            '</select>' +
            '<input type="hidden" class="date-picker-validator" value="" />' +
        '</div>');
};

function cleardownHtmlFixture() {
    jasmine.getFixtures().set("");
};

describe("Shared.Datepicker.calculateFormattedDate", function () {
    it("should be able to calculate a date when all elements supplied", function () {
        var formattedDate = Campervibe.Shared.Datepicker.calculateFormattedDate(10, 5, 2015);
        expect(formattedDate).toEqual("2015-05-10");
    });

    it("should be empty if any elements are not supplied", function () {
        var formattedDate = Campervibe.Shared.Datepicker.calculateFormattedDate(null, 5, 2015);
        expect(formattedDate).toEqual("");
    });

    it("should be empty if all elements are not supplied", function () {
        var formattedDate = Campervibe.Shared.Datepicker.calculateFormattedDate(null, null, null);
        expect(formattedDate).toEqual("");
    });
});

describe(".date-picker-container select change event", function () {
    beforeEach(function () {
        setUpHtmlFixture();
        Campervibe.Shared.Datepicker.initialize();
    });

    afterEach(function () {
        cleardownHtmlFixture();
    });

    it("should not set validator value if only one field is set", function () {
        $(".date-picker-day").val("1");
        $(".date-picker-day").trigger('change');
        expect($(".date-picker-validator").val()).toEqual("");
    });

    it("should set validator value if  all fields are set", function () {
        $(".date-picker-day").val("1");
        $(".date-picker-day").trigger('change');
        $(".date-picker-month").val("1");
        $(".date-picker-month").trigger('change');
        $(".date-picker-year").val("2015");
        $(".date-picker-year").trigger('change');
        expect($(".date-picker-validator").val()).toEqual("2015-01-01");
    });

    it("should reset validator value if only all fields are set then one is unset", function () {
        $(".date-picker-day").val("1");
        $(".date-picker-day").trigger('change');
        $(".date-picker-month").val("1");
        $(".date-picker-month").trigger('change');
        $(".date-picker-year").val("2015");
        $(".date-picker-year").trigger('change');
        $(".date-picker-day").val("");
        $(".date-picker-day").trigger('change');
        expect($(".date-picker-validator").val()).toEqual("");
    });
});