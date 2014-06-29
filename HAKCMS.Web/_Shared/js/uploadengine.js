//Enumeration for messages status
MessageStatus = {
    Success: 1,
    Information: 2,
    Warning: 3,
    Error: 4
}

//Enumeration for messages status class
MessageCSS = {
    Success: "Success",
    Information: "Information",
    Warning: "Warning",
    Error: "Error"
}

//Global variables
var intervalID = 0;
var subintervalID = 0;
var fileUpload;
var form;
var previousClass = '';

//Attach to the upload click event and grab a reference to the progress bar
function pageLoad() {
    Sys.UI.DomElement.removeCssClass($get('dvProgress'), 'progress');
    Sys.UI.DomElement.removeCssClass($get('dvFileName'), 'alert success margin');
    $addHandler($get('CP1_ucMedyaEkle_upload'), 'click', onUploadClick);
}

//Register the form
function register(form, fileUpload) {
    this.form = form;
    this.fileUpload = fileUpload;
}

//Start upload process
function onUploadClick() {
    if (fileUpload.value.length > 0) {
        var filename = fileExists();
        if (filename == '') {
            //Update the message
            Sys.UI.DomElement.addCssClass($get('dvProgress'), 'progress');
            updateMessage(MessageStatus.Information, 'Yukleniyor...', '', '0 / 0 Bytes');
            //Submit the form containing the fileupload control
            form.submit();
            //Set transparancy 20% to the frame and upload button
            //Sys.UI.DomElement.addCssClass($get('dvUploader'), 'StartUpload');
            //Initialize progressbar
            setProgress(0);
            //Start polling to check on the progress ...
            startProgress();
            intervalID = window.setInterval(function () {
                PageMethods.GetUploadStatus(function (result) {
                    if (result) {
                        setProgress(result.percentComplete);
                        //Upadte the message every 500 milisecond
                        updateMessage(MessageStatus.Information, result.message, result.fileName, result.downloadBytes);
                        if (result == 100) {
                            //clear the interval
                            window.clearInterval(intervalID);
                            clearTimeout(subintervalID);
                        }
                    }
                });
            }, 500);
        }
        else
            onComplete(MessageStatus.Error, "'<b>" + filename + "'</b> daha önce yüklenmiþ.", '', '0 / 0 Bytes');
    }
    else
        onComplete(MessageStatus.Warning, 'Dosya seçmelisiniz..', '', '0 / 0 Bytes');
}

//Stop progrss when file was successfully uploaded
function onComplete(type, msg, filename, downloadBytes) {
    window.clearInterval(intervalID);
    clearTimeout(subintervalID);
    updateMessage(type, msg, filename, downloadBytes);
    if (type == MessageStatus.Success)
    {
        Sys.UI.DomElement.removeCssClass($get('dvProgress'), 'progress');
        Sys.UI.DomElement.addCssClass($get('dvFileName'), 'alert success margin');
        setProgress(100);
    }
    //Set transparancy 100% to the frame and upload button
    
    //Refresh uploaded files list.
//    refreshFileList('<%=hdRefereshGrid.ClientID %>');
}

//Update message based on status
function updateMessage(type, message, filename, downloadBytes) {
    var _className = MessageCSS.Error;
    var _messageTemplate = $get('tblMessage');
    var _icon = $get('dvIcon');
    _icon.innerHTML = message;
    $get('dvDownload').innerHTML = downloadBytes;
    $get('dvFileName').innerHTML = filename + ' Basariyla Yuklendi..';
    switch (type) {
        case MessageStatus.Success:
            _className = MessageCSS.Success;
            break;
        case MessageStatus.Information:
            _className = MessageCSS.Information;
            break;
        case MessageStatus.Warning:
            _className = MessageCSS.Warning;
            break;
        default:
            _className = MessageCSS.Error;
            break;
    }
    _icon.className = '';
    _messageTemplate.className = '';
    Sys.UI.DomElement.addCssClass(_icon, _className);
    Sys.UI.DomElement.addCssClass(_messageTemplate, _className);
}

//Refresh uploaded file list when new file was uploaded successfully
function refreshFileList(hiddenFieldID) {
    var hiddenField = $get(hiddenFieldID);
    if (hiddenField) {
        hiddenField.value = (new Date()).getTime();
        __doPostBack(hiddenFieldID, '');
    }
}

//Set progressbar based on completion value
function setProgress(completed) {
    $get('dvProgressPrcent').innerHTML = completed + '%';
    $get('dvProgress').style.width = completed + '%';
}

//Display mouse over and out effect of file upload list
function eventMouseOver(_this) {
    previousClass = _this.className;
    _this.className = 'GridHoverRow';
}
function eventMouseOut(_this) {
    _this.className = previousClass;
}

//This will call every 200 milisecnd and update the progress based on value
function startProgress() {
    var increase = $get('dvProgressPrcent').innerHTML.replace('%', '');
    increase = Number(increase) + 1;
    if (increase <= 100) {
        setProgress(increase);
        subintervalID = setTimeout("startProgress()", 200);
    }
    else {
        window.clearInterval(subintervalID);
        clearTimeout(subintervalID);
    }
}

//This will check whether will was already exist on the server, 
//if file was already exists it will return file name else empty string.
function fileExists() {
    //var selectedFile = fileUpload.value.split('\\');
    //var file = $get('CP1_ucMedyaEkle_gvNewFiles').getElementsByTagName('a');
    //for (var f = 0; f < file.length; f++) {
    //    if (file[f].innerHTML == selectedFile[selectedFile.length - 1]) {
    //        return file[f].innerHTML;
    //    }
    //}
    return '';
}
