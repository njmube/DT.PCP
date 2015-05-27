$(document).ready(function () {
   
    $(document).on('click', '#show_login_instr', function () {

        var editUrl = getCultureUrl('/help/LoginInfo');
        showModal(editUrl, function () {
            $('#modal').css('width', '1056px').css('margin-left', -($('#modal').width() / 2)).css('margin-right', 'auto');
        });
    });


    $(document).on('click', '#cabinet', function (e) {
        
        if ($.trim($('#car_number').val()).length > 0 && $.trim($('#car_passport_number').val()).length > 0) {
            return makeLogin();
        }
        
        if ($.trim($('#car_number_1').val()).length > 0 && $.trim($('#car_order_number').val()).length > 0) {
            return makeLoginByOrder();
        }
    });

    function makeLoginByOrder() {
        var editUrl = getCultureUrl('/account/LoginByOrder');
        var redirectUrl = getCultureUrl('/cabinet/violationList');

        $.ajax({
            type: "POST",
            url: editUrl,
            data: { carNumber: $('#car_number_1').val(), orderNumber: $('#car_order_number').val() },
            cache: false,
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    window.location = redirectUrl;
                } else {
                    createModal(data.message);
                }
            },
        });
        return true;

    }
   

    function makeLogin() {
        var isValid = true;
        $('#car_passport_number').tooltip('hide');
        $('#car_number').tooltip('hide');
        if ($('#car_number').val().length == 0) {
            isValid = false;
            $('#car_number').tooltip('show');
        }

        if ($('#car_passport_number').val().length == 0) {
            isValid = false;
            $('#car_passport_number').tooltip('show');
        }

        if (!isValid) return false;

        var editUrl = getCultureUrl('/account/login');
        var redirectUrl = getCultureUrl('/cabinet/violationList');

        $.ajax({
            type: "POST",
            url: editUrl,
            data: { carNumber: $('#car_number').val(), passportNumber: $('#car_passport_number').val()},
            cache: false,
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    window.location = redirectUrl;
                } else {
                    createModal(data.message);
                }
            },
        });
        return true;
    }

})