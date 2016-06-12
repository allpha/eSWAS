function updateErrorEditor(editor, text) {

    var errorEditor = $(editor);

    errorEditor.text(text);
    errorEditor.append(' <button class="close" data-close="alert"></button> ');
    errorEditor.show();
}

function hideErrorEditor(editor, text) {
    var errorEditor = $(editor);
    errorEditor.hide();
}

function updataErrorEditorByValidatorError(editor, errorList) {
    if (errorList != null) {
        var errorEditor = $(editor);

        errorEditor.text("");

        errorEditor.append(' <button class="close" data-close="alert"></button> ');
        var errorText = "";

        for (var i = 0; i < errorList.length; i++)
            errorEditor.append(errorList[i].element.placeholder + " - " + errorList[i].message + "<br />");

        errorEditor.show();
    }
}


var Login = function () {

    var handleLogin = function () {

        $('.login-form').validate({
            errorElement: 'span',
            errorClass: 'help-block',
            focusInvalid: false,
            rules: {
                username: {
                    required: true
                },
                password: {
                    required: true
                },
            },

            invalidHandler: function (event, validator) {
                updataErrorEditorByValidatorError("#errorEditor", validator.errorList);
            },

            highlight: function (element) {
                $(element)
                    .closest('.form-group').addClass('has-error');
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function (form) {
                login();
            }
        });

        $('.login-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.login-form').validate().form()) {
                    $('.login-form').submit();
                }
                return false;
            }
        });

        $('.forget-form').validate({
            errorElement: 'span',
            errorClass: 'help-block',
            focusInvalid: false,
            rules: {
                newPassword: {
                    minlength: 6,
                    required: true
                },
                reNewPassword: {
                    minlength: 6,
                    equalTo: "#newPassword",
                    required: true
                },
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   
                updataErrorEditorByValidatorError("#changePasswordErrorEditor", validator.errorList);
            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function (form) {
                changePassword();
            }
        });

        $('.forget-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.forget-form').validate().form()) {
                    $('.forget-form').submit();
                }
                return false;
            }
        });


        $('#back-btn').click(function () {
            hideErrorEditor("#errorEditor");
            $('.login-form').show();
            $('.forget-form').hide();
        });

    }
    return {
        init: function () {
            $('.forget-form').hide();
            handleLogin();

            $('.login-bg').backstretch([
                "../assets/css/IMG_7370.jpg"
            ], {
                fade: 1000,
                duration: 8000
            }
            );
        }

    };

}();

jQuery(document).ready(function () {
    BusyIndicator.init("#MainForm");
    Login.init();
});


function login() {

    BusyIndicator.setBusy();

    $.ajax({
        url: "/Account/LogIn",
        type: "POST",
        dataType: "json",
        data: {
            userName: $("#username").val(),
            password: $("#password").val(),
        },
        success: function (data) {
            if (!data.ok && data.needChangePassword) {
                $('.login-form').hide();
                $('.forget-form').show();
            } else
                if (data.ok && !data.needChangePassword)
                    window.location = data.newurl;

            BusyIndicator.unsetBusy();
        },
        error: function (request, status, error) {
            updateErrorEditor("#errorEditor", request.responseText)
            BusyIndicator.unsetBusy();
        }
    });
}

function changePassword() {

    BusyIndicator.setBusy();

    $.ajax({
        url: "/Account/FirstLoginChangePasswrod",
        type: "POST",
        dataType: "json",
        data: {
            newPassword: $("#newPassword").val(),
            retryNewPassword: $("#reNewPassword").val(),
        },
        success: function (data) {
            if (data.ok && !data.needChangePassword)
                window.location = data.newurl;

            BusyIndicator.unsetBusy();

        },
        error: function (request, status, error) {
            updateErrorEditor("#changePasswordErrorEditor", request.responseText)
            BusyIndicator.unsetBusy();
        }
    });
}
