function initTableItemSource(itemSource) {

    $("#historyItemSource").find("tr:not(:first)").remove();

    for (var i = 0; i < itemSource.length; i++) {
        var row = '<tr>' +
                       '<td>' + itemSource[i].Order + '</td>' +
                       '<td>' + moment(itemSource[i].CreateDate).format('DD/MM/YYYY hh:MM') + '</td> <td>';
        row += '<a target="_blank" href="/SolidWasteActHistory/LoadHistory/?historyId=' + itemSource[i].Id + '" class="btn btn-xs default"><i class="fa fa-print"></i></a>';
        row+=  '</td>' +
                  '</tr>';
        $('#historyItemSource tr:last').after(row);
    }
}

function loadData() {

    editorName = '#editorDialog'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $.ajax({
        url: "/SolidWasteActHistory/Load",
        type: "POST",
        dataType: "json",
        data: {
            solidWasteActId: $("#Id").val(),
        },
        success: function (data) {
            initTableItemSource(data);
            App.unblockUI(editorName);
        },
        error: function (request, status, error) {
            alert('მოხდა სერვერსული შეცდომა');
            App.unblockUI(editorName);
        }
    });

}



jQuery(document).ready(function () {
    loadData();
});
