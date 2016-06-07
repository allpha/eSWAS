function loadRegions() {

    editorName = '#viewBody'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $.ajax({
        url: "/Landfill/LoadLangfills",
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
            editButton = '<a href="javascript:edit(' + itemSource[i].Id + ');" class="btn btn-icon-only default"><i class="fa fa-edit"></i></a>';
        if ($("#hasDelete").val() == "true")
            removeButton = '<a href="javascript:remove(' + itemSource[i].Id + ');" class="btn btn-icon-only default"><i class="fa fa-remove"></i></a>';
        var row = '<tr><td>' +
                        itemSource[i].Name +
                    '</td><td>' +
                        itemSource[i].RegionName +
                    '</td><td>' +
                     editButton +
                     removeButton +
                    '</td></tr>';

        $('#regionsItemSource tr:last').after(row);
    }
}

function create() {
    $('#ajax-model-dialog-editor').attr("data-url", "/Landfill/Create");
    $('#ajax-model-dialog-editor').click();
}

function edit(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/Landfill/Edit/" + id);
    $('#ajax-model-dialog-editor').click();
}

function remove(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/Landfill/Delete/" + id);
    $('#ajax-model-dialog-editor').click();
}

jQuery(document).ready(function () {
    loadRegions();
});