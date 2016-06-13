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

function UpdateItemSource(wasteTypeId, wasteTypeName, quantity, unitPrice, amount) {
    for (var i = 0; i < $itemSource.length; i++) {
        if ($itemSource[i].WasteTypeId == wasteTypeId) {
            $itemSource[i].WasteTypeId = wasteTypeId;
            $itemSource[i].WasteTypeName = wasteTypeName;
            $itemSource[i].Quantity = quantity;
            $itemSource[i].UnitPrice = unitPrice;
            $itemSource[i].Amount = amount;
        }
    }
}

function EditButtonClickHandler(wasteTypeID) {
    var $wasteTypeItemSource = $("#WasteTypeId").select2();

    $wasteTypeItemSource.val(wasteTypeID).trigger("change");
    $("#WasteTypeId").select2({
        width: null
    });

    $editId = FindItem(wasteTypeID).Id;
    $editorMode = 'EDIT';
    $('#wasteEditorErrorText').hide();
}

function EditButtonClickUpdateQuantity() {

    $selectedRow = $(this).closest("tr");
    var quantity = $selectedRow.find(".trQuantity").text();

    $('#radioQuantityTon').prop('checked', true);
    $('#radioQuantityM3').prop('checked', false);
    $('#M3Quantity').val('0');
    $('#TonQuantity').val(quantity);
}

function RemoveFromItemSource(wasteTypeId) {
    for (var i = 0; i < $itemSource.length; i++) {
        if ($itemSource[i].WasteTypeId == wasteTypeId) {
            $itemSource.splice(i, 1);
            return;
        }
    }
}

function RemoveButtonClickHandler() {
    $(this).closest("tr").remove();
    updataTotalSumValue();
}

function saveSolidWasteAct() {
    editorName = '#editorDialog'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $.ajax({
        url: "/SolidWasteActJurnal/Edit",
        type: "POST",
        dataType: "json",
        data: {
            id: document.getElementById("Id").value,
            actDate: document.getElementById("ActDate").value,
            landfillId: document.getElementById("LandfillId").value,
            receiverName: document.getElementById("ReceiverName").value,
            receiverLastName: document.getElementById("ReceiverLastName").value,
            positionName: document.getElementById("PositionName").value,
            type: document.getElementById("Type").value,
            customerName: document.getElementById("CustomerName").value,
            customerCode: document.getElementById("CustomerCode").value,
            customerContactInfo: document.getElementById("CustomerContactInfo").value,
            representativeName: document.getElementById("RepresentativeName").value,
            transporterCarNumber: document.getElementById("TransporterCarNumber").value,
            transporterCarModel: document.getElementById("TransporterCarModel").value,
            transporterDriverInfo: document.getElementById("TransporterDriverInfo").value,
            remark: document.getElementById("Remark").value,
            solidWasteActDetails: $itemSource
        },
        success: function (data) {
            App.unblockUI(editorName);
            $('#btEditDiv').hide();
            $('#btSaveDiv').hide();
            $('#btNexDiv').hide();
            $('#confirmInformation').hide();
            $('#print').show();
            $("#printData").attr("href", "/SolidWasteActPrint/Index/" + data);
            $('#isSaved').val('saved')
        },
        error: function (textStatus, errorThrown) {
            updateErrorEditor($('#confirmationErrorEditor'), textStatus)
            App.unblockUI(editorName);
        }
    });
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


$('#addWaste').click(function () {
    $editorMode = 'ADD';
    $('#radioQuantityTon').prop('checked', true);
    $('#radioQuantityM3').prop('checked', false);
    $('#M3Quantity').val('0');
    $('#TonQuantity').val('0');
    $('#wasteEditorErrorText').hide();
});

function updateWasteEditorError(text) {
    var editorSource = $('#wasteEditorErrorText');
    editorSource.text(text);
    editorSource.append(' <button class="close" data-close="alert"></button> ');
    editorSource.show();
}


function updataTotalSumValue() {
    totalSum = 0;

    for (var i = 0; i < $itemSource.length; i++) {
        totalSum += $itemSource[i].Amount;
    }

    $('#totalSum').text(Number(totalSum).toFixed(2) + ' ₾');
}

$('#btWasteSave').click(function () {

    var $wasteTypeId = document.getElementById("WasteTypeId").value;

    if (($editorMode == 'ADD' && FindItem($wasteTypeId) != null) || ($editorMode == 'EDIT' && FindItemForEdit($wasteTypeId, $editId) != null))
        updateWasteEditorError('ასეთი ჩანაწერი არსებობს')
    else {

        App.blockUI({
            target: $detailEditorName,
            animate: true
        });

        var quantity = 0;
        var isCubeChecked = false;

        if ($("input[name='radioQuantity']:checked").val() == -1) {
            quantity = $('#M3Quantity').val();
            isCubeChecked = true;
        }
        else {
            quantity = $('#TonQuantity').val();
            isCubeChecked = false;
        }

        $.ajax({
            url: "/SolidWasteActJurnal/CalculateWasteAmount",
            type: "POST",
            dataType: "json",
            data: { customerType: document.getElementById("Type").value, wasteTypeId: document.getElementById("WasteTypeId").value, quantity: quantity, isInQubeMeter: isCubeChecked },
            success: function (data) {

                if ($editorMode == 'ADD') {

                    $itemIncriment++;

                    editButton = '<td><button type="button" id="edit' + $itemIncriment + '"' +
                                  'onClick="EditButtonClickHandler(' + data.WasteTypeId + ')"  ' +
                                 'class="btn btn-xs default fa fa-edit" data-toggle="modal" data-target="#detailEditorDialog" ></button>'

                    removeButton = '<button type="button" id="remove' + $itemIncriment + '"' +
                                    'onClick="RemoveFromItemSource(' + data.WasteTypeId + ')"  ' +
                                   ' class="btn btn-xs default fa fa-remove"></button></td>'

                    var hidden = '<input type="hidden" id="hidden' + data.WasteTypeId + '" value="Norway">'
                    var row = '<tr><td class="trWasteTypeName">' + data.WasteTypeName +
                               '</td> <td class="trQuantity" style="text-align: right;">' + Number(data.Quantity).toFixed(2) +
                               '</td> <td class="trUnitPrice" style="text-align: right;">' + Number(data.UnitPrice).toFixed(2) +
                               '</td><td class="trAmount" style="text-align: right;">' + Number(data.Amount).toFixed(2) +
                                '</td>' + editButton + removeButton + ' </tr>' + hidden;

                    $('#SolidWastActDetailTable tr:last').after(row);

                    $('#edit' + $itemIncriment).unbind("click");
                    $('#edit' + $itemIncriment).bind("click", EditButtonClickUpdateQuantity);

                    $('#remove' + $itemIncriment).unbind("click");
                    $('#remove' + $itemIncriment).bind("click", RemoveButtonClickHandler);

                    $itemSource.push({
                        Id: $itemIncriment,
                        WasteTypeId: data.WasteTypeId,
                        WasteTypeName: data.WasteTypeName,
                        Quantity: data.Quantity,
                        UnitPrice: data.UnitPrice,
                        Amount: data.Amount
                    });

                    updataTotalSumValue();
                    $('#btWasteClose').click();
                    App.unblockUI($detailEditorName);
                }
                else {

                    $selectedRow.find(".trWasteTypeName").html(data.WasteTypeName);
                    $selectedRow.find(".trQuantity").html(Number(data.Quantity).toFixed(2));
                    $selectedRow.find(".trUnitPrice").html(Number(data.UnitPrice).toFixed(2));
                    $selectedRow.find(".trAmount").html(Number(data.Amount).toFixed(2));

                    UpdateItemSource(data.WasteTypeId, data.WasteTypeName, data.Quantity, data.UnitPrice, data.Amount);
                    updataTotalSumValue();
                    $('#btWasteClose').click();
                }

                App.unblockUI($detailEditorName);
            },
            error: function (textStatus, errorThrown) {
                App.unblockUI($detailEditorName);
                updateWasteEditorError('მოხდა შეცდომა')
            }

        });
    }
});

var FormValidationMd = function () {


    var handleValidation = function () {
        var form1 = $('#registrationForm');
        var errorEditor = $('.alert-danger', form1);

        form1.validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                Name: {
                    minlength: 3,
                    required: true
                },
                LandfillId: {
                    required: true
                },

                ActDate: {
                    required: true
                },
                ReceiverName: {
                    minlength: 3,
                    required: true
                },
                ReceiverLastName: {
                    minlength: 3,
                    required: true
                },
                PositionName: {
                    minlength: 3,
                    required: true
                },
                CustomerName: {
                    minlength: 3,
                    required: true
                },
                CustomerCode: {
                    minlength: 3,
                    required: true
                },
                CustomerContactInfo: {
                    minlength: 3,
                    required: true
                },
                //RepresentativeName: {
                //    minlength: 3,
                //    required: true
                //},
                TransporterCarNumber: {
                    minlength: 3,
                    required: true
                },
                TransporterCarModel: {
                    minlength: 3,
                    required: true
                },
                TransporterDriverInfo: {
                    minlength: 3,
                    required: true
                }
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
                generateActReview();
                errorEditor.hide();
                $('#createDiv').hide();
                $('#confirmInformation').show();

                $('#btEditDiv').show();
                $('#btSaveDiv').show();
                $('#btNexDiv').hide();
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
                        '<td style="text-align:right">' +Number($itemSource[i].Quantity).toFixed(2) + '</td>' +
                        '<td style="text-align:right">' + Number($itemSource[i].UnitPrice).toFixed(2) + '</td>' +
                        '<td style="text-align:right">' + Number($itemSource[i].Amount).toFixed(2) + '</td>' +
                      '</tr>';

        totalAmount += $itemSource[i].Amount;
    }

    return { htmlText: text, TotalAmount: totalAmount };
}

function generateActReview() {

    var actDetailInfo = getActReviewDetail();

    var x = '<div class="portlet-body form">' +
        '<form action="#" class="form-horizontal form-row-seperated">' +
            '<div class="form-body">' +
                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"><b>თარიღი:</b></div>' +
                    '<div class="col-sm-3" style="text-align:left">' + document.getElementById("ActDate").value + '</div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>ფიზიკური/იურიდიული პირის დასახელება:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + document.getElementById("CustomerName").value + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"><b>ნაგავსაყრელის მდებარეობა:</b></div>' +
                    '<div class="col-sm-3" style="text-align:left">' + $('#LandfillId').select2('data')[0].text + '</div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>პირადი/ საინდენდიფიკაციო კოდი:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + document.getElementById("CustomerCode").value + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"><b>მიმღების სახელი:</b></div>' +
                    '<div class="col-sm-3" style="text-align:left">' + document.getElementById("ReceiverName").value + '</div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>საკონტაქტო ინფორმაცია:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + document.getElementById("CustomerContactInfo").value + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"><b>მიმღების გვარი:</b></div>' +
                    '<div class="col-sm-3" style="text-align:left">' + document.getElementById("ReceiverLastName").value + '</div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>იურიდიული პირის წარმომადგენელი:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + document.getElementById("RepresentativeName").value + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"><b>მიმღების თანამდებობა:</b></div>' +
                    '<div class="col-sm-3" style="text-align:left">' + document.getElementById("PositionName").value + '</div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>ავტოსატრანსპორტო საშუალების ნომერი:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + document.getElementById("TransporterCarNumber").value + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"></div>' +
                    '<div class="col-sm-3" style="text-align:left"></div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>ავტომობილის მოდელი:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + document.getElementById("TransporterCarModel").value + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"></div>' +
                    '<div class="col-sm-3" style="text-align:left"></div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>მძღოლი:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + document.getElementById("TransporterDriverInfo").value + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div style="padding:10px;">' +
                        '<table class="table table-bordered table-hover" width="100%">' +
                            '<thead>' +
                                '<tr>' +
                                    '<th style="height: 100%;vertical-align: middle; text-align: center;">ნარჩენის ტიპი</th>' +
                                    '<th style="height: 100%;vertical-align: middle; text-align: center; width: 100px;">რ-ბ[ტონა]</th>' +
                                    '<th style="height: 100%;vertical-align: middle; text-align: center; width: 100px;">ერთეულის ფასი [დღგ-ს ჩათვლით]</th>' +
                                    '<th style="height: 100%;vertical-align: middle; text-align: center; width: 100px;">ჯამი[დღგ-ს ჩათვლით]</th>' +
                                '</tr>' +
                            '</thead>' +
                            '<tbody>' +
                                actDetailInfo.htmlText +
                            '</tbody>' +
                        '</table>' +
                        '<div class="btn-group pull-right">' +
                            '<b>სულ:&nbsp&nbsp</b>' + Number(actDetailInfo.TotalAmount).toFixed(2) + ' ₾' +
                        '</div>' +
                    '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"><b>შენიშვნა:</b></div>' +
                    '<div class="col-sm-10" style="text-align:left">' + document.getElementById("Remark").value + '</div>' +
                '</div>' +

            '</div>' +
        '</form>' +
    '</div>';

    $('#confirmInformation').html(x);
}

function AddDetailTableDataSource(dataSourse) {

    for (var i = 0; i < dataSourse.length; i++) {

        $itemIncriment++;
        var data = dataSourse[i];


        editButton = '<td><button type="button" id="edit' + $itemIncriment + '"' +
                      'onClick="EditButtonClickHandler(' + data.WasteTypeId + ')"  ' +
                     'class="btn btn-xs default fa fa-edit" data-toggle="modal" data-target="#detailEditorDialog" ></button>'

        removeButton = '<button type="button" id="remove' + $itemIncriment + '"' +
                        'onClick="RemoveFromItemSource(' + data.WasteTypeId + ')"  ' +
                       ' class="btn btn-xs default fa fa-remove"></button></td>'

        var hidden = '<input type="hidden" id="hidden' + data.WasteTypeId + '" value="Norway">'
        var row = '<tr><td class="trWasteTypeName">' + data.WasteTypeName +
                   '</td> <td class="trQuantity" style="text-align: right;">' + Number(data.Quantity).toFixed(2) +
                   '</td> <td class="trUnitPrice" style="text-align: right;">' + Number(data.UnitPrice).toFixed(2) +
                   '</td><td class="trAmount" style="text-align: right;">' +  Number( data.Amount).toFixed(2) +
                    '</td>' + editButton + removeButton + ' </tr>' + hidden;

        $('#SolidWastActDetailTable tr:last').after(row);

        $('#edit' + $itemIncriment).unbind("click");
        $('#edit' + $itemIncriment).bind("click", EditButtonClickUpdateQuantity);

        $('#remove' + $itemIncriment).unbind("click");
        $('#remove' + $itemIncriment).bind("click", RemoveButtonClickHandler);

        $itemSource.push({
            Id: $itemIncriment,
            WasteTypeId: data.WasteTypeId,
            WasteTypeName: data.WasteTypeName,
            Quantity: data.Quantity,
            UnitPrice: data.UnitPrice,
            Amount: data.Amount
        });

        updataTotalSumValue()
    }
}

jQuery(document).ready(function () {

    if (jQuery().datepicker) {
        $('.date-picker').datepicker({
            rtl: App.isRTL(),
            orientation: "left",
            autoclose: true
        });
    }

    $('#TonQuantity').focus(function () {
        $('#radioQuantityTon').prop('checked', true);
        $('#radioQuantityM3').prop('checked', false);
        $('#M3Quantity').val('0');
    })

    $('#M3Quantity').focus(function () {
        $('#radioQuantityM3').prop('checked', true);
        $('#radioQuantityTon').prop('checked', false);
        $('#TonQuantity').val('0');
    })


    $("#LandfillId").select2({
        width: null
    });
    $("#WasteTypeId").select2({
        width: null
    });

    var recieverDataSource = new Bloodhound({
        datumTokenizer: function (d) { return Bloodhound.tokenizers.whitespace(d.Description); },
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        local: recieverItemSource
    });

    var customerCodeDataSource = new Bloodhound({
        datumTokenizer: function (d) { return Bloodhound.tokenizers.whitespace(d.Code); },
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        local: customerItemSource
    });

    var customerNameDataSource = new Bloodhound({
        datumTokenizer: function (d) { return Bloodhound.tokenizers.whitespace(d.Name); },
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        local: customerItemSource
    });


    var transporterDataSource = new Bloodhound({
        datumTokenizer: function (d) { return Bloodhound.tokenizers.whitespace(d.CarNumber); },
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        local: transporterItemSource
    });

    recieverDataSource.initialize();
    customerCodeDataSource.initialize();
    customerNameDataSource.initialize();
    transporterDataSource.initialize();

    if (App.isRTL()) {
        $('#ReceiverName').attr("dir", "rtl");
    }
    if (App.isRTL()) {
        $('#CustomerCode').attr("dir", "rtl");
    }

    if (App.isRTL()) {
        $('#CustomerName').attr("dir", "rtl");
    }

    if (App.isRTL()) {
        $('#TransporterCarNumber').attr("dir", "rtl");
    }

    $('#ReceiverName').typeahead(null, {
        displayKey: 'ReceiverName',
        hint: (App.isRTL() ? false : true),
        source: recieverDataSource.ttAdapter(),
        templates: {
            suggestion: Handlebars.compile([
              '<div class="media">',
                    '<div class="media-body">',
                        '<h4 class="media-heading">{{ReceiverName}} {{ReceiverLastName}}</h4>',
                        '<p>{{Posistion}}</p>',
                    '</div>',
              '</div>',
            ].join(''))
        }

    }).on('typeahead:selected', function (event, selection) {
        $("#ReceiverLastName").val(selection.ReceiverLastName);
        $("#PositionName").val(selection.Posistion);
    });

    $('#CustomerCode').typeahead(null, {
        displayKey: 'Code',
        hint: (App.isRTL() ? false : true),
        source: customerCodeDataSource.ttAdapter(),
        templates: {
            suggestion: Handlebars.compile([
              '<div class="media">',
                    '<div class="media-body">',
                        '<h4 class="media-heading">{{Code}} - {{Name}}</h4>',
                        '<p><b>საკონტაქტო ინფორმაცია:</b> {{ContactInfo}}</p>',
                        '<p><b>წარმომადგენელი:</b> {{RepresentativeName}}</p>',
                    '</div>',
              '</div>',
            ].join(''))
        }

    }).on('typeahead:selected', function (event, selection) {
        $("#CustomerName").val(selection.Name);
        $("#CustomerContactInfo").val(selection.ContactInfo);
        $("#RepresentativeName").val(selection.RepresentativeName);
    });

    $('#CustomerName').typeahead(null, {
        displayKey: 'Name',
        hint: (App.isRTL() ? false : true),
        source: customerNameDataSource.ttAdapter(),
        templates: {
            suggestion: Handlebars.compile([
              '<div class="media">',
                    '<div class="media-body">',
                        '<h4 class="media-heading">{{Code}} - {{Name}}</h4>',
                        '<p><b>საკონტაქტო ინფორმაცია:</b> {{ContactInfo}}</p>',
                        '<p><b>წარმომადგენელი:</b> {{RepresentativeName}}</p>',
                    '</div>',
              '</div>',
            ].join(''))
        }

    }).on('typeahead:selected', function (event, selection) {
        $("#CustomerCode").val(selection.Code);
        $("#CustomerContactInfo").val(selection.ContactInfo);
        $("#RepresentativeName").val(selection.RepresentativeName);
    });

    $('#TransporterCarNumber').typeahead(null, {
        displayKey: 'CarNumber',
        hint: (App.isRTL() ? false : true),
        source: transporterDataSource.ttAdapter(),
        templates: {
            suggestion: Handlebars.compile([
              '<div class="media">',
                    '<div class="media-body">',
                        '<h4 class="media-heading">{{CarNumber}}</h4>',
                        '<p><b>მოდელი:</b> {{CarModel}}</p>',
                        '<p><b>მძღოლი:</b> {{DriverInfo}}</p>',
                    '</div>',
              '</div>',
            ].join(''))
        }

    }).on('typeahead:selected', function (event, selection) {
        //$("#TransporterCarNumber").val(selection.CarNumber);
        $("#TransporterCarModel").val(selection.CarModel);
        $("#TransporterDriverInfo").val(selection.DriverInfo);
    });

    $('#Type').change(function () {

        editorName = '#editorDialog'
        App.blockUI({
            target: editorName,
            animate: true
        });

        $.ajax({
            url: "/SolidWasteActJurnal/LoadCustomerByCode",
            type: "GET",
            dataType: "json",
            data: { CustomerType: document.getElementById("Type").value },
            success: function (data) {
                $("#CustomerCode").val('');
                $("#CustomerName").val('');
                $("#CustomerContactInfo").val('');
                $("#RepresentativeName").val('');
                removeTableData();

                customerCodeDataSource.clear();
                customerNameDataSource.clear();

                customerCodeDataSource.local = data;
                customerNameDataSource.local = data;

                customerCodeDataSource.initialize(true);
                customerNameDataSource.initialize(true);

                App.unblockUI(editorName);
            },
            error: function (request, status, error) {
                updateErrorEditor(errorEditor, request.responseText)
                App.unblockUI(editorName);
            }
        })

    });

    $('#next').click(function () {
        $('#registrationForm').submit();
    });

    $('#btEdit').click(function () {
        $('#createDiv').show();
        $('#confirmInformation').hide();

        $('#btEditDiv').hide();
        $('#btSaveDiv').hide();
        $('#btNexDiv').show();
    });

    $('#btSave').click(function () {
        saveSolidWasteAct();
    });

    AddDetailTableDataSource(modelSolidWasteTypeItemSource);

    FormValidationMd.init();
});
