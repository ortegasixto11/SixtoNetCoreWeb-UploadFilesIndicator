function submitFormUploadFile(url, input_file, id_tag_html_indicator) {

    var xhr = new XMLHttpRequest()

    xhr.upload.onprogress = function (e) {
        if (e.lengthComputable) {
            var percentComplete = parseInt((e.loaded / e.total) * 100);
            updateValueUploadIndicator(id_tag_html_indicator, percentComplete)
            console.log(percentComplete + '% uploaded')
        }
    };

    xhr.upload.onloadstart = function (e) {
        console.log('Uploaded Start')
    }

    xhr.upload.onloadend = function (e) {
        console.log('Uploaded End')
    }

    xhr.upload.onerror = function (e) {
        updateValueUploadIndicator(id_tag_html_indicator, 'ERROR')
        console.log(e)
    }

    xhr.onload = function () {
        if (this.status == 200) {
            updateValueUploadIndicator(id_tag_html_indicator, 'Success')
            console.log('Uploaded Completed')
        };
    };

    xhr.open('POST', url, true)
    var formData = new FormData()
    for (var i = 0; i < input_file.files.length; i++) {
        formData.append("files", input_file.files[i])
    }
    xhr.send(formData)
}

function updateValueUploadIndicator(id_tag_html_indicator, valueHtml) {
    document.getElementById(id_tag_html_indicator).innerHTML = valueHtml
}