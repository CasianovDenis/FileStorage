$(document).ready(function () {
    $('#file_btn').click(function () {
        $('#uploadfile').click();

        $("#uploadfile").on('change', function () {
            $('#upload_input').click();
        });
        
    });
});