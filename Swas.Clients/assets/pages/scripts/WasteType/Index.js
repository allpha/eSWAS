function loadWasteTypes() {

    editorName = '#viewBody'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $.ajax({
        url: "/WasteType/LoadWasteTypes",
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

function initTableItemSource(itemSource) {

    for (var i = 0; i < itemSource.length; i++) {
        var row = '<tr>  <td><b>' +
                            itemSource[i].Name +
                        '</b></td>' +
                        '<td style="text-align:right">' +
                            '<b>' +
                                '< ' + itemSource[i].LessQuantity + 'ტ.</b>' +
                        '</td>' +
                        '<td style="text-align:right"> <b>' +
                                itemSource[i].FromQuantity + '-' + itemSource[i].EndQuantity +
                                'ტ.</b> </td>' +
                        '<td style="text-align:right"> <b> > ' +
                            itemSource[i].MoreQuantity +
                         'ტ.</b> </td>' +
                        '<td style="text-align:center"><b>' +
                            itemSource[i].Coeficient +
                        'ტ.</b></td>' +
                        '<td>' +
                        '<a href="javascript:edit(' + itemSource[i].Id + ');" class="btn btn-icon-only default"><i class="fa fa-edit"></i></a>' +
                        '<a href="javascript:remove(' + itemSource[i].Id + ');" class="btn btn-icon-only default"><i class="fa fa-remove"></i></a>' +
                        '</td></tr>' +
                        '<tr style="text-align:right">' +
                            '<td>' +
                                 'მუნიციპალიტეტი' +
                            '</td>' +
                            '<td>' +
                                itemSource[i].MunicipalityLessQuantityPrice +
                        '₾</td>' +
                            '<td>' +
                                itemSource[i].MunicipalityIntervalQuantityPrice +
                        '₾</td>' +
                            '<td>' +
                                itemSource[i].MunicipalityMoreQuantityPrice +
                        '₾</td>' +
                        '<td ></td>' +
                        '<td></td></tr>' +

                        ///

                            '<tr style="text-align:right">' +
                            '<td>' +
                                 'იურიდიული პირი' +
                            '</td>' +
                            '<td>' +
                                itemSource[i].LegalPersonLessQuantityPrice +
                        '₾</td>' +
                            '<td>' +
                                itemSource[i].LegalPersonIntervalQuantityPrice +
                        '₾</td>' +
                            '<td>' +
                                itemSource[i].LegalPersonMoreQuantityPrice +
                        '₾</td>' +
                        '<td style="display:none"></td>' +
                        '<td style="display:none"></td></tr>' +

                                ///

                                '<tr style="text-align:right">' +
                                '<td>' +
                                     'ფიზიკური პირი' +
                                '</td>' +
                                '<td>' +
                                    itemSource[i].PhysicalPersonLessQuantityPrice +
                            '₾</td>' +
                                '<td>' +
                                    itemSource[i].PhysicalPersonIntervalQuantityPrice +
                            '₾</td>' +
                                '<td>' +
                                    itemSource[i].PhysicalPersonMoreQuantityPrice +
                            '₾</td>' +
                            '<td style="display:none"></td>' +
                            '<td style="display:none"></td></tr> '







        $('#regionsItemSource tr:last').after(row);
    }
}

function create() {
    $('#ajax-model-dialog-editor').attr("data-url", "/WasteType/Create");
    $('#ajax-model-dialog-editor').click();
}

function edit(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/WasteType/Edit/" + id);
    $('#ajax-model-dialog-editor').click();
}

function remove(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/WasteType/Delete/" + id);
    $('#ajax-model-dialog-editor').click();
}

jQuery(document).ready(function () {
    loadWasteTypes();
});