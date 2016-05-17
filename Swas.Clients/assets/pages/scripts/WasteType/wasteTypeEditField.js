/**
LessQuantityEditor editable input.
Internally value stored as {city: "Moscow", street: "Lenina", building: "15"}

@class LessQuantityEditor
@extends abstractinput
@final
@example
<a href="#" id="LessQuantityEditor" data-type="LessQuantityEditor" data-pk="1">awesome</a>
<script>
$(function(){
    $('#LessQuantityEditor').editable({
        url: '/post',
        title: 'Enter city, street and building #',
        value: {
            city: "Moscow", 
            street: "Lenina", 
            building: "15"
        }
    });
});
</script>
**/
(function ($) {
    "use strict";

    var LessQuantityEditor = function (options) {
        this.init('LessQuantityEditor', options, LessQuantityEditor.defaults);
    };

    //inherit from Abstract input
    $.fn.editableutils.inherit(LessQuantityEditor, $.fn.editabletypes.abstractinput);

    $.extend(LessQuantityEditor.prototype, {
        /**
        Renders input from tpl

        @method render() 
        **/
        render: function () {
            this.$input = this.$tpl.find('input');
        },

        /**
        Default method to show value in element. Can be overwritten by display option.
        
        @method value2html(value, element) 
        **/
        value2html: function (value, element) {
            if (!value) {
                $(element).empty();
                return;
            }
            var html = $('<div>').text(value.LessQuantity).html() + ', ' + $('<div>').text(value.MunicipalityLessQuantityPrice).html() + ' st., bld. ' + $('<div>').text(value.LegalPersonLessQuantityPrice).html();
            $(element).html(html);
        },

        /**
        Gets value from element's html
        
        @method html2value(html) 
        **/
        html2value: function (html) {
            /*
              you may write parsing method to get value by element's html
              e.g. "Moscow, st. Lenina, bld. 15" => {city: "Moscow", street: "Lenina", building: "15"}
              but for complex structures it's not recommended.
              Better set value directly via javascript, e.g. 
              editable({
                  value: {
                      city: "Moscow", 
                      street: "Lenina", 
                      building: "15"
                  }
              });
            */
            return null;
        },

        /**
         Converts value to string. 
         It is used in internal comparing (not for sending to server).
         
         @method value2str(value)  
        **/
        value2str: function (value) {
            var str = '';
            if (value) {
                for (var k in value) {
                    str = str + k + ':' + value[k] + ';';
                }
            }
            return str;
        },

        /*
         Converts string to value. Used for reading value from 'data-value' attribute.
         
         @method str2value(str)  
        */
        str2value: function (str) {
            /*
            this is mainly for parsing value defined in data-value attribute. 
            If you will always set value by javascript, no need to overwrite it
            */
            return str;
        },

        /**
         Sets value of input.
         
         @method value2input(value) 
         @param {mixed} value
        **/
        value2input: function (value) {
            if (!value) {
                return;
            }
            this.$input.filter('[name="LessQuantity"]').val(value.LessQuantity);
            this.$input.filter('[name="MunicipalityLessQuantityPrice"]').val(value.MunicipalityLessQuantityPrice);
            this.$input.filter('[name="LegalPersonLessQuantityPrice"]').val(value.LegalPersonLessQuantityPrice);
            this.$input.filter('[name="PhysicalPersonLessQuantityPrice"]').val(value.PhysicalPersonLessQuantityPrice);
        },

        /**
         Returns value of input.
         
         @method input2value() 
        **/
        input2value: function () {
            return {
                LessQuantity: this.$input.filter('[name="LessQuantity"]').val(),
                MunicipalityLessQuantityPrice: this.$input.filter('[name="MunicipalityLessQuantityPrice"]').val(),
                LegalPersonLessQuantityPrice: this.$input.filter('[name="LegalPersonLessQuantityPrice"]').val(),
                PhysicalPersonLessQuantityPrice: this.$input.filter('[name="PhysicalPersonLessQuantityPrice"]').val()
            };
        },

        /**
        Activates input: sets focus on the first field.
        
        @method activate() 
       **/
        activate: function () {
            this.$input.filter('[name="LessQuantity"]').focus();
        },

        /**
         Attaches handler to submit form in case of 'showbuttons=false' mode
         
         @method autosubmit() 
        **/
        autosubmit: function () {
            this.$input.keydown(function (e) {
                if (e.which === 13) {
                    $(this).closest('form').submit();
                }
            });
        }
    });

    LessQuantityEditor.defaults = $.extend({}, $.fn.editabletypes.abstractinput.defaults, {
        tpl: '<div ><div class="editable-address"><label><span>რაოდენობა: </span><input type="text" name="LessQuantity" class="form-control input-small"></label></div>' +
             '<div class="editable-address"><label><span>ღირებულება მუნიციპალიტეტისთვის: </span><input type="text" name="MunicipalityLessQuantityPrice" class="form-control input-small"></label></div>' +
             '<div class="editable-address"><label><span>ღირებულება იურიდიული პირისთვის: </span><input type="text" name="LegalPersonLessQuantityPrice" class="form-control input-small"></label></div>' +
             '<div class="editable-address"><label><span>ღირებულება ფიზიკური პირისთვის: </span><input type="text" name="PhysicalPersonLessQuantityPrice" class="form-control input-small"></label></div>',

        inputclass: ''
    });

    $.fn.editabletypes.LessQuantityEditor = LessQuantityEditor;

}(window.jQuery));