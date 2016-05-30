$('#btSave').click(function () {
    $('#registrationForm').submit();
});

function saveRole(errorEditor) {
    BusyIndicator.setBusy();

    $.ajax({
        url: "/Role/Create",
        type: "POST",
        dataType: "json",
        data: {
            descirption: $("#description").val(),
            permissions: $('#permissionCombo').val()
        },
        success: function (data) {
            location.href = '/Role/Index';
            BusyIndicator.unsetBusy();
        },
        error: function (request, status, error) {
            updateErrorEditor(errorEditor, request.responseText)
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
                description: {
                    minlength: 2,
                    required: true
                },
                permissionCombo: {
                    minlength: 1,
                    required: true
                },

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
                saveRole(errorEditor);
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


function loadPermission() {

    BusyIndicator.setBusy();

    $.ajax({
        url: "/role/LoadPermissions",
        type: "POST",
        dataType: "json",
        data: {

        },
        success: function (data) {
            var select2Name = "#permissionCombo";
            $(select2Name).empty();
            for (var i = 0; i < data.length; i++) {
                $(select2Name).append('<option value="' + data[i].Id + '">' + data[i].Name + '</option>');
            }
            BusyIndicator.unsetBusy();
        },
        error: function (request, status, error) {
            BusyIndicator.unsetBusy();
        }
    });

}

function initPermission() {
    $("#permissionCombo").select2({
        placeholder: "გთხოვთ აირჩიოთ უფლებები",
        allowClear: true,
        width: null
    });

    loadPermission();
}


jQuery(document).ready(function () {

    BusyIndicator.init("#editorDialog");
    initPermission();
    FormValidationMd.init();
});