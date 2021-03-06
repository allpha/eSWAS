﻿function FilterSolidWasteAct() {


    $('#cencelSolidWasteAct').click(function () {
        $('#filterDiv').hide();

        $fromDate = null;
        $endDate = null;
        $landfillDataSource = [];
        $recordNumber = null;
        $wasteTypeDataSource = [];
        $customerDataSource = [];
        $loadAllLandfill = true;
        $loadAllWasteType = true;
        $loadAllCustomer = true;

        loadPageCount();
    });

    $('#filterSolidWasteAct').click(function () {

        var actNumber = $('#solidWasteActNumber').val();
        var fromDate = document.getElementById("ActDateFrom").value; //$('#ActDateFrom').val();
        var ActDateTo = document.getElementById("ActDateTo").value; //$('#ActDateTo').val();
        var landfillItemSource = [];
        if ($('#allLandfillSelected').is(':checked')) {
            var landfillData = $('#landfillSearchCombo').find("option");
            if (landfillData == null)
                landfillData = [];
            for (var i = 0; i < landfillData.length; i++) {
                landfillItemSource.push(landfillData[i].value);
            }
        }
        else
            landfillItemSource = $('#landfillSearchCombo').val();

        if (landfillItemSource == null)
            landfillItemSource = [0];

        $recordNumber = document.getElementById("solidWasteActNumber").value;
        //$fromDate = new Date(fromDate);
        //$endDate = new Date(ActDateTo);
        $fromDate = fromDate;
        $endDate = ActDateTo;
        $landfillDataSource = landfillItemSource;
        $wasteTypeDataSource = $('#wasteTypeSearchCombo').val();
        $customerDataSource = $('#customerSearchCombo').val();
        $loadAllWasteType = $('#allWasteTypeSelected').is(':checked');
        $loadAllCustomer = $('#allCustomerSelected').is(':checked');
        $loadAllLandfill = false;
        loadPageCount();

    });

}

function initTableItemSource(itemSource) {

    $("#itemSource").find("tr:not(:first)").remove();

    for (var i = 0; i < itemSource.length; i++) {
        var row = '<tr>' +
                       '<td>' + itemSource[i].ActId + '</td>' +
                       '<td>' + moment(itemSource[i].ActDate).format('MM/DD/YYYY') + '</td>' +
                       '<td>' + itemSource[i].LandfillName + '</td>' +
                       '<td>' + itemSource[i].CustomerName + '</td>' +
                        '<td>' + itemSource[i].CustomerCode + '</td>' +
                        '<td>' + itemSource[i].CustomerInfo + '</td>' +
                       '<td style="text-align:right">' + itemSource[i].Quantity.toFixed(2) + ' ტ.</td>' +
                       '<td style="text-align:right">' + itemSource[i].Price.toFixed(2) + ' ₾</td>' +
                       '<td style="text-align:right">' + itemSource[i].PaidAmount.toFixed(2) + ' ₾</td>' +
                       '<td style="text-align:right">' + itemSource[i].DebtAmount.toFixed(2) + ' ₾</td>' +
                       '<td >';
        if ($("#hasEdit").val() == "true")
            row += '<a href="javascript:add(' + itemSource[i].ActId + ');" class="btn btn-xs default"><i class="fa fa-edit"></i></a>';
        if ($("#hasEdit").val() == "true")
            row += '<a href="javascript:info(' + itemSource[i].ActId + ');" class="btn btn-xs default"><i class="icon-tag"></i></a>';
        if ($("#hasPrint").val() == "true")
            row += '<a target="_blank" href="/SolidWasteActPrint/Index/' + itemSource[i].ActId + '" class="btn btn-xs default"><i class="fa fa-print"></i></a>';
                            
        row+= '</td>' + '</tr>';
        $('#itemSource tr:last').after(row);
    }

}

function add(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/Payment/Edit/" + id);
    $('#ajax-model-dialog-editor').click();
}

function info(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/SolidWasteAct/Detail/" + id);
    $('#ajax-model-dialog-editor').click();
}

function loadRegoinItemSource() {
    $.ajax({
        url: "/Payment/LoadFilterRegions",
        type: "POST",
        dataType: "json",
        data: {

        },
        success: function (data) {
            var select2Name = "#regionSearchCombo";
            $(select2Name).empty();
            for (var i = 0; i < data.length; i++) {
                $(select2Name).append('<option value="' + data[i].Id + '">' + data[i].Name + '</option>');
            }
        },
        error: function (request, status, error) {
        }
    });

}


function loadLandfillSource(selectAll) {
    $.ajax({
        url: "/Payment/LoadFilterLandfills",
        type: "POST",
        dataType: "json",
        data: {
            selectAll: selectAll,
            regionItemSource: $('#regionSearchCombo').val(),
        },
        success: function (data) {
            var select2Name = "#landfillSearchCombo";
            var selectedValues = $(select2Name).val();
            var existingSelectedValues = []
            if (selectedValues == null)
                selectedValues = [];

            $(select2Name).empty();

            for (var i = 0; i < data.length; i++) {

                var exist = false;
                for (var j = 0; j < selectedValues.length; j++)
                    if (selectedValues[j] == data[i].Id) {
                        exist = true;
                    }

                if (exist) {
                    existingSelectedValues.push(data[i].Id);
                }

                $(select2Name).append('<option value="' + data[i].Id + '">' + data[i].Name + '</option>');
            }
            $(select2Name).val(existingSelectedValues).trigger("change");

        },
        error: function (request, status, error) {
        }
    });

}

function loadWasteTypeItemSource() {
    $.ajax({
        url: "/Payment/LoadFilterWasteType",
        type: "POST",
        dataType: "json",
        data: {

        },
        success: function (data) {
            var select2Name = "#wasteTypeSearchCombo";
            $(select2Name).empty();
            for (var i = 0; i < data.length; i++) {
                $(select2Name).append('<option value="' + data[i].Id + '">' + data[i].Name + '</option>');
            }

        },
        error: function (request, status, error) {
        }
    });

}

function loadCustomerItemSource() {
    $.ajax({
        url: "/Payment/LoadFilterCustomer",
        type: "POST",
        dataType: "json",
        data: {

        },
        success: function (data) {
            var select2Name = "#customerSearchCombo";
            $(select2Name).empty();
            for (var i = 0; i < data.length; i++) {
                $(select2Name).append('<optgroup label="' + data[i].TypeDescription + '">');
                for (var j = 0; j < data[i].ChildItemSource.length; j++) {
                    $(select2Name).append('<option value="' + data[i].ChildItemSource[j].Id + '">&nbsp&nbsp&nbsp' + data[i].ChildItemSource[j].Name + '</option>');
                }
                $(select2Name).append('</optgroup>');
            }

        },
        error: function (request, status, error) {
        }
    });
}

function InitSearchCombos() {
    $.fn.select2.defaults.set("theme", "bootstrap");

    var comboPlaceholder = 'არჩეულია ყველა ჩანაწერი';
    var comboPlaceholderToSelect = 'გთხოვთ აირჩიოთ ჩანაწერი';

    $("#regionSearchCombo").select2({
        placeholder: comboPlaceholder,
        allowClear: true,
        width: null
    });

    $("#wasteTypeSearchCombo").select2({
        placeholder: comboPlaceholder,
        allowClear: true,
        width: null
    });

    $("#customerSearchCombo").select2({
        placeholder: comboPlaceholder,
        allowClear: true,
        width: null
    });

    $("#landfillSearchCombo").select2({
        placeholder: comboPlaceholder,
        allowClear: true,
        width: null
    });




    $("#allLandfillSelected").on("click", function () {
        $('#landfillSearchCombo').prop("disabled", this.checked);
        if (this.checked) {
            $('#landfillSearchCombo').select2('val', '');
            $("#landfillSearchCombo").select2({ placeholder: comboPlaceholder });
        }
        else {
            $("#landfillSearchCombo").select2({ placeholder: comboPlaceholderToSelect });
        }
    });


    $("#allRegionSelected").on("click", function () {
        $('#regionSearchCombo').prop("disabled", this.checked);
        if (this.checked) {
            $('#regionSearchCombo').select2('val', '');
            $("#regionSearchCombo").select2({ placeholder: comboPlaceholder });
            loadLandfillSource(true);
        }
        else {
            $("#regionSearchCombo").select2({ placeholder: comboPlaceholderToSelect });
            loadLandfillSource(false);
        }
    });

    $("#regionSearchCombo").on("change", function (e) {
        if (!$('#allRegionSelected').is(':checked')) {
            loadLandfillSource(false);
        }
    });

    $("#allWasteTypeSelected").on("click", function () {
        $('#wasteTypeSearchCombo').prop("disabled", this.checked);
        if (this.checked) {
            $('#wasteTypeSearchCombo').select2('val', '');
            $("#wasteTypeSearchCombo").select2({ placeholder: comboPlaceholder });
        }
        else {
            $("#wasteTypeSearchCombo").select2({ placeholder: comboPlaceholderToSelect });
        }
    });

    $("#allCustomerSelected").on("click", function () {
        $('#customerSearchCombo').prop("disabled", this.checked);
        if (this.checked) {
            $('#customerSearchCombo').select2('val', '');
            $("#customerSearchCombo").select2({ placeholder: comboPlaceholder });
        }
        else {
            $("#customerSearchCombo").select2({ placeholder: comboPlaceholderToSelect });
        }
    });
}

function InitDate() {
    $('.date-picker').datepicker({
        rtl: App.isRTL(),
        orientation: "left",
        autoclose: true
    });
}

function InitFilter() {
    $('#filterButton').click(function () {
        if ($('#filterDiv').is(":hidden"))
            $('#filterDiv').show();
        else
            $('#filterDiv').hide();
    });
}


var $fromDate = null;
var $endDate = null;
var $landfillDataSource = [];
var $recordNumber = null;
var $wasteTypeDataSource = [];
var $customerDataSource = [];
var $loadAllLandfill = true;
var $loadAllWasteType = true;
var $loadAllCustomer = true;

function loadPageCount() {

    editorName = '#viewBody'
    App.blockUI({
        target: editorName,
        animate: true
    });

    InitPages(0);

    $.ajax({
        url: "/Payment/LoadPageCount",
        type: "POST",
        dataType: "json",
        data: {
            id: $recordNumber,
            fromDate: $fromDate,
            endDate: $endDate,
            landFillIdSource: $landfillDataSource,
            wasteTypeIdSource: $wasteTypeDataSource,
            customerIdSource: $customerDataSource,
            loadAllWasteType: $loadAllWasteType,
            loadAllCustomer: $loadAllCustomer,
            loadAllLandfill: $loadAllLandfill,
        },
        success: function (data) {
            InitPages(data.pageCount)
            loadData(1);
            App.unblockUI(editorName);
        },
        error: function (request, status, error) {
            alert('მოხდა სერვერსული შეცდომა');
            App.unblockUI(editorName);
        }
    });
}

function loadData(pageNum) {

    editorName = '#viewBody'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $.ajax({
        url: "/Payment/Load",
        type: "POST",
        dataType: "json",
        data: {
            id: $recordNumber,
            fromDate: $fromDate,
            endDate: $endDate,
            landFillIdSource: $landfillDataSource,
            wasteTypeIdSource: $wasteTypeDataSource,
            customerIdSource: $customerDataSource,
            loadAllWasteType: $loadAllWasteType,
            loadAllCustomer: $loadAllCustomer,
            loadAllLandfill: $loadAllLandfill,
            pageNumber: pageNum,
        },
        success: function (data) {
            initTableItemSource(data);
            App.unblockUI(editorName);
        },
        error: function (request, status, error) {
            alert('მოხდა სერვერსული შეცდომა');
            App.unblockUI(editorName);
        }
    });

}

function InitPages(count) {
    $('#tablePages').bootpag({ total: count });
}

function exportToExcel() {

    $('#paymentReport').click(function () {
        editorName = '#viewBody'
        App.blockUI({
            target: editorName,
            animate: true
        });

        $.ajax({
            url: "/Reporting/PaymentReport",
            type: "POST",
            dataType: "json",
            data: {
                id: $recordNumber,
                fromDate: $fromDate,
                endDate: $endDate,
                landFillIdSource: $landfillDataSource,
                wasteTypeIdSource: $wasteTypeDataSource,
                customerIdSource: $customerDataSource,
                loadAllWasteType: $loadAllWasteType,
                loadAllCustomer: $loadAllCustomer,
                loadAllLandfill: $loadAllLandfill,
            },
            success: function (data) {
                App.unblockUI(editorName);

                window.open('/Reporting/DownloadFile?fileName=' + data.fileName, '_blank');
            },
            error: function (request, status, error) {
                alert('მოხდა სერვერსული შეცდომა');
                App.unblockUI(editorName);
            }
        });
    });
}

jQuery(document).ready(function () {
    InitDate();
    InitFilter();
    InitSearchCombos();

    $('#tablePages').bootpag({
        paginationClass: 'pagination pagination-sm',
        next: '<i class="fa fa-angle-right"></i>',
        prev: '<i class="fa fa-angle-left"></i>',
        total: 0,
        page: 1,
        maxVisible: 6
    }).on('page', function (event, num) {
        loadData(num)
    });

    loadPageCount();
    loadRegoinItemSource();
    loadLandfillSource(true);
    loadWasteTypeItemSource();
    loadCustomerItemSource();
    FilterSolidWasteAct();
    exportToExcel();
});
