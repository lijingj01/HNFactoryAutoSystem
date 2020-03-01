
function ModelSetCenter(modelId) {
    // 将事件监听的事件改成show.bs.modal 即可解决
    $('#' + modelId).on('show.bs.modal', function (e) {
        // 关键代码，如没将modal设置为 block，则$modala_dialog.height() 为零
        $(this).css('display', 'block');
        var modalHeight = $(window).height() / 2 - $('#' + modelId + ' .modal-dialog').height() / 2;
        $(this).find('.modal-dialog').css({
            'margin-top': modalHeight
        });
    });
}


//select 数据同步
function chose_get_ini(select) {
    $(select).chosen().change(function () { $(select).trigger("liszt:updated"); });
}
//select value获取
function chose_get_value(select) {
    return $(select).val();
}
//select text获取，多选时请注意
function chose_get_text(select) {
    return $(select + " option:selected").text();
}