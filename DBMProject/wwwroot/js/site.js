// Write your JavaScript code.

/*function uploadfile() {
    var myFile = document.getElementById("projectFile");
    var formData = new FormData();
    var _url = "/Projetos/UploadProject";
    formData.append('file', $('#projectFile')[0].files[0]);

    if (myFile.files.length > 0) {
        for (var i = 0; i < myFile.files.length; i++) {
            formData.append('file-' + i, myFile.files[i]);
        }
    }

    $.ajax({
        url: _url,
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            alert("Projeto enviado com sucesso!");
        },
        error: function (jqXHR) {

        },
        complete: function (jqXHR, status) {

        }
    });
}
*/