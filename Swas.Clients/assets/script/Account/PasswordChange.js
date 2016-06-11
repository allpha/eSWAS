function changePassword() {

    BusyIndicator.setBusy();

    $.ajax({
        url: "/Account/ChangePasswrod",
        type: "POST",
        dataType: "json",
        data: {
            oldPassword: $("#OldPassword").val(),
            newPassword: $("#NewPassword").val(),
            retryNewPassword: $("#ReNewPassword").val(),
        },
        success: function (data) {
                $("#dialogClose").click();
            BusyIndicator.unsetBusy();
            
        },
        error: function (request, status, error) {
            updateErrorEditor(request.responseText)
            BusyIndicator.unsetBusy();
        }
    });
}

function updateErrorEditor(text) {
    var form1 = $('#changePasswordForm');
    var errorEditor = $('.alert-danger', form1);

    errorEditor.text(text);
    errorEditor.append(' <button class="close" data-close="alert"></button> ');
    errorEditor.show();
}


var validation = function () {

    var handleLogin = function () {
        var form1 = $('#changePasswordForm');
        var errorEditor = $('.alert-danger', form1);

        form1.validate({
            errorElement: 'span', 
            errorClass: 'help-block help-block-error', 
            focusInvalid: false, 
            ignore: "", 
            rules: {
                OldPassword: {
                    required: true
                },
                NewPassword: {
                    minlength:6,
                    required: true
                },
                ReNewPassword: {
                    required: true,
                    minlength: 6,
                    equalTo: "#NewPassword"
                },

            },

            invalidHandler: function (event, validator) { 
                updateErrorEditor('მონაცემები არ არის ვალიდური');
            },

            highlight: function (element) { 
                $(element)
                    .closest('.form-group').addClass('has-error'); 
            },

            unhighlight: function (element) { 
                $(element)
                    .closest('.form-group').removeClass('has-error'); 
            },

            success: function (label) {
                label
                    .closest('.form-group').removeClass('has-error'); 
            },

            submitHandler: function (form) {
                errorEditor.hide();
                changePassword();
            }
        });
    }
    return {
        init: function () {
            $("#btSave").click(function () { $("#changePasswordForm").submit(); });

            handleLogin();
        }

    };

}();    

jQuery(document).ready(function () {
    BusyIndicator.init("#editorDialog");
    validation.init();

});
