function loadAgreements() {

    editorName = '#viewBody'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $.ajax({
        url: "/Agreement/Load",
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
        var row = '<tr><td>' +
                        itemSource[i].Code +
                    '</td>' +
                    '<td>' +
                        itemSource[i].CustomerCode +
                     '</td>' +

                     '<td>' +
                        itemSource[i].CustomerName +
                     '</td>' +

                     '<td>' +
                        moment(itemSource[i].StartDate).format('DD/MM/YYYY') +
                     '</td>' +

                     '<td>' +
                        moment(itemSource[i].EndDate).format('DD/MM/YYYY') +
                     '</td>' +
                    '<td>';
        if ($("#hasEdit").val() == "true")
            row += '<a href="javascript:edit(' + itemSource[i].Id + ');" class="btn btn-xs default"><i class="fa fa-edit"></i></a>';
        if ($("#hasDelete").val() == "true")
            row += '<a href="javascript:remove(' + itemSource[i].Id + ');" class="btn btn-xs default"><i class="fa fa-remove"></i></a>';
        row += '</td></tr>';

        $('#itemSource tr:last').after(row);
    }
}

function create() {
    $('#ajax-model-dialog-editor').attr("data-url", "/Agreement/Create");
    $('#ajax-model-dialog-editor').click();
}

function edit(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/Agreement/Edit/" + id);
    $('#ajax-model-dialog-editor').click();
}

function remove(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/Agreement/Delete/" + id);
    $('#ajax-model-dialog-editor').click();
}

jQuery(document).ready(function () {
    loadAgreements();
});