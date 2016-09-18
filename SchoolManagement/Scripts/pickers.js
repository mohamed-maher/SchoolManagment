var Pickers = function () {

    var handleDatePickers = function () {

        if (jQuery().datepicker) {
            $('.date-picker').datepicker({
                rtl: Metronic.isRTL(),
                orientation: "left",
                autoclose: true
            });
            //$('body').removeClass("modal-open"); // fix bug when inline picker is used in modal
        }
    }

    var handleDatetimePicker = function () {

        if (!jQuery().datetimepicker) {
            return;
        }

        $(".datetime-picker").datetimepicker({
            isRTL: Metronic.isRTL(),
            format: "mm/dd/yyyy hh:ii",
            pickerPosition: (Metronic.isRTL() ? "bottom-right" : "bottom-left")
        });

        $('body').removeClass("modal-open"); // fix bug when inline picker is used in modal
    }

    return {
        //main function to initiate the module
        init: function () {
            handleDatePickers();
            handleDatetimePicker();
        }
    };

}();