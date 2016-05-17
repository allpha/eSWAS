function loadSolidWasteActs() {

    editorName = '#viewBody'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $.ajax({
        url: "/SolidWasteAct/LoadSolidWasteActs",
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
        //var actDate = dateFormat(itemSource[i].ActDate, "mm/dd/yyyy");
        var d = new Date(parseInt(itemSource[i].ActDate.slice(6, -2)));
      //  alert('' + (1 + d.getMonth()) + '/' + d.getDate() + '/' + d.getFullYear().toString().slice(-2));


        var row = '<tr>' +
                       '<td>' + itemSource[i].Id + '</td>' +
                       '<td>' + moment(itemSource[i].ActDate).format('DD/MM/YYYY') + '</td>' +
                       '<td>' + itemSource[i].LandfillName + '</td>' +
                       '<td>' + itemSource[i].Receiver + '</td>' +
                       '<td>' + itemSource[i].Customer + '</td>' +
                       '<td>' + itemSource[i].Quantity + '</td>' +
                       '<td>' + itemSource[i].Price + '</td>' +
                       '<td>' +
                            '<a href="javascript:edit(' + itemSource[i].Id + ');" class="btn btn-xs default"><i class="fa fa-edit"></i></a>' +
                            '<a href="javascript:remove(' + itemSource[i].Id + ');" class="btn btn-xs default"><i class="fa fa-remove"></i></a>' +
                       '</td>' +
                  '</tr>';


        $('#itemSource tr:last').after(row);
    }
}

function create() {
    $('#ajax-model-dialog-editor').attr("data-url", "/SolidWasteAct/Create");
    $('#ajax-model-dialog-editor').click();
}

function edit(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/SolidWasteAct/Edit/" + id);
    $('#ajax-model-dialog-editor').click();
}

function remove(id) {
    $('#ajax-model-dialog-editor').attr("data-url", "/SolidWasteAct/Delete/" + id);
    $('#ajax-model-dialog-editor').click();
}

jQuery(document).ready(function () {
    loadSolidWasteActs();
});