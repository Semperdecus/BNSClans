
var rowCount = $('#memberTable tr').length;


$(document).ready(function () {
    $("#clanTable").tablesorter({
        //disable sorting first header

        headers: {
            0: {
                sorter: false
            }
        }
    });
    document.getElementById('default').checked = true;

    //choosing data to display
    $("input[type='checkbox']").change(function () {
        var val = $(this).val();
        $("#clanTable tr:first").find("th:eq(" + val + ")").toggle();
        $("#clanTable tr").each(function () {
            $(this).find("td:eq(" + val + ")").toggle();
        });
        if ($("#clanTable tr:first").find("th:visible").length > 0) {
            $("#clanTable").removeClass("noborder");
        }
        else {
            $("#clanTable").addClass("noborder");
        }
    });

    $('#loadingAnimation').hide();
    $('#loadingAnimation2').hide();
    $('#loadingAnimation3').hide();

    $('#checkboxes').show();
    $('#avatardownload').show();
});

