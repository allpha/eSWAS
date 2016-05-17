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
        url: "/Landfill/Create",
        type: "POST",
        dataType: "json",
        data: {
            regionId: $("#RegionId").val(),
            name: $("#Name").val()
        },
        success: function (data) {
            //location.href = '/Landfill/Index';
            //App.unblockUI(editorName);
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
                LessQuantity : {
                    required: true,
                    number: true
                },
                //FromQuantity : {
                //    required: true
                //},
                //EndQuantity : {
                //    required: true,
                //    number: true
                //},
                //MoreQuantity : {
                //    required: true,
                //    number: true
                //},
                //MunicipalityLessQuantityPrice : {
                //    required: true,
                //    number: true
                //},
                //MunicipalityIntervalQuantityPrice : {
                //    required: true,
                //    number: true
                //},
                //MunicipalityMoreQuantityPrice : {
                //    required: true,
                //    number: true
                //},
                //LegalPersonLessQuantityPrice : {
                //    required: true,
                //    number: true
                //},
                //LegalPersonIntervalQuantityPrice : {
                //    required: true,
                //    number: true
                //},
                //LegalPersonMoreQuantityPrice : {
                //    required: true,
                //    number: true
                //},
                //PhysicalPersonLessQuantityPrice : {
                //    required: true,
                //    number: true
                //},
                //PhysicalPersonIntervalQuantityPrice : {
                //    required: true,
                //    number: true
                //},
                //PhysicalPersonMoreQuantityPrice : {
                //    required: true,
                //    number: true
                //},
                //Coeficient: {
                //    required: true,
                //    number: true
                //},


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