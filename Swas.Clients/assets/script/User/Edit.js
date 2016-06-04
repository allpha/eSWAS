$('#btSave').click(function () {
    $('#registrationForm').submit();
});

function save(errorEditor) {
    BusyIndicator.setBusy();

    $.ajax({
        url: "/User/Edit",
        type: "POST",
        dataType: "json",
        data: {
            id: $("#Id").val(),
            userName: $("#UserName").val(),
            email: $("#Email").val(),
            useEmailAsUserName: $('#UseEmailAsUserName').is(':checked'),
            roleId: $("#roleCombo").val(),
            firstName: $("#FirstName").val(),
            lastName: $("#LastName").val(),
            privateNumber: $("#PrivateNumber").val(),
            birthDate: $("#BirthDate").val(),
            jobPosition: $("#JobPosition").val(),
            regions: $("#regionCombo").val()
        },
        success: function (data) {
            location.href = '/User/Index';
            BusyIndicator.unsetBusy();
        },
        error: function (request, status, error) {
            updateErrorEditor(errorEditor, request.responseText)
            BusyIndicator.unsetBusy();
        }
    });
}


function load() {
    BusyIndicator.setBusy();

    $.ajax({
        url: "/User/Get",
        type: "POST",
        dataType: "json",
        data: {
            id: $("#Id").val(),
        },
        success: function (data) {

            $("#UserName").val(data.UserName);
            $("#Email").val(data.Email);
            $("#UseEmailAsUserName").prop("checked", data.UseEmailAsUserName);
            $("#FirstName").val(data.FirstName);
            $("#LastName").val(data.LastName);
            $("#PrivateNumber").val(data.PrivateNumber);
            if (data.BirthDate != null)
                $("#BirthDate").val(moment(data.BirthDate).format('MM/DD/YYYY'));
            $("#JobPosition").val(data.JobPosition);

            loadRegions(data.RoleId, data.Regions)
            BusyIndicator.unsetBusy();


        },
        error: function (request, status, error) {
            //updateErrorEditor(errorEditor, request.responseText)
            BusyIndicator.unsetBusy();
        }
    });
}

function updateErrorEditor(source, text) {
    source.text(text);
    source.append(' <button class="close" data-close="alert"></button> ');
    source.show();
}

var FormValidationMd = function () {


    var handleValidation = function () {
        var form1 = $('#registrationForm');
        var errorEditor = $('.alert-danger', form1);

        form1.validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                UserName: {
                    minlength: 5,
                    required: true
                },
                Email: {
                    minlength: 5,
                    required: true,
                    email: true
                },
                roleCombo: {
                    minlength: 1,
                    required: true
                },
                FirstName: {
                    minlength: 2,
                    required: true
                },
                LastName: {
                    minlength: 2,
                    required: true
                },
                JobPosition: {
                    minlength: 5,
                    required: true
                },
                PeronalNumber: {
                    digits: true,
                    minlength: 11,
                    maxlength: 11
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit
                updateErrorEditor(errorEditor, 'მონაცემები არ არის ვალიდური');
            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            unhighlight: function (element) { // revert the change done by hightlight
                $(element)
                    .closest('.form-group').removeClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label
                    .closest('.form-group').removeClass('has-error'); // set success class to the control group
            },

            submitHandler: function (form) {
                errorEditor.hide();
                save(errorEditor);
            }
        });
    }

    return {
        //main function to initiate the module
        init: function () {
            handleValidation();
        }
    };
}();


function loadRegions(roleId, regions) {

    BusyIndicator.setBusy();

    $.ajax({
        url: "/User/LoadRegions",
        type: "POST",
        dataType: "json",
        data: {

        },
        success: function (data) {
            var select2Name = "#regionCombo";
            $(select2Name).empty();
            for (var i = 0; i < data.length; i++) {
                $(select2Name).append('<option value="' + data[i].Id + '">' + data[i].Name + '</option>');
            }
            $(select2Name).val(regions).trigger("change");
            BusyIndicator.unsetBusy();
            loadRoles(roleId);
        },
        error: function (request, status, error) {
            BusyIndicator.unsetBusy();
            loadRoles(roleId);
        }
    });

}

function loadRoles(roleId) {

    BusyIndicator.setBusy();

    $.ajax({
        url: "/User/LoadRoles",
        type: "POST",
        dataType: "json",
        data: {

        },
        success: function (data) {
            var select2Name = "#roleCombo";
            $(select2Name).empty();
            for (var i = 0; i < data.length; i++) {
                $(select2Name).append('<option value="' + data[i].Id + '">' + data[i].Name + '</option>');
            }
            $(select2Name).val(roleId).trigger("change");
            BusyIndicator.unsetBusy();
        },
        error: function (request, status, error) {
            BusyIndicator.unsetBusy();
        }
    });

}


function initRegion() {
    $("#regionCombo").select2({
        placeholder: "",
        allowClear: true,
        width: null
    });

    $("#roleCombo").select2({
        placeholder: "",
        allowClear: true,
        width: null
    });
}

function InitDataTimePicker() {
    if (jQuery().datepicker) {
        $('.date-picker').datepicker({
            rtl: App.isRTL(),
            orientation: "left",
            autoclose: true
        });
    }
}

jQuery(document).ready(function () {
    InitDataTimePicker();
    BusyIndicator.init("#editorDialog");
    initRegion();
    load();
    FormValidationMd.init();
});