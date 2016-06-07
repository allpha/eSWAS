function load() {

    editorName = '#viewBody'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $.ajax({
        url: "/Role/Load",
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

        var editButton = '';
        var removeButton = '';
        if ($("#hasEdit").val() == "true")
            editButton = '<a href="javascript:edit(' + itemSource[i].Id + ');" class="btn btn-xs default"><i class="fa fa-edit"></i></a>';
        if ($("#hasDelete").val() == "true")
            removeButton = '<a href="javascript:remove(' + itemSource[i].Id + ');" class="btn btn-xs default"><i class="fa fa-remove"></i></a>';


        var row = '<tr><td>' +
                        itemSource[i].Description +
                    '</td><td>' +
                     editButton +
                     removeButton +
                    '</td></tr>';

        $('#itemSource tr:last').after(row);
    }
}

function create() {
    $('#ajax-model-dialog-editor').attr("data-url", "/Role/Create");
    $('#ajax-model-dialog-editor').click();
}

function edit(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/Role/Edit/" + id);
    $('#ajax-model-dialog-editor').click();
}

function remove(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/Role/Delete/" + id);
    $('#ajax-model-dialog-editor').click();
}

jQuery(document).ready(function () {
    load();
});