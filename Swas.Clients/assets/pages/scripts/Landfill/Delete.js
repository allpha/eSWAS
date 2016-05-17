﻿$('#btSave').click(function () {
    saveRegion();
});

function saveRegion() {

    editorName = '#editorDialog'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $.ajax({
        url: "/SolidWasteAct/DeleteSolidWasteAct",
        type: "POST",
        dataType: "json",
        data: {
            id: $("#Id").val(),
        },
        success: function (data) {
            location.href = '/SolidWasteAct/Index';
            App.unblockUI(editorName);
        },
        error: function (request, status, error) {
            updateErrorEditor($('.alert-danger', form1), request.responseText)
            App.unblockUI(editorName);
        }
    });
}

function updateErrorEditor(source, text) {
    source.text(text);
    source.append(' <button class="close" data-close="alert"></button> ');
    source.show();
}
