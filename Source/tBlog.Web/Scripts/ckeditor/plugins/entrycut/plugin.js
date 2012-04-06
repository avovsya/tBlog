CKEDITOR.plugins.add('entrycut',
{
    init: function (editor) {
        var pluginName = 'entrycut';
        editor.addCommand(pluginName, {
            exec: function (editor) {
                editor.insertHtml('{{cut}}');
            }
        });
        editor.ui.addButton('entrycut',
            {
                label: 'Insert entry cut',
                command: pluginName,
                icon: this.path + 'images/entrycut.png'
            });
    }
});