$('#btSave').click(function () {
    remove();
});

function remove() {
    BusyIndicator.setBusy();
    $.ajax({
        url: "/User/Remove",
        type: "POST",
        dataType: "json",
        data: {
            id: $("#Id").val(),
        },
        success: function (data) {
            location.href = '/User/Index';
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
