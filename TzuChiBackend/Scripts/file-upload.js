function initFileUpload(ASPSESSID, AUTHID) {
    //解決Firefox掉Session用的程式碼
    //var ASPSESSID = "@Session.SessionID";
    //var AUTHID = "@(Request.Cookies[FormsAuthentication.FormsCookieName] == null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value)";

    var width = 814;
    var height = 269;

    //js註冊使用jQuery Uploadify
    //Filedata為input的id
    //queue為div id，佇列div的 id

    //setTimeout(UploadFileFunc("CoverImageData", "CoverImageQueue", ASPSESSID, AUTHID, width, height, buttonImageUrl), 0);
    //setTimeout(UploadFileFunc("ContentImageData", "ContentQueue", ASPSESSID, AUTHID, width, height, buttonImageUrl), 0);
    //setTimeout(UploadFileFunc("ImageData", "ImageQueue", ASPSESSID, AUTHID, width, height, buttonImageUrl), 0);
    //setTimeout(UploadFileFunc("VideoData", "VideoQueue", ASPSESSID, AUTHID, width, height, buttonImageUrl), 0);

    UploadFileFunc("CoverImageData", "CoverImageQueue", ASPSESSID, AUTHID, width, height);
    UploadFileFunc("ContentImageData", "ContentQueue", ASPSESSID, AUTHID, width, height);
    UploadFileFunc("ImageData", "ImageQueue", ASPSESSID, AUTHID, width, height);
    UploadFileFunc("VideoData", "VideoQueue", ASPSESSID, AUTHID, width, height);

}


function removePicUrl(id) {
    if ($("#PicUrl_" + id + " .coverInput").val() == 'true') {
        $("#PicUrl_" + id).remove();
        $("#PicUrlListDiv .coverInput").first().val('true');
        $("#PicUrlListDiv .coverBtn").first().hide();
        $("#PicUrlListDiv .coverLabel").first().show();
    }
    else
        $("#PicUrl_" + id).remove();
}

function setPicCoverageUrl(id) {
    $("#PicUrlListDiv .coverBtn").show();
    $("#Set_" + id).hide();
    $("#PicUrlListDiv .coverLabel").hide();
    $("#Cover_" + id).show();
    $("#PicUrlListDiv .coverInput").val('false');
    $("input[name$='PicUrlList[" + id + "].CoverImage']").val('true');

}

function removeVideoUrl(id) {
    $("#VideoUrl_" + id).remove();
}

function removeCoverImage() {
    $("#CoverImageDiv").empty();
}

function removeContentImage() {
    $("#ContentImageDiv").empty();
}

function setCoveragePicPath(path) {
    $(".popupPhoto").attr("src", path);
    console.log(path);
}


function UploadFileFunc(FiledataID, QueueID, ASPSESSID, AUTHID, width, height) {

    var mfileTypeExts = "*.jpg;*.gif;*.png;*.JPG;*.GIF;*.PNG";
    var mfileTypeDesc = "Image Files (.jpg, .gif, .png)";
    var mfileSizeLimit = "1000MB";

    //判斷是否為影片
    if ("VideoData" == FiledataID) {
        mfileTypeExts = "*.mp4;*.MP4;*.Mp4";
        mfileTypeDesc = "Attachment Files (.mp4)";
        mfileSizeLimit = "1000MB";
    }

    $('#' + FiledataID).uploadify({

        swf: '/Scripts/uploadify/uploadify.swf',
        uploader: '/FileUtility/FileUpload',//使用哪個Action 做上傳

        multi: true,//true支援選擇多檔案上傳
        auto: true,//設置true，檔案選擇框，一按確定就上傳，false的話，要另外呼叫方法傳遞upload參數觸發上傳行為
        fileTypeExts: mfileTypeExts,//限制可以選擇的檔案類型
        fileTypeDesc: mfileTypeDesc,//選擇檔案時的說明
        fileSizeLimit: mfileSizeLimit,//在js端就限制檔案大小，User選擇超過大小的檔案時，就會跳出error
        queueID: QueueID,//上傳進度條呈現的地方
        queueSizeLimit: 10,//限制queue的數量
        simUploadLimit: 0,//同時上傳檔案數，0為無限
        removeCompleted: true,//檔案上傳完成時，畫面上的佇列是否消失

        fileObjName: 'Filedata',//Server端的Action，以什麼名稱接收HttpPostedFileBase物件

        onSelectError: function (file) {

            alert('選取檔案時發生錯誤');

        },

        onUploadError: function (file, errorCode, errorMsg, errorString) {
            alert('上傳失敗');
        },
        onUploadSuccess: function (file, data, response) {
            //一個佇列上傳成功時

            //data參數是Controller回傳的字串，想要Json格式的話，要再另外找Plugin把字串轉成Json物件
            //以本文範例data就是該圖片上傳後的Url字串
            //var jsondata = jQuery.parseJSON(data);

            if ("CoverImageData" == FiledataID) {

                removeCoverImage();

                $("#CoverImageDiv").append(data);

            }
            else if ("ContentImageData" == FiledataID) {


                removeContentImage();
                $("#ContentImageDiv").append(data);

            }
            else if ("ImageData" == FiledataID) {

                $("#PicUrlListDiv").append(data);

                if ($("#PicUrlListDiv .coverInput").size() == 1) {
                    $("#PicUrlListDiv .coverInput").val('true');
                    $("#PicUrlListDiv .coverBtn").hide();
                    $("#PicUrlListDiv .coverLabel").show();
                }
            }
            else if ("VideoData" == FiledataID) {

                $("#VideoUrlListDiv").append(data);
            }

        },
        onQueueComplete: function (queueData) {
            //全部佇列執行完畢時
            //alert(queueData.uploadsSuccessful + ' files were successfully uploaded.');

        },
        //解決Firefox掉Session的程式碼，ASPSESSID和AUTHID命名要和Global.asax.cs裡寫的一樣
        formData: {
            ASPSESSID: ASPSESSID,
            AUTHID: AUTHID,
            Type: FiledataID
        }
    });
}