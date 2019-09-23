function openModalEditar(idSeccion) {

    $.ajax({
        //url: '@Url.Action("GetSeccion", "Cuenta")',
        url: 'GetSeccion',
        type: 'POST',
        data: { id: idSeccion },
        success: function (data) {
            console.log(data);
            $("#textTituloEditarSeccion").val(data.Titulo);


            $("#summernoteEditarContenidoSeccion").summernote('code',data.Contenido);

            //$("#textContenidoEditarSeccion").val(data.Contenido);
            $("#textIdEditarSeccion").val(data.Id);
        },
        error: function (data) {
            console.log(data);
        }
    });

    $('#modalEditarSeccion').modal();
}

function openModalEditarImagen(idImagen) {

    $.ajax({
        //url: '@Url.Action("GetSeccion", "Cuenta")',
        url: 'GetImagen',
        type: 'POST',
        data: { id: idImagen },
        success: function (data) {
            console.log(data);
            $("#textTituloEditarSeccion").val(data.Titulo);


            $("#summernoteEditarContenidoSeccion").summernote('code', data.Contenido);

            //$("#textContenidoEditarSeccion").val(data.Contenido);
            $("#textIdEditarSeccion").val(data.Id);
        },
        error: function (data) {
            console.log(data);
        }
    });

    $('#ModalEditarImagen').modal();
}


function saveSeccion() {
    var data = $('#summernoteEditarContenidoSeccion').summernote('code');
    console.log(data);
    var strParams = {
        Id: $('#textIdEditarSeccion').val(),
        Titulo: $('#textTituloEditarSeccion').val(),       
        Contenido: data
    };
    $.ajax({
        url: 'SaveSeccion',
        type: 'POST',
        data: { model: strParams },
        success: function (data) {
            $('#modalEditarSeccion').modal('hide');
            setTimeout("location.reload(true);", 2000);

        },
        error: function (data) {
            console.log(data);
        }
    });
}
