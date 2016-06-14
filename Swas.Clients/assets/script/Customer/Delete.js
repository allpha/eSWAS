$('#btSave').click(function () {
    saveCustomer();
});

function saveCustomer() {
    BusyIndicator.setBusy();
    $.ajax({
        url: "/Customer/Remove",
        type: "POST",
        dataType: "json",
        data: {
            id: $("#Id").val(),
        },
        success: function (data) {
            location.href = '/Customer/Index';
            BusyIndicator.unsetBusy();
        },
        error: function (request, status, error) {
            updateErrorEditor($('.alert-danger', "#registrationForm"), request.responseText)
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
