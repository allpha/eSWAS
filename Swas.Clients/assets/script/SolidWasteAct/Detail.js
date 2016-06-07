
function getActReviewDetail(itemSource) {
    var text = '';
    for (var i = 0; i < itemSource.length; i++) {

        text += '<tr>' +
                        '<td>' + itemSource[i].WasteTypeName + '</td>' +
                        '<td style="text-align:right">' + Number(itemSource[i].Quantity).toFixed(2) + '</td>' +
                        '<td style="text-align:right">' + Number(itemSource[i].UnitPrice).toFixed(2) + '</td>' +
                        '<td style="text-align:right">' + Number(itemSource[i].Amount).toFixed(2) + '</td>' +
                      '</tr>';

    }

    return text;
}

function generateActReview(data) {

    var x = '<div class="portlet-body form">' +
        '<form action="#" class="form-horizontal form-row-seperated">' +
            '<div class="form-body">' +
                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"><b>თარიღი:</b></div>' +
                    '<div class="col-sm-3" style="text-align:left">' + moment(data.ActDate).format('DD/MM/YYYY HH:mm') + ' </div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>ფიზიკური/იურიდიული პირის დასახელება:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + data.CustomerName + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"><b>ნაგავსაყრელის მდებარეობა:</b></div>' +
                    '<div class="col-sm-3" style="text-align:left">' + data.LandfillName + '</div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>პირადი/ საინდენდიფიკაციო კოდი:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + data.CustomerCode + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"><b>მიმღების სახელი:</b></div>' +
                    '<div class="col-sm-3" style="text-align:left">' + data.ReceiverName + '</div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>საკონტაქტო ინფორმაცია:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + data.CustomerContactInfo + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"><b>მიმღების გვარი:</b></div>' +
                    '<div class="col-sm-3" style="text-align:left">' + data.ReceiverLastName + '</div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>იურიდიული პირის წარმომადგენელი:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + data.RepresentativeName + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"><b>მიმღების თანამდებობა:</b></div>' +
                    '<div class="col-sm-3" style="text-align:left">' + data.PositionName + '</div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>ავტომობილის მარკა:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + data.TransporterCarModel + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"></div>' +
                    '<div class="col-sm-3" style="text-align:left"></div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>ავტომობილის ნომერი:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + data.TransporterCarNumber + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"></div>' +
                    '<div class="col-sm-3" style="text-align:left"></div>' +
                    '<div class="col-sm-3" style="text-align:right; font-size:13px;"><b>მძღოლი:</b> </div>' +
                    '<div class="col-sm-4" style="text-align:left">' + data.TransporterDriverInfo + '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div style="padding:10px;">' +
                        '<table class="table table-bordered table-hover" width="100%">' +
                            '<thead>' +
                                '<tr>' +
                                    '<th style="height: 100%;vertical-align: middle; text-align: center;">ნარჩენის ტიპი</th>' +
                                    '<th style="height: 100%;vertical-align: middle; text-align: center; width: 100px;">რ-ბ[ტონა]</th>' +
                                    '<th style="height: 100%;vertical-align: middle; text-align: center; width: 100px;">ერთეულის ფასი [დღგ-ს ჩათვლით]</th>' +
                                    '<th style="height: 100%;vertical-align: middle; text-align: center; width: 100px;">ჯამი[დღგ-ს ჩათვლით]</th>' +
                                '</tr>' +
                            '</thead>' +
                            '<tbody>' +
                                getActReviewDetail(data.SolidWasteActDetails) +
                            '</tbody>' +
                        '</table>' +
                        '<div class="btn-group pull-right">' +
                            '<b>სულ:&nbsp&nbsp</b>' + Number(data.TotalAmount).toFixed(2) + ' ₾' +
                        '</div>' +
                    '</div>' +
                '</div>' +

                '<div class="form-group col-sm-12">' +
                    '<div class="col-sm-2" style="text-align:right; font-size:13px;"><b>შენიშვნა:</b></div>' +
                    '<div class="col-sm-10" style="text-align:left">' + data.Remark + '</div>' +
                '</div>' +

            '</div>' +
        '</form>' +
    '</div>';

    $('#confirmInformation').html(x);
}

function load() {

    editorName = '#editorDialog'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $.ajax({
        url: "/SolidWasteActPrint/Load",
        type: "POST",
        dataType: "json",
        data: {
            id: $("#Id").val()
        },
        success: function (data) {
            App.unblockUI(editorName);
            generateActReview(data);
        },
        error: function (request, status, error) {
            App.unblockUI(editorName);
            alert(error)
        }
    });
}

function windowCloseClick() {
    if ($('#isSaved').val() == 'saved')
        location.href = '/SolidWasteAct/Index';
    else
        windlowCloseHiddenButton.click();
}


jQuery(document).ready(function () {
    load();
});
