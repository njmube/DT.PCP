var Pay = (function ($) {
    var selectedWay;
    var PayWay = {
        Card: 0,
        Terminal: 1
    };

    // обработка нажатия на кнопку Оплата банковскими картами
    function bindPayWayCard() {
        $(document).on('click', '#pay_way_card', function () {

            var editUrl = getCultureUrl('/pay/UserInfoConfirmation');

            showModal(editUrl, function () {
            });

            selectedWay = PayWay.Card;
        });
    }

    // обработка нажатия на кнопку Оплата терминалами
    function bindPayWayTerminal() {
        $(document).on('click', '#pay_way_terminal', function () {
            var editUrl = getCultureUrl('/pay/UserInfoConfirmation');
            showModal(editUrl, function () {
            });

            selectedWay = PayWay.Terminal;

        });
    }

    // заполняет форму для epay
    function fillEpayForm() {
        var url = getCultureUrl('/pay/FillEpayForm?' + Math.random());

        $.ajax({
            type: "GET",
            url: url,
            cache: false,
            dataType: "json",
            success: function (data) {
                $('#pay_form').find('input[name="Signed_Order_B64"]').val(data.signedString);
                $('#pay_form').find('input[name="appendix"]').val(data.appendix);
                $('#pay_form').find('input[name="email"]').val(data.email);
                $('#pay_form').submit();
            },
            async: false
        });
        return false;
    }

    function bindConfirmOrder() {
        $(document).on('click', '#pay_confirm_order', function () {
           // fillEpayForm();
        });
    }

    function bindSubmitForm() {
        $(document).on('click', '#pay_confirm_user_info', function () {
            $('#pay_user_confirm').submit();
        });
    }

    function showTooltipForValidationError() {

        $('input.input-validation-error').each(function () {

            var input = $(this);
            var data = $(input).data();
            

            for (var key in data) {
               
                if (key.indexOf('val') === 0 && key.length > 3 && key.indexOf('Pattern') === -1 && key.indexOf('Max') === -1 && key.indexOf('Min') === -1) {
                   
                    if (input.val().length === 0 && input.data('valRequired') !== 'undefined') {
                        input.data('title', data['valRequired']);
                        input.tooltip('show');
                        break;
                    }
                    if (key !== 'valRequired') {
                        input.data('title', data[key]);
                        input.tooltip('show');
                    }
                }
            }
        });
    }

    // подтверждение данных и перенапрвление в зависимости от выбранного способа оплаты
    function bindPayConfirmUserInfo() {
        $(document).on('submit', '#pay_user_confirm', function (e) {
            e.preventDefault();
            var form = $('#pay_user_confirm');
            var url = getCultureUrl('/pay/UserInfoConfirmation');

            $.ajax({
                type: "POST",
                url: url,
                data: form.serialize(),
                cache: false,
                dataType: "json",
                success: function (data) {
                    if (!data.success) {

                        $('#modal').html(data.view);
                        showTooltipForValidationError();
                    } else {
                        if (selectedWay == PayWay.Card) {
                            var confirmOrderUrl = getCultureUrl('/pay/ConfirmOrder');
                            document.location = confirmOrderUrl;
                        } else {
                            var terminalInfoUrl = getCultureUrl('/pay/TerminalPayInfo');
                            window.location = terminalInfoUrl;
                        }
                    }
                },
                async: false
            });
        });
    }

    return {
        init: function () {
            bindPayWayCard();
            bindPayWayTerminal();
            bindPayConfirmUserInfo();
            bindSubmitForm();
            bindConfirmOrder();
        }
    };



})(jQuery);

$(function () {
    Pay.init();
});