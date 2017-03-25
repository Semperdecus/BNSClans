var total = 0;
var sort = false;

$(document).one('ready', function (total) {
    //returnrows handler return the number of members in the clan
    $("#memberCount").load("ReturnRows.ashx");

    //write number to browser
    total = $('#memberCount').html();

    //returnvar has the amount of members saved (needed for the loop)
    $.get("ReturnRows.ashx", function (returnvar) {
        for (var i = 1; i < returnvar; i++) {
            //loaddata handler creates a character object and returns this in an html row
            $.get("LoadData.ashx?value=" + encodeURIComponent(i), function (html) {
                //writes the html row from the loaddata response to element result in page
                $("#memberTable tbody").append(html);

                $("#memberTable").tablesorter({
                    headers: {
                        0: {
                            // disable sorting for first column (number)
                            sorter: false
                        },
                    }
                });
            });

            //here be data for class amount and averages etc.
        }
    });
});
