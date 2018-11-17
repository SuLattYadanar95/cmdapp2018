function ShowUploadAttachment(e) {
    $('#uploadTitle').text($(e).data("title"));
    $('#uploadDescription').text($(e).data("description"));
    var model = $('#ModalUploadAttachment');
    model.data("targetdiv", $(e).data('targetdiv'));
    model.data("fileid", $(e).attr('value'));
    model.data("requesttype", $(e).data('requesttype'));
    model.data("folder", $(e).data('folder'));
    model.modal('show');
}
function resetUpload() {
    $(".k-upload-files").remove();
    $(".k-upload-status").remove();
    $(".k-upload.k-header").addClass("k-upload-empty");
    $(".k-upload-button").removeClass("k-state-focused");
}

$(".files").kendoUpload({
    async: {
        saveUrl: '/Movie/UploadFile',
        removeUrl: '/Movie/DeleteFile',
        autoUpload: true,

    },
    multiple: true,
    localization: {
        select: "Select or drop your file to upload"
    },
    progress: onProgress,
    complete: onComplete,
    error: onError,
    select: onSelect,
    success: onSuccess,
    upload: onUpload
});

function onUpload(e) {
    var model = $('#ModalUploadAttachment');
    
    e.data = {
        fileId: model.data("fileid"),
        requesttype: model.data("requesttype"),
        folder: model.data("folder")
        
        
    };
    //var files = e.files;

    //$.each(files, function () {
    //    if (this.extension.toLowerCase() != ".jpg") {
    //        alert("Only .jpg files can be uploaded")
    //        e.preventDefault();
    //    }
    //});
}
function onProgress(e) {
    // Array with information about the uploaded files
    var files = e.files;

    console.log(e.percentComplete);
}
function onSuccess(e) {
    var model = $('#ModalUploadAttachment');
    var requestType = model.data("requesttype");
    var targetdiv = model.data("targetdiv");
    console.log(targetdiv);
    $.ajax({
        url: '/Movie/GetAttachments',
        data: { fileid: model.data("fileid"), requestType: requestType },
        success: function (response) {
            if (response != "Error!") {
                $('#' + targetdiv).empty().append(response);
                model.modal('hide')
            }
        }
    });
}
function onComplete(e) {
    
}

function onError(e) {

}

function onSelect(e) {
}
function getFileName(e) {
    return $.map(e.files, function (file) {

        return file.name;

    }).join("", "");
}