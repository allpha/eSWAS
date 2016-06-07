var BusyIndicator = function () {

    var busyIndicatorDivName = '';

    var initBusyIndicator = function (divName) {
        busyIndicatorDivName = divName;
    }

    var setBusy = function () {
        App.blockUI({
            target: busyIndicatorDivName,
            animate: true
        });
    }

    var unsetBusy = function () {
        App.unblockUI(busyIndicatorDivName);
    }

    return {
        init: function (divName) {
            initBusyIndicator(divName);
        },
        setBusy: function () {
            setBusy();
        },
        unsetBusy: function () {
            unsetBusy();
        }
    }
}();

//jQuery(document).ready(function () {
//    FormValidationMd.init();
//});