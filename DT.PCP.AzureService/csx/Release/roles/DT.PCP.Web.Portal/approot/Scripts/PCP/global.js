$(document).ready(function() {
    $('input, textarea').placeholder();
    
    $(document).ajaxComplete(function (event, request, settings) {
        if (request.getResponseHeader('REQUIRES_AUTH') === '1') {
            var editUrl = getCultureUrl('/cabinet/AuthRequired');
            showModal(editUrl, function() {
            });

        };
    });
})
