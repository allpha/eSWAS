﻿function load() {

    BusyIndicator.setBusy();

    $.ajax({
        url: "/User/Load",
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
        if ($("#hasUnlock").val() == "true")
            unlockButton = '<a href="javascript:unlock(' + itemSource[i].Id + ');" class="btn btn-xs default"><i class="glyphicon glyphicon-user"></i></a>';
        if ($("#hasDisible").val() == "true")
            disableButton = '<a href="javascript:setDisible(' + itemSource[i].Id + ');" class="btn btn-xs default"><i class="glyphicon glyphicon-eye-close"></i></a>';
        if ($("#hasChangePassword").val() == "true")
            changePasswordButton = '<a href="javascript:changePassword(' + itemSource[i].Id + ');" class="btn btn-xs default"><i class="glyphicon glyphicon-wrench"></i></a>';


        var row = '<tr>'+
                    '<td>' + itemSource[i].FirstName + '&nbsp' + itemSource[i].LastName + '</td>' +
                    '<td>' + itemSource[i].PrivateNumber + '</td>' +
                    '<td>' + itemSource[i].UserName + '</td>' +
                    '<td>' + itemSource[i].Email + '</td>' +
                    '<td>' + itemSource[i].JobPosition + '</td>' +
                    '<td>' + itemSource[i].RoleName + '</td>' +
                    '<td>' + itemSource[i].Status + '</td>' +
                    '<td>' +
                     editButton +
                     removeButton +
                     unlockButton +
                    disableButton +
                    changePasswordButton +
                    '</td></tr>';

        $('#itemSource tr:last').after(row);
    }
}

function create() {
    $('#ajax-model-dialog-editor').attr("data-url", "/User/Create");
    $('#ajax-model-dialog-editor').click();
}

function unlock(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/User/UnlockPromt/" + id);
    $('#ajax-model-dialog-editor').click();
}

function setDisible(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/User/DisiblePromt/" + id);
    $('#ajax-model-dialog-editor').click();
}

function changePassword(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/User/PasswordReset/" + id);
    $('#ajax-model-dialog-editor').click();
}


function view(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/User/Edit/" + id);
    $('#ajax-model-dialog-editor').click();
}


function edit(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/User/Edit/" + id);
    $('#ajax-model-dialog-editor').click();
}

function remove(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/User/Delete/" + id);
    $('#ajax-model-dialog-editor').click();
}

jQuery(document).ready(function () {
    BusyIndicator.init("#editorDialog");
    load();
});

