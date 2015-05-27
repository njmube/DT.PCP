var Cabinet = (function ($) {
    
    function bindInstructionShow() {

        $(document).on('click', '#edit_instr', function () {

            var editUrl = getCultureUrl('/help/EditInfo');
            showModal(editUrl, function () {
                $('#modal').css('width', '1056px').css('margin-left', -($('#modal').width() / 2)).css('margin-right', 'auto');
            });
        });

        $(document).on('click', '#notification_instr', function () {

            var editUrl = getCultureUrl('/help/NotificationInfo');
            showModal(editUrl, function () {
                $('#modal').css('width', '1056px').css('margin-left', -($('#modal').width() / 2)).css('margin-right', 'auto');
            });
        });

        $(document).on('click', '#load_instr', function () {

            var editUrl = getCultureUrl('/help/CheckInfo');
            showModal(editUrl, function () {
                $('#modal').css('width', '1056px').css('margin-left', -($('#modal').width() / 2)).css('margin-right', 'auto');
            });
        });
    }

    function bindSearchViolation() {
        $(document).on('click', '#violation_search', function (e) {
            var text = $("#search_query").val();
            
            grid.ApplyFilter("[PostAddress] LIKE '%" + text + "%' OR [Cost] LIKE '%" + text + "%' OR [ViolationType] LIKE '%" + text + "%' OR [FixationTime] LIKE '%"+ text + "%'");
        });
    }
    
    function bindClearViolation() {
        $(document).on('click', '#clear_search', function (e) {
            $('#search_query').val('');
            grid.ClearFilter();
        });
    }
    
    $(document).on('click', '#notification_check', function () {
        $('#form_notification_check').submit();
    });

    $(document).on('submit', '#form_notification_check', function (e) {
       
        var editUrl = getCultureUrl('/cabinet/ConfirmationNotification');
        var form = $('#form_notification_check');
        $.ajax({
            async:false,
            type: "POST",
            url: editUrl,
            data: form.serialize(),
            cache: false,
            dataType: "json",
            success: function (data) {
                if (!data.success) {
                    $('#modal').html(data.view);
                    showTooltipForValidationError();
                } else {
                    //$('#profile').html(data.view);
                    $('#modal').modal('hide');
                }
            },
        });
    });
    
    


    // обработка нажатия на вкладке
    function bindTabClick() {
        $(document).on('click', '.nav-tabs > li > a', function (e) {
            e.preventDefault();
           
            $('.nav > li').removeClass('active');
            $(this).parent().addClass('active');
            var tabUrl = $(this).attr('href');
            var isExistParam = RegExp('payed=' + '(.+?)(&|$)').exec(tabUrl) || [, null][1];
           
            $.ajax({
                cache: false,
                type: "GET",
                url: tabUrl,
                success: function (content) {
                    $('#tab_content').html(content);
                   
                    if (isExistParam != null)
                        $('#pay_penalty').hide();
                    else
                        $('#pay_penalty').show();
                }
            });

            //$.get(tabUrl, function (content) {
            //    $('#tab_content').html(content);
            //});
        });
    }

    // показ фомры для редактирования данных во всплывающем окне
    function bindEditInfoClick() {
        $(document).on('click', '#edit_info', function () {
            var editUrl = getCultureUrl('/cabinet/edit');
            showModal(editUrl, function () {
                $('#PhoneNumber').mask("7 (999) 999-9999");
                $('#modal').css('width', '540px').css('margin-left', -($('#modal').width() / 2)).css('margin-right', 'auto');
            });
        });
    }

    function showTooltipForValidationError() {

        $('input.input-validation-error').each(function () {
            var input = $(this);
            var data = $(input).data();

            for (var key in data) {
                if (key.indexOf('val') === 0 && key.length > 3 && key.indexOf('Pattern') === -1) {

                    input.data('title', data[key]);
                    input.tooltip('show');

                }
            }
        });
    }

    function bindPayFromDetail() {
        $(document).on('click', '#pay_from_detail', function () {
            var orderNumbers = $('#detail_order_number').html();
            var url = getCultureUrl('/pay/payway?orderNumbers=' + orderNumbers);
            document.location = url;
        });

    }

    function bindPayPenaltyClick() {
        $(document).on('click', '#pay_penalty', function () {
            
            var orderNumbers = [];
            $('input:checkbox:checked').each(function () {
                orderNumbers.push($(this).attr('name'));
            });

            if (orderNumbers.length == 0)
                return;
            
            orderNumbers.toString();
            var url = getCultureUrl('/pay/payway?orderNumbers=' + orderNumbers);
            document.location = url;
        });
    }

    // загрузка личных данных с сервиса
    function bindLoadFromService() {
        $(document).on('click', '#load_info', function () {

            var editUrl = getCultureUrl('/cabinet/LoadFromService');

            showModal(editUrl, function () {
                $('#PhoneNumber').mask("7 (999) 999-9999");
                $('#modal').css('width', '540px').css('margin-left', -($('#modal').width() / 2)).css('margin-right', 'auto');
            });

        });
    }

    function bindSearchByEnterKey() {
        $(document).on('keyup', '#search_query', function(event) {
            if (event.keyCode == 13) {
                $("#violation_search").click();
            }
        });
        
    }

    // подписка/отписка на уведомления
    function bindNotification() {
        $(document).on('click', '#subscribe', function (e) {
            var editUrl = getCultureUrl('/cabinet/Notification');
            showModal(editUrl, function () {
                $('#Phone').mask("7 (999) 999-9999");
                $('#modal').css('width', '500px').css('margin-left', -($('#modal').width() / 2)).css('margin-right', 'auto');
            });
        });
    }

    // отправка формы подписки/отписки
    function bindMakeSubmitSubscribeForm() {
        $(document).on('click', '#subscribe_confirm', function () {
            $('#subscribe_form').submit();
        });
    }

    // перехват отправки формы подписки/отписки
    function bindSubmitSubscribeForm() {
        $(document).on('submit', '#subscribe_form', function (e) {
            e.preventDefault();

            var phoneNumber = $('#subscribe_form').find('input[name="Phone"]').val();
            phoneNumber = deleteCharsFromString(phoneNumber, /[-()\s]/gi);
            $('#subscribe_form').find('input[name="Phone"]').val(phoneNumber);
           
            var editUrl = getCultureUrl('/cabinet/notification');
            var form = $('#subscribe_form');
            var isShowNotificationCheck;
            $.ajax({
                async:false,
                type: "POST",
                url: editUrl,
                data: form.serialize(),
                cache: false,
                dataType: "json",
                success: function (data) {
                    if (!data.success) {
                        $('#modal').html(data.view);
                        $('#Phone').mask("7 (999) 999-9999");
                        showTooltipForValidationError();
                    } else {
                        $('#profile').html(data.view);
                        $('#modal').modal('hide');
                        isShowNotificationCheck = data.showNotificationCheck;
                    }
                },
            });
            
            if (isShowNotificationCheck)
                showNotificationCheck();
        });
    }
    
    // обрабатывает клик на строке грида и открывает детальный просмотр
    $(document).on('click', '.dxgvDataRow', function (e) {
       
        var input = $(this).find('input:checkbox:first')[0];
        var id = $(input).attr('name');
        var isPayed = $(input).attr('disabled');
        var editUrl = getCultureUrl('/cabinet/details?orderNumber=' + id);
        showModal(editUrl, function () {
            if (isPayed === "disabled")
                $('#pay_from_detail').hide();
            $('#modal').addClass('modal-violation');
            $('#modal').css('width', '935px').css('margin-left', -($('#modal').width() / 2)).css('margin-right', 'auto');
            //loadViolationImage(id);

        });
    });
    
    $(document).on('click', 'input:checkbox', function (e, s) {
      
        var row = $(this).closest('.dxgvDataRow');
        if (e.target.checked)
            $(row).addClass('violation-row');
        else
            $(row).removeClass('violation-row');
        e.stopPropagation();
       
    });

    function bindShowEditInstruction() {
        $(document).on('click', '#show_edit_instr', function () {

            var editUrl = getCultureUrl('/cabinet/EditInstruction');
            showModal(editUrl, function () {

                $('#modal').css('width', '1024px').css('margin-left', -($('#modal').width() / 2)).css('margin-right', 'auto');
            });
        });
    }

    function showNotificationCheck() {
        var editUrl = getCultureUrl('/cabinet/ConfirmationNotification');
        showModal(editUrl, function () {
           
            $('#modal').css('width', '500px').css('margin-left', -($('#modal').width() / 2)).css('margin-right', 'auto');
        });
    }
    


    // перехват отправки формы редактирования данных
    function bindSubmitEditInfoForm() {
        $(document).on('submit', '#update_user_info', function (e) {
            e.preventDefault();
            var form = $('#update_user_info');

            var phoneNumber = $('#update_user_info').find('input[name="PhoneNumber"]').val();
            phoneNumber = deleteCharsFromString(phoneNumber, /[-()\s]/gi);
            $('#update_user_info').find('input[name="PhoneNumber"]').val(phoneNumber);

            var editUrl = getCultureUrl('/cabinet/edit');
            var isShowNotificationCheck;
            $.ajax({
                async:false,
                type: "POST",
                url: editUrl,
                data: form.serialize(),
                cache: false,
                dataType: "json",
                success: function (data) {
                    if (!data.success) {
                        $('#modal').html(data.view);
                        $('#PhoneNumber').mask("7 (999) 999-9999");
                        showTooltipForValidationError();
                    } else {
                        $('#profile').html(data.view);
                        $('#modal').modal('hide');
                        isShowNotificationCheck = data.showNotificationCheck;
                    }
                },
            });
           
            if (isShowNotificationCheck)
                showNotificationCheck();
        });
    }

    return {
        init: function () {
            bindTabClick(),
            bindEditInfoClick(),
            bindSubmitEditInfoForm();
            bindLoadFromService();
            bindPayFromDetail();
            bindPayPenaltyClick();
            bindNotification();
            bindSubmitSubscribeForm();
            bindMakeSubmitSubscribeForm();
            bindShowEditInstruction();
            bindInstructionShow();
            bindSearchViolation();
            bindClearViolation();
            bindSearchByEnterKey();

        }
        
    };
})(jQuery);

$(function () {
    Cabinet.init();
    

});

//function onRowClick(s, e) {
//    debugger;
//    var originalTarget = e.htmlEvent.originalTarget;

//    if (originalTarget.nodeName.toLowerCase() != 'input') {

//        var r = $(originalTarget).closest('.templateTable').find('input:checkbox:first')[0];
//        var id = $(r).attr('name');
//        var isPayed = $(r).attr('disabled');
//        var editUrl = getCultureUrl('/cabinet/details?orderNumber=' + id);
//        showModal(editUrl, function () {
//            if (isPayed === "disabled")
//                $('#pay_from_detail').hide();
//            $('#modal').addClass('modal-violation');
//            $('#modal').css('width', '935px').css('margin-left', -($('#modal').width() / 2)).css('margin-right', 'auto');
//            loadViolationImage(id);

//        });
//    }

//}

function loadViolationImage(orderNumber) {
    $.ajax({
        type: 'GET',
        url: '/Ru/Cabinet/GetViolationImage',
        data: { orderNumber: orderNumber },
        async: false,
        success: function (data) {
            debugger;
            if (data != '') {
                var vImg = $('#violation_info');
                vImg.attr('src', data.Image);
            }
        }
    });
    

}
