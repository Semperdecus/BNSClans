﻿var total = 0;
var calcTotals = true;
var rowCount = $('#memberTable tr').length;
var loadingPercentage = 0;

document.getElementById('default').checked = true;

$(document).one('ready', function (total) {
    $('#checkboxes').hide();
    $('#memberAvatars').hide();
    $('#avatardownload').hide();

    //returnvar has the amount of members saved (needed for the loop)
    $.get(location.protocol + "//" + location.host + "/scripts/handlers/ReturnRows.ashx", function (returnvar) {
        for (var i = 0; i < returnvar; i++) {
            //loaddata handler creates a character object and returns this in an html row
            $.get(location.protocol + "//" + location.host + "/scripts/handlers/LoadData.ashx?value=" + encodeURIComponent(i + 1), function (html) {
                //writes the html row from the loaddata response to element result in page
                $("#memberTable tbody").append(html);
                $("#memberCount").text(returnvar);

                rowCount = $('#memberTable tr').length;
                loadingPercentage = ((100 / returnvar) * rowCount);
                if (rowCount < 11) {
                    $("#numberTable tbody").append("<tr><td class=\"number\">" + "0" + (rowCount - 1) + "</td></tr>");
                    $('#loading').width(loadingPercentage + '%');
                }
                else {
                    $("#numberTable tbody").append("<tr><td class=\"number\">" + (rowCount - 1) + "</td></tr>");
                    $('#loading').width(loadingPercentage + '%');
                }

            });
        }
    });
});

//functions after data has been loaded in
var ajaxCount = 0;
$(document).ajaxStop(function () {

    ajaxCount += 1;

    if (ajaxCount == 1)
    {
        //get average ap
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=ap", function (html) {
            $("#avgAP").text(html);
        });

        //get average level
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=level", function (html) {
            $("#avgLevel").text(html);
        });

        //get average score
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=score", function (html) {
            $("#avgScore").text(html);
        });

        //get server
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=server", function (html) {
            $("#server").text(html);
        });

        //get chokma users
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=chokma", function (html) {
            $("#chokma").text(html);
        });

        //count true souls
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=truesoul", function (html) {
            $("#truesoul").text(html);
        });

        //count unleashed pets
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=unleashedpet", function (html) {
            $("#unleashedpet").text(html);
        });

        //count hongmoon level 15+
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=maxlevel", function (html) {
            $("#maxlevel").text(html);
        });

        //get classes
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=sin", function (html) {
            $("#sinCount").text(html);
        });
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=bd", function (html) {
            $("#bdCount").text(html);
        });
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=bm", function (html) {
            $("#bmCount").text(html);
        });
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=des", function (html) {
            $("#desCount").text(html);
        });
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=fm", function (html) {
            $("#fmCount").text(html);
        });
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=kfm", function (html) {
            $("#kfmCount").text(html);
        });
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=sf", function (html) {
            $("#sfCount").text(html);
        });
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=sum", function (html) {
            $("#sumCount").text(html);
        });
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=wl", function (html) {
            $("#wlCount").text(html);
        });
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=errors", function (html) {
            
            $("#errorLoading").text(html);
        });
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=avatars", function (html) {
            $("#memberAvatars").append(html);
        });
        $('#loading').width('0%');

        ajaxCount += 1;
    }

    if (ajaxCount == 2) {
        //update database clan data
        $.get(location.protocol + "//" + location.host + "/scripts/handlers/ClanSummaryData.ashx?value=updateDatabase", function (html) {
            $("#dataBaseStatus").text(html);
        });

        $("#memberTable").tablesorter({
            //disable sorting first header
            headers: {
                0: {
                    sorter: false
                }
            }
        });

        //choosing data to display
        $("input[type='checkbox']").change(function () {
            var val = $(this).val();
            $("#memberTable tr:first").find("th:eq(" + val + ")").toggle();
            $("#memberTable tr").each(function () {
                $(this).find("td:eq(" + val + ")").toggle();
            });
            if ($("#memberTable tr:first").find("th:visible").length > 0) {
                $("#memberTable").removeClass("noborder");
            }
            else {
                $("#memberTable").addClass("noborder");
            }
        });

        $('#loadingAnimation').hide();
        $('#loadingAnimation2').hide();
        $('#loadingAnimation3').hide();

        $('#checkboxes').show();
        $('#avatardownload').show();
    }
});

//clicking the avatar button
$("#showAvatar").click(function () {
    if ($('#memberData').is(":hidden")) {
        $(this).css("background-color", "#e7e7ee");

        $('#memberData').show();
        $('#memberAvatars').hide();
    }
    else if ($('#memberAvatars').is(":hidden")) {
        $('#memberAvatars').show();
        $('#memberData').hide();
        $(this).css("background-color", "#f6f6f6");

    }
});
