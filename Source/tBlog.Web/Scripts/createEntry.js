$(document).ready(function () {
    function split(val) {
        return val.split(/,\s*/);
    }
    function extractLast(term) {
        return split(term).pop();
    }

    $('#TagString')
        .bind("keydown", function (event) {
            if (event.keyCode === $.ui.keyCode.TAB &&
                $(this).data("autocomplete").menu.active) {
                event.preventDefault();
            }
        })
        .autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Autocomplete/GetTags",
                    data: { term: extractLast(request.term) },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return { label: item, value: item };
                        }));
                    }
                });
            },
            focus: function () {
                // prevent value inserted on focus
                return false;
            },
            select: function (event, ui) {
                var terms = split(this.value);
                // remove the current input
                terms.pop();
                // add the selected item
                terms.push(ui.item.value);
                // add placeholder to get the comma-and-space at the end
                terms.push("");
                this.value = terms.join(", ");
                return false;
            }
        });

    $('#CategoryName').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Autocomplete/GetCategories",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Name, value: item.Name };
                    }));
                }
            });
        }
    });
})