// добавляет в переданный URL культуру и возвращает новый URL 
function getCultureUrl(url) {
   
    var currentCulture = window.location.pathname.split('/')[1];
    if (currentCulture.length === 0)
        currentCulture = "Ru";
    return '/' + currentCulture + url;
}

function createDialog() {
    return $('<div id="dialog" style="display:hidden"></div>').appendTo('body');
}

function createModal(html) {
    $(html).appendTo('body');
    $('#modal').modal();
}

function showModal(url, openCallback) {
    $('#modal').remove();
    $('<div id="modal"class="modal hide fade" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" ></div>').appendTo('body');

    $.ajax({
        cache: false,
        data: "GET",
        url: url,
        success: function (content, textStatus, request) {
            var requiredAuth = request.getResponseHeader('REQUIRES_AUTH') === '1';
            if (requiredAuth)
                return true;
            
            $('#modal').html(content);
            $('#modal').modal();
            openCallback();
        }
    });
    //$('#modal').load(url, function () {

    //    $('#modal').modal();
    //    openCallback();
    //});
}

function showDialog(dialog, url, openCallback) {
    dialog.load(url, function () {
        dialog.dialog({
            close: function () {
                closeDialog();
            },
            open: openCallback,
            modal: true,
            width: 460,
            resizable: false
        });
    });
}

function closeDialog() {
    $('#dialog').remove();
}

function deleteCharsFromString(source, charsForDelete) {
    return source.replace(charsForDelete, '');
}

function escapeCharacter(string) {
    return string.replace(".", "\\.");
}