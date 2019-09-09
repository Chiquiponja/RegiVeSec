// Parameters for this function (IdInput,IdBotonSubir,IdImgFoto,NombreEntidad,TamañoImagen,RequiereImagenBoolean)
function UploadFile(inputFileId, uploadButtonId, photoImgId, entityName, width, requireImageFile) {

  vm.$data.p_ErrorMessage = "";

  //if (document.getElementById(uploadButtonId)) {
  //  document.getElementById(uploadButtonId).disabled = true;
  //} else {
  //  vm.$data.p_ErrorMessage = window.validate_UploadButtonIdNotFounded;
  //  enableUploadButton();
  //  return;
  //}



  if ($("input[name=DetalleArchivo]").val() === "") {
    vm.$data.p_ErrorMessage = window.validate_PutDescriptionFile;
    enableUploadButton();
    return;
  }

  vm.$data.p_ErrorMessage = "";

  //if ($("#" + inputFileId).val() === "") {
  //  vm.$data.p_ErrorMessage = window.validate_SelectFileToUpload;
  //  enableUploadButton();
  //  return;
  //}

  var file = ($("#" + inputFileId))[0].files[0];
  var fileName = file.name.toLowerCase();

  var isImageFile = false;
  if (EndsWith(fileName, ".jpg") ||
    EndsWith(fileName, ".png") ||
    EndsWith(fileName, ".gif") ||
    EndsWith(fileName, ".jpeg")) {
    isImageFile = true;
  }

  if (requireImageFile && requireImageFile === true) {
    if (!isImageFile) {
      vm.$data.p_ErrorMessage = "requiere una imagen";
      enableUploadButton();
      return;
    }
  }

  var name = entityName + "_" + uuidv4();
  var formData = new FormData();

  if (file.size === 0) {
    vm.$data.p_ErrorMessage = window.validate_FileSizeMinimum;
    enableUploadButton();
    return;
  }
  if (file.size > 8388608) {
    vm.$data.p_ErrorMessage = window.validate_FileSizeGreatherThanEigthMB;
    enableUploadButton();
    return;
  }

  // Subimos el archivo
  formData.append("name", name);
  formData.append("file", file);
  //Mostramos el loader hasta que se complete el AJAX
  if (photoImgId) {
    $(photoImgId).attr("src", "/assets/images/Rolling.gif");
  }
  $.ajax({
    url: "/api/UploadFile/Add",
    type: "POST",
    data: formData,
    contentType: false,
    processData: false,
    success: function (d) {
      // Borramos el Input file
      $("#" + inputFileId).val(null);

      // Obtenemos la extension
      var re = /(?:\.([^.]+))?$/;
      var ext = re.exec(file.name)[1];

      var filePath = "/UploadedFiles/" + name + "." + ext;

      // Si es es una imagen, agregamos el query string para redimiensionar la imagen
      if (isImageFile) {
        filePath = filePath + "?width=" + width + "&rmode=lanczos3";
        vm.$data.vehiculo.foto = filePath;
      }

      //Habilito el boton UpLoadFile
      enableUploadButton();
      // Y mandamos a actualizar la interface de usuario 
     // UpdateFile(entityName, filePath);

    },

    

  });


  function enableUploadButton() {
    if (document.getElementById(uploadButtonId)) {
      document.getElementById(uploadButtonId).disabled = false;
    }
  }
}
function uuidv4() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
}
function EndsWith(str, suffix) {
  return str.slice(-suffix.length) === suffix;
};
