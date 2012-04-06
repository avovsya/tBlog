$(document).ready(function () {
    $('section#comments div.pager a').live('click', function (e) {
        e.preventDefault();
        var href = $(this).attr('href');
        if (href) {
            $.get(href, function (data) {
                $('#comment-container').html(data);
                window.showRemoveButtons();
            });
        }
    });
});