$('#btSave').click(function () {
    $('#registrationForm').submit();
});

function saveWasteType(errorEditor) {
    editorName = '#editorDialog'
    App.blockUI({
        target: editorName,
        animate: true
    });

    $.ajax({
        url: "/WasteType/Create",
        type: "POST",
        dataType: "json",
        data: {
            name: $("#Name").val() ,
            lessQuantity: $("#LessQuantity").val(),
            fromQuantity: $("#FromQuantity").val(),
            endQuantity: $("#EndQuantity").val(),
            moreQuantity: $("#MoreQuantity").val(),
            municipalityLessQuantityPrice: $("#MunicipalityLessQuantityPrice").val(),
            municipalityIntervalQuantityPrice: $("#MunicipalityIntervalQuantityPrice").val(),
            municipalityMoreQuantityPrice: $("#MunicipalityMoreQuantityPrice").val(),
            legalPersonLessQuantityPrice: $("#LegalPersonLessQuantityPrice").val(),
            legalPersonIntervalQuantityPrice: $("#LegalPersonIntervalQuantityPrice").val(),
            legalPersonMoreQuantityPrice: $("#LegalPersonMoreQuantityPrice").val(),
            physicalPersonLessQuantityPrice: $("#PhysicalPersonLessQuantityPrice").val(),
            physicalPersonIntervalQuantityPrice: $("#PhysicalPersonIntervalQuantityPrice").val(),
            physicalPersonMoreQuantityPrice: $("#PhysicalPersonMoreQuantityPrice").val(),
            coeficient: $("#Coeficient").val(),
        },
        success: function (data) {
            location.href = '/WasteType/Index';
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
                Name: {
                    minlength: 2,
                    required: true
                },
                LessQuantity: {
                    required: true,
                    number: true
                },
                FromQuantity: {
                    required: true,
                    number: true
                },
                EndQuantity: {
                    required: true,
                    number: true
                },
                MoreQuantity: {
                    required: true,
                    number: true
                },
                MunicipalityLessQuantityPrice: {
                    required: true,
                    number: true
                },
                MunicipalityIntervalQuantityPrice: {
                    required: true,
                    number: true
                },
                MunicipalityMoreQuantityPrice: {
                    required: true,
                    number: true
                },
                LegalPersonLessQuantityPrice: {
                    required: true,
                    number: true
                },
                LegalPersonIntervalQuantityPrice: {
                    required: true,
                    number: true
                },
                LegalPersonMoreQuantityPrice: {
                    required: true,
                    number: true
                },
                PhysicalPersonLessQuantityPrice: {
                    required: true,
                    number: true
                },
                PhysicalPersonIntervalQuantityPrice: {
                    required: true,
                    number: true
                },
                PhysicalPersonMoreQuantityPrice: {
                    required: true,
                    number: true
                },
                Coeficient: {
                    required: true,
                    number: true
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
                saveWasteType(errorEditor);
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