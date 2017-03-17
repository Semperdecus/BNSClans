
$(document).one('ready', function () {
    $("#result").load("LoadData.ashx");
});

$(function () {
    $("#memberCount").load("LoadMemberCount.ashx");
});