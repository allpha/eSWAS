var $itemSource = [];
var $itemIncriment = 0;
var $editId = 0;
var $editorMode = 'ADD';
var $selectedRow = null;
var $detailEditorName = '#detailEditorDialog'

function FindItem(wasteTypeId) {
    for (var i = 0; i < $itemSource.length; i++) {

        if ($itemSource[i].WasteTypeId == wasteTypeId) return $itemSource[i];
    }
}

function FindItemForEdit(wasteTypeId, editId) {
    for (var i = 0; i < $itemSource.length; i++)
        if ($itemSource[i].WasteTypeId == wasteTypeId && $itemSource[i].Id != editId) {
            return $itemSource[i];
        }
}

function UpdateItemSource(payDate, amount) {
    for (var i = 0; i < $itemSource.length; i++) {
        if ($itemSource[i].Id == $editId) {
            $itemSource[i].PayDate = payDate;
            $itemSource[i].Amount = amount;
        }
    }
}

function EditButtonClickHandler(editId) {
    $editId = editId;
    $editorMode = 'EDIT';
    $('#wasteEditorErrorText').hide();
}

$('#addPayment').click(function () {
    $editorMode = 'ADD';
    $("#PayDate").val('');
    $("#Amount").val(0)
});


function EditButtonClickUpdateQuantity() {

    $selectedRow = $(this).closest("tr");
    var payDate = $selectedRow.find(".trPayDay").text();
    var amount = $selectedRow.find(".trAmount").text();
    $("#PayDate").val(payDate);
    $("#Amount").val(amount)
}

function RemoveFromItemSource(removeId) {
    for (var i = 0; i < $itemSource.length; i++) {
        if ($itemSource[i].Id == removeId) {
            $itemSource.splice(i, 1);
            return;
        }
    }
}

function RemoveButtonClickHandler() {
    $(this).closest("tr").remove();
    updataTotalSumValue();
}

function updateErrorEditor(source, text) {
    source.text(text);
    source.append(' <button class="close" data-close="alert"></button> ');
    source.show();
}

function removeTableData() {
    $("#SolidWastActDetailTable").find("tr:not(:first)").remove();
    $itemSource = [];
    updataTotalSumValue();
}


function updateWasteEditorError(text) {
    var editorSource = $('#wasteEditorErrorText');
    editorSource.text(text);
    editorSource.append(' <button class="close" data-close="alert"></button> ');
    editorSource.show();
}


function updataTotalSumValue() {
    var totalSum = Number(0);

    for (var i = 0; i < $itemSource.length; i++) {
        totalSum += Number($itemSource[i].Amount);
    }

    var totalPaymentAmount = Number($('#TotalPayAmount').text());
    $('#PaidAmount').text(totalSum.toFixed(2));
    $('#RemainAmount').text((totalPaymentAmount - totalSum).toFixed(2));
}

var FormValidationMd = function () {
    var handleValidation = function () {

        var form1 = $('#addPaymentForm');
        var errorEditor = $('.alert-danger', form1);

        form1.validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                PayDate: {
                    required: true
                },
                Amount: {
                    required: true,
                    number: true
                },
            },

            invalidHandler: function (event, validator) { //display error alert on form submit
                updateErrorEditor(errorEditor, 'მონაცემები არ არის ვალიდური');
            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                .closest('.form-group').addClass('has-error'); // set error class to the control group
                //.closest('.form-control').addClass('has-error').css('color','red'); // set error class to the control group
            },

            unhighlight: function (element) { // revert the change done by hightlight
                $(element)
                    .closest('.form-group').removeClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label
                    .closest('.form-group').removeClass('has-error'); // set success class to the control group
            },

            submitHandler: function (form) {
                savePaymentData();
                errorEditor.hide();
            }
        });
    }

    return {
        //main function to initiate the module
        init: function () {
            handleValidation();
        }
    };
}();

function getActReviewDetail() {
    var text = '';
    var totalAmount = 0;
    for (var i = 0; i < $itemSource.length; i++) {

        text += '<tr>' +
                        '<td>' + $itemSource[i].WasteTypeName + '</td>' +
                        '<td style="text-align:right">' + $itemSource[i].Quantity + '</td>' +
                        '<td style="text-align:right">' + $itemSource[i].UnitPrice + '</td>' +
                        '<td style="text-align:right">' + $itemSource[i].Amount + '</td>' +
                      '</tr>';

        totalAmount += $itemSource[i].Amount;
    }

    return { htmlText: text, TotalAmount: totalAmount };
}

function savePaymentData() {
    if ($editorMode == 'ADD') {

        $itemIncriment++;
        editButton = '<td><button type="button" id="edit' + $itemIncriment + '"' +
                      'onClick="EditButtonClickHandler(' + $itemIncriment + ')"  ' +
                     'class="btn btn-xs default fa fa-edit" data-toggle="modal" data-target="#detailEditorDialog" ></button>';

        removeButton = '<button type="button" id="remove' + $itemIncriment + '"' +
                        'onClick="RemoveFromItemSource(' + $itemIncriment + ')"  ' +
                       ' class="btn btn-xs default fa fa-remove"></button></td>';

        var hidden = '<input type="hidden" id="hidden' + $itemIncriment + '" value="Norway">';

        var amount = Number($("#Amount").val())

        var row = '<tr><td class="trPayDay">' + $("#PayDate").val() +
                   '</td> <td class="trAmount" style="text-align: right;">' + amount.toFixed(2) +
                    '</td>' + editButton + removeButton + ' </tr>' + hidden;

        $('#SolidWastActDetailTable tr:last').after(row);

        $('#edit' + $itemIncriment).unbind("click");
        $('#edit' + $itemIncriment).bind("click", EditButtonClickUpdateQuantity);

        $('#remove' + $itemIncriment).unbind("click");
        $('#remove' + $itemIncriment).bind("click", RemoveButtonClickHandler);

        $itemSource.push({
            Id: $itemIncriment,
            PayDate: $("#PayDate").val(),
            Amount: $("#Amount").val()
        });

        updataTotalSumValue();
        $('#btWasteClose').click();
    }
    else {
        var updateAmount = Number($("#Amount").val())
        $selectedRow.find(".trPayDay").html($("#PayDate").val());
        $selectedRow.find(".trAmount").html(updateAmount.toFixed(2));

        UpdateItemSource($("#PayDate").val(), Number($("#Amount").val()));
        updataTotalSumValue();
        $('#btWasteClose').click();
    }

}


function AddDetailTableDataSource(dataSourse) {

    for (var i = 0; i < dataSourse.length; i++) {

        $itemIncriment++;
        var data = dataSourse[i];

        editButton = '<td><button type="button" id="edit' + $itemIncriment + '"' +
                      'onClick="EditButtonClickHandler(' + $itemIncriment + ')"  ' +
                     'class="btn btn-xs default fa fa-edit" data-toggle="modal" data-target="#detailEditorDialog" ></button>';

        removeButton = '<button type="button" id="remove' + $itemIncriment + '"' +
                        'onClick="RemoveFromItemSource(' + $itemIncriment + ')"  ' +
                       ' class="btn btn-xs default fa fa-remove"></button></td>';

        var convertedDate = moment(data.PayDate).format('MM/DD/YYYY')

        var hidden = '<input type="hidden" id="hidden' + $itemIncriment + '" value="Norway">'
        var row = '<tr><td class="trPayDay">' + convertedDate +
                   '</td> <td class="trAmount" style="text-align: right;">' + data.Amount.toFixed(2) +
                    '</td>' + editButton + removeButton + ' </tr>' + hidden;

        $('#SolidWastActDetailTable tr:last').after(row);

        $('#edit' + $itemIncriment).unbind("click");
        $('#edit' + $itemIncriment).bind("click", EditButtonClickUpdateQuantity);

        $('#remove' + $itemIncriment).unbind("click");
        $('#remove' + $itemIncriment).bind("click", RemoveButtonClickHandler);

        $itemSource.push({
            Id: $itemIncriment,
            PayDate: convertedDate,
            Amount: data.Amount.toFixed(2)
        });

    }

    updataTotalSumValue();
}

function updateErrorEditorForSave(text) {

    var form1 = $('#registrationForm');
    var source = $('.alert-danger', form1);


    source.text(text);
    source.append(' <button class="close" data-close="alert"></button> ');
    source.show();
}


function savePayment() {
    BusyIndicator.setBusy();

    var sentData = []
    if ($itemSource != null) {
        for (var i = 0; i < $itemSource.length; i++) {
            sentData.push({
                PayDate: $itemSource[i].PayDate,
                Amount: $itemSource[i].Amount,
            });
        }

    }

    $.ajax({
        url: "/Payment/SavePayment",
        type: "POST",
        dataType: "json",
        data: {
            id: $("#Id").val(),
            payments: sentData
        },
        success: function (data) {
            location.href = '/Payment/Index';
            BusyIndicator.unsetBusy();
        },
        error: function (request, status, error) {
            updateErrorEditorForSave(request.responseText)
            BusyIndicator.unsetBusy();
        }
    });

}

function loadPayment() {
    BusyIndicator.setBusy();

    $.ajax({
        url: "/Payment/GetPayment",
        type: "POST",
        dataType: "json",
        data: {
            id: $("#Id").val(),
        },
        success: function (data) {

            $("#ActId").text(data.ActId);
            $("#ActDate").text(moment(data.ActDate).format('MM/DD/YYYY'));
            $("#LandfillName").text(data.LandfillName);
            $("#CustomerCode").text(data.CustomerCode);
            $("#CustomerName").text(data.CustomerName);
            $("#CustomerInfo").text(data.CustomerInfo);
            $("#TotalPayAmount").text(data.Price.toFixed(2));
            
            AddDetailTableDataSource(data.HistoryItemSource)

            BusyIndicator.unsetBusy();
        },
        error: function (request, status, error) {
            updateErrorEditorForSave(request.responseText)
            BusyIndicator.unsetBusy();
        }
    });

}

jQuery(document).ready(function () {
    BusyIndicator.init("#editorDialog");

    if (jQuery().datepicker) {
        $('.date-picker').datepicker({
            rtl: App.isRTL(),
            orientation: "left",
            autoclose: true
        });
    }

    $('#tbSavePaymentHistory').click(function () {
        $('#addPaymentForm').submit();
    })

    $('#btSave').click(function () {
        savePayment();
    });

    FormValidationMd.init();

    loadPayment();
});
