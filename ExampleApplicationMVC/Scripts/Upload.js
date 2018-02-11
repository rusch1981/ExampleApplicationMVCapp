$(function () {
    $("#myFile").css('opacity', '0');
    $("#myButton").hide();
    $("#uploadsLabel").hide();
    $("#finishButton").hide();
    $("#myFile").on('change', myFileChanged);
    $("#myButton").click(post);

    if ($("#CaptchaError").html() !== "  ") {
        $("#CaptchaInputText").focus();
    }
});

var formData;
function myFileChanged() {
    var files = $("#myFile").get(0).files;

    if (files.length > 0 && files[0].size > 0 && files[0].name != null) {
        createApplicant(files);
        $("#fileLabel").removeClass("btn btn-default myBackgroundButton");
        $("#fileLabel").html(files[0].name);
        $("#myButton").show();
    }
    //todo may need to add warning if file is not present.  
}

function post() {
    $("#fileLabel").addClass("btn btn-default myBackgroundButton");
    $("#fileLabel").html("Choose a File");
    $("#myButton").hide();
    $("#uploadsLabel").show();
    var uploadsLength = $("#uploads").children().length;
    var fileName = $("#myFile").get(0).files[0].name;
    if (uploadsLength > 0) {
        var newLabel = "textLabel" + (uploadsLength + 1);
        $("#uploads").append('<li name="' + newLabel + '">' + fileName + '</li>');
    } else {
        $("#uploads").append('<li name="textLabel1">' + fileName + '</li>');
    }

    $.ajax({
        type: "POST",
        url: "/api/Application/",
        contentType: false,
        processData: false,
        data: formData
    });
    $("#finishButton").show();

    //https://www.codeproject.com/Articles/806075/File-Upload-using-jQuery-AJAX-in-ASP-NET-Web-API
    //send email db best practice background threaded  https://chsakell.com/2014/04/04/sending-emails-in-asp-net-do-it-right/
    //if all else fails:  https://www.codeproject.com/Articles/634146/Background-Thread-Let-me-count-the-ways
}

function createApplicant(files) {
    formData = new FormData();
    formData.append("UploadedImage", files[0]);
    formData.append("Name", $("#Name").val());
    formData.append("Age", $("#Age").val());
    formData.append("Email", $("#Email").val());
    formData.append("AboutYou", $("#AboutYou").val());
    formData.append("Experience", $("#Experience").val());
    formData.append("SkillsTalents", $("#SkillsTalents").val());
}