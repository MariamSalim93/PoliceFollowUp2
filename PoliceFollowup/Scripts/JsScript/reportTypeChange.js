$(document).ready(function () {
    $('#ReportType').change(function () {
        if ($(this).val() == "مصدر") {
            $('#SourceNumberContainer').show();
        } else {
            $('#SourceNumberContainer').hide();
        }
    });
});
