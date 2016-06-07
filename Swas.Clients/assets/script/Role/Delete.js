$('#btSave').click(function () {
    saveRegion();
});

function saveRegion() {
    BusyIndicator.setBusy();
    $.ajax({
        url: "/Role/Remove",
        type: "POST",
        dataType: "json",
        data: {
            id: $("#Id").val(),
        },
        success: function (data) {
            location.href = '/Role/Index';
            BusyIndicator.unsetBusy();
        },
        error: function (request, status, error) {
            updateErrorEditor($('.alert-danger', form1), request.responseText)
            BusyIndicator.unsetBusy();
        }
    });
}

function updateErrorEditor(source, text) {
    source.text(text);
    source.append(' <button class="close" data-close="alert"></button> ');
    source.show();
}

jQuery(document).ready(function () {
    BusyIndicator.init("#editorDialog");
});
