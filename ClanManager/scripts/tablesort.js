$(document).ready(function () {
    $('#memberTable').tablesorter()
    $("#trigger-link").click(function () {
        // set sorting column and direction, this will sort on the first and third column the column index starts at zero 
        var sorting = [[0, 0], [2, 0]];
        // sort on the first column 
        $("table").trigger("sorton", [sorting]);
        // return false to stop default link action 
        return false;
    });
});