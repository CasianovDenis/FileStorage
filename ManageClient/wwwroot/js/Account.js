var loader;

function loadNow(opacity) {
    if (opacity <= 0) {
        displayContent();
    } else {
        loader.style.opacity = opacity;
        window.setTimeout(function () {
            loadNow(opacity - 0.05);
        }, 50);
    }
}

function displayContent() {
    loader.style.display = 'none';
    document.getElementById('content').style.display = 'block';
}

document.addEventListener("DOMContentLoaded", function () {
    loader = document.getElementById('loader');
    loadNow(1);
});



$(document).ready(function () {
    

    $("#nav-bar").show();
    $("#header").show();

    var id_file;
    var url_file;
    var type_file;
    var name_file;

    //script for get data from selected file
    $(".card").click(function () {

        id_file = $(this).attr('id');
        url_file = $(this).attr('href');
        type_file = $(this).attr('resource');
        name_file = $(this).attr('title');
    });

    //method for view data from file
    $(".open_file").click(function () {

        $(".card").hide();
        if (type_file == "mp3" || type_file == "wav") {
            $("#file_name").show();
            $("#file_name").text(name_file);
            $("#audio_controls").show();
            $("#back").show();
            $("#audio_controls").attr("src", "data:audio/" + type_file + ";base64," + url_file);
        }
        else
            if (type_file == "avi" || type_file == "mov" || type_file == "mp4" || type_file == "mpeg" || type_file == "MOV") {
                $("#file_name").show();
                $("#file_name").text(name_file);
                $("#video_controls").show();
                $("#back").show();
                $("#video_controls").attr("src", "data:video/" + type_file + ";base64," + url_file);
            }
            else
                if (type_file == "jpg" || type_file == "jpeg" || type_file == "png" || type_file == "gif") {
                    $("#file_name").show();
                    $("#file_name").text(name_file);
                    $("#image_controls").show();
                    $("#back").show();
                    $("#image_controls").attr("src", "data:image/png;base64," + url_file);
                }
                else
                    if (type_file == "pdf" ) {
                        $("#docs_controls").show();
                        $("#back").show();
                        $("#docs_controls").attr("src", "data:application/" + type_file + ";base64," + url_file);
                    }
                else {
                    $("#warning_text").show();
                    $("#back").show();
                }

    });

    //return after open file
    $("#back").click(function () {
        $(".card").show();
        $("#file_name").hide();
        $("#image_controls").hide();
        $("#audio_controls").hide();
        $("#video_controls").hide();
        $("#warning_text").hide();
        $("#docs_controls").hide();
        $("#back").hide();
    });

    //method for download selected file
    $(".download_file").click(function () {

        var a = document.createElement("a"); //Create <a>
        a.href = "data:image/" + type_file + ";base64," + url_file; //Image Base64 Goes here
        a.download = name_file; //File name Here
        a.click(); //Downloaded file
       
    });

    //method for delete file for mongodb
    $(".delete_file").click(function () {

        $("#nav-bar").hide();
        $("#header").hide();
        $(".card").hide();
        $("#content").hide(); 
        $(".footer").hide();

        $("#delete_loader").show();

        $.post("/Account/DeleteFile", { id: id_file }, function () {
           
            location.reload(true);
        });
    });

    
    //upload button method
    $('#file_btn').click(function () {

        
        $('#uploadfile').click();
 

        $("#uploadfile").on('change', function () {
            
            $("#nav-bar").hide();
            $("#header").hide();
            $(".card").hide();
            $("#content").hide();
            $(".footer").hide();

            $("#upload_loader").show();

            $('#upload_input').click();

        });
        
    });
});