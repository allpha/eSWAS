function load() {

    BusyIndicator.setBusy();

    $.ajax({
        url: "/Transporter/Load",
        type: "POST",
        dataType: "json",
        data: {

        },
        success: function (data) {
            initTableItemSource(data);
            BusyIndicator.unsetBusy();
        },
        error: function (request, status, error) {
            BusyIndicator.unsetBusy();
        }
    });
}

function initTableItemSource(itemSource) {

    for (var i = 0; i < itemSource.length; i++) {

        var editButton = '';
        var removeButton = '';
        var unlockButton = '';
        var disableButton = '';
        var changePasswordButton = '';
        if ($("#hasEdit").val() == "true")
            editButton = '<a href="javascript:edit(' + itemSource[i].Id + ');" class="btn btn-xs default"><i class="fa fa-edit"></i></a>';
        if ($("#hasDelete").val() == "true")
            removeButton = '<a href="javascript:remove(' + itemSource[i].Id + ');" class="btn btn-xs default"><i class="fa fa-remove"></i></a>';

        var row = '<tr>'+
                    '<td>' + itemSource[i].CarModel +
                    '<td>' + itemSource[i].CarNumber + '</td>' +
                    '<td>' + itemSource[i].DriverInfo + '</td>' +
                    '<td>' +
                     editButton +
                     removeButton +
                    '</td></tr>';

        $('#itemSource tr:last').after(row);
    }
}

function create() {
    $('#ajax-model-dialog-editor').attr("data-url", "/Transporter/Create");
    $('#ajax-model-dialog-editor').click();
}

function edit(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/Transporter/Edit/" + id);
    $('#ajax-model-dialog-editor').click();
}

function remove(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/Transporter/Delete/" + id);
    $('#ajax-model-dialog-editor').click();
}

jQuery(document).ready(function () {
    BusyIndicator.init("#editorDialog");
    load();
});

