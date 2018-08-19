$(function () {
    var postUrl = $("#url").val();
    if (postUrl) {
        // this is post detail page
        anchors.options = {
            placement: 'left'
        };
        anchors.add('#post_detail h3');
        anchors.add('#post_detail h4');
        hljs.initHighlightingOnLoad();

        $.post(postUrl);
    } else {
        var select = $('select');
        if (select) {
            // this is category or archive page
            select.selectize({
                sortField: [
                    {
                        field: 'text',
                        direction: 'desc'
                    }
                ],
                onChange: function (value) {
                    window.location.href = value;
                }
            });
        }
    }
});