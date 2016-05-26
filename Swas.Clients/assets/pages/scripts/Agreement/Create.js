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
        url: "/Agreement/Create",
        type: "POST",
        dataType: "json",
        data: {
            code: $("#Code").val(),
            customerId: $("#CustomerId").val(),
            startDate: $("#StartDate").val(),
            endDate: $("#EndDate").val(),
        },
        success: function (data) {
            location.href = '/Agreement/Index';
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
                Code: {
                    minlength: 3,
                    required: true
                },
                CustomerId: {
                    required: true
                },

                StartDate: {
                    required: true
                },

                EndDate: {
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

function initDate() {
    $('.date-picker').datepicker({
        rtl: App.isRTL(),
        orientation: "left",
        autoclose: true
    });

}

function initCustomer() {

    editorName = '#editorDialog'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $("#CustomerId").select2({
        allowClear: true,
        width: null
    });

    $.ajax({
        url: "/Agreement/LoadCustomer",
        type: "POST",
        dataType: "json",
        data: {
        },
        success: function (data) {
            App.unblockUI(editorName);

            var select2Name = "#CustomerId";
            $(select2Name).empty();

            for (var i = 0; i < data.length; i++) {
                $(select2Name).append('<option value="' + data[i].Id + '">' + data[i].Name + '</option>');
            }

            if (data != null && data.length > 0)
                $(select2Name).val(data[0].Id).trigger("change");
        },
        error: function (request, status, error) {
        }
    });


}

jQuery(document).ready(function () {
    initDate();
    initCustomer();
    FormValidationMd.init();
});