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
            if (data.ok)
                window.location = data.newurl;
            BusyIndicator.unsetBusy();
        },
        error: function (request, status, error) {
            updateErrorEditor(request.responseText)
            BusyIndicator.unsetBusy();
        }
    });
}

function updateErrorEditor(text) {
    var form1 = $('#LoginForm');
    var errorEditor = $('.alert-danger', form1);

    errorEditor.text(text);
    errorEditor.append(' <button class="close" data-close="alert"></button> ');
    errorEditor.show();
}


var Login = function () {

    var handleLogin = function () {

        $('.login-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            rules: {
                username: {
                    required: true
                },
                password: {
                    required: true
                },
            },

            messages: {
                username: {
                    required: "Username is required."
                },
                password: {
                    required: "Password is required."
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   
                $('.alert-danger', $('.login-form')).show();
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
                login();
            }
        });

        $('.login-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.login-form').validate().form()) {
                    $('.login-form').submit(); //form validation success, call ajax form submit
                }
                return false;
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

        $('#forget-password').click(function () {
            $('.login-form').hide();
            $('.forget-form').show();
        });

        $('#back-btn').click(function () {
            $('.login-form').show();
            $('.forget-form').hide();
        });
    }
    return {
        //main function to initiate the module
        init: function () {

            handleLogin();

            // init background slide images
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
