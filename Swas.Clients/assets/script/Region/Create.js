$('#btSave').click(function () {
    $('#registrationForm').submit();
});

function saveRegion(errorEditor) {
    editorName = '#editorDialog'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $.ajax({
        url: "/Region/Create",
        type: "POST",
        dataType: "json",
        data: {
            name: $("#regionName").val()
        },
        success: function (data) {
            location.href = '/Region/Index';
            App.unblockUI(editorName);
        },
        error: function (request, status, error) {
            updateErrorEditor(errorEditor, request.responseText)
            App.unblockUI(editorName);
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
                regionName: {
                    minlength: 2,
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
                saveRegion(errorEditor);
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

jQuery(document).ready(function () {
    FormValidationMd.init();
});