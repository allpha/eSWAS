function loadSolidWasteActs() {

    editorName = '#viewBody'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $.ajax({
        url: "/SolidWasteAct/LoadSolidWasteActs",
        type: "POST",
        dataType: "json",
        data: {

        },
        success: function (data) {
            initTableItemSource(data);
            App.unblockUI(editorName);
        },
        error: function (request, status, error) {
            App.unblockUI(editorName);
        }
    });
}

function FilterSolidWasteAct() {


    $('#cencelSolidWasteAct').click(function () {
        $('#filterDiv').hide();
        loadSolidWasteActs()
    });

    $('#filterSolidWasteAct').click(function () {

        editorName = '#viewBody'
        App.blockUI({
            target: editorName,
            animate: true
        });

        var actNumber = $('#solidWasteActNumber').val();
        var fromDate = document.getElementById("ActDateFrom").value; //$('#ActDateFrom').val();
        var ActDateTo = document.getElementById("ActDateTo").value; //$('#ActDateTo').val();

        $.ajax({
            url: "/SolidWasteAct/FilterSolidWasteAct",
            type: "POST",
            dataType: "json",
            data: {
                id: actNumber,
                fromDate: fromDate,
                endDate: ActDateTo,
                landFillIdSource: $('#regionSearchCombo').val(),
                wasteTypeIdSource: $('#wasteTypeSearchCombo').val(),
                customerIdSource: $('#customerSearchCombo').val(),
                isAllLandfill: $('#allRegionSelected').is(':checked'),
                isAllWasteType: $('#allWasteTypeSelected').is(':checked'),
                isAllCustomer: $('#allCustomerSelected').is(':checked')
            },
            success: function (data) {
                initTableItemSource(data);
                App.unblockUI(editorName);

            },
            error: function (request, status, error) {
                alert('shecdomaa');
                App.unblockUI(editorName);
            }
        });
    });

}


function initTableItemSource(itemSource) {

    $("#itemSource").find("tr:not(:first)").remove();

    for (var i = 0; i < itemSource.length; i++) {
        var row = '<tr>' +
                       '<td>' + itemSource[i].Id + '</td>' +
                       '<td>' + moment(itemSource[i].ActDate).format('DD/MM/YYYY') + '</td>' +
                       '<td>' + itemSource[i].LandfillName + '</td>' +
                       '<td>' + itemSource[i].Receiver + '</td>' +
                       '<td>' + itemSource[i].Customer + '</td>' +
                       '<td>' + itemSource[i].Quantity + '</td>' +
                       '<td>' + itemSource[i].Price + '</td>' +
                       '<td>' +
                            '<a href="javascript:edit(' + itemSource[i].Id + ');" class="btn btn-xs default"><i class="fa fa-edit"></i></a>' +
                            '<a href="javascript:remove(' + itemSource[i].Id + ');" class="btn btn-xs default"><i class="fa fa-remove"></i></a>' +
                       '</td>' +
                  '</tr>';


        $('#itemSource tr:last').after(row);
    }
}

function create() {
    $('#ajax-model-dialog-editor').attr("data-url", "/SolidWasteAct/Create");
    $('#ajax-model-dialog-editor').click();
}

function edit(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/SolidWasteAct/Edit/" + id);
    $('#ajax-model-dialog-editor').click();
}

function remove(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/SolidWasteAct/Delete/" + id);
    $('#ajax-model-dialog-editor').click();
}

function loadRegoinItemSource() {
    $.ajax({
        url: "/SolidWasteAct/LoadFilterRegions",
        type: "POST",
        dataType: "json",
        data: {

        },
        success: function (data) {
            var select2Name = "#regionSearchCombo";
            $(select2Name).empty();
            for (var i = 0; i < data.length; i++) {
                $(select2Name).append('<optgroup label="' + data[i].Name + '">');
                for (var j = 0; j < data[i].LandfillItemSource.length; j++) {
                    $(select2Name).append('<option value="' + data[i].LandfillItemSource[j].Id + '">&nbsp&nbsp&nbsp' + data[i].LandfillItemSource[j].Name + '</option>');
                }
                $(select2Name).append('</optgroup>');
            }
        },
        error: function (request, status, error) {
        }
    });

}

function loadWasteTypeItemSource() {
    $.ajax({
        url: "/SolidWasteAct/LoadFilterWasteType",
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
        url: "/SolidWasteAct/LoadFilterCustomer",
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



    $("#allRegionSelected").on("click", function () {
        $('#regionSearchCombo').prop("disabled", this.checked);
        if (this.checked) {
            $('#regionSearchCombo').select2('val', '');
            $("#regionSearchCombo").select2({ placeholder: comboPlaceholder });
        }
        else {
            $("#regionSearchCombo").select2({ placeholder: comboPlaceholderToSelect });
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


jQuery(document).ready(function () {
    InitDate();
    InitFilter();
    InitSearchCombos();
    loadSolidWasteActs();
    loadRegoinItemSource();
    loadWasteTypeItemSource();
    loadCustomerItemSource();
    FilterSolidWasteAct();
});
