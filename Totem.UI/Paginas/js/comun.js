function msgRespuesta(message, type) {

    if (type == 'success') {
        msgSuccess(message);
    }

    if (type == 'error') {
        msgError(message);
    }

    if (type == 'warning') {
        msgWarning(message);
    }
}


function msgError(message) {
    $.toast({
        heading: 'Error',
        text: message,
        icon: 'error',
        loader: false,
        showHideTransition: 'slide',
        hideAfter: 4000,
        position: 'bottom-right'

    })
}

function msgSuccess(message) {
    $.toast({
        heading: 'Operacion exitosa',
        text: message,
        icon: 'success',
        loader: false,
        showHideTransition: 'slide',
        hideAfter: 4000,
        position: 'bottom-right'

    })
}

function msgWarning(message) {
    $.toast({
        heading: 'Operacion exitosa',
        text: message,
        icon: 'warning',
        loader: false,
        showHideTransition: 'slide',
        hideAfter: 4000,
        position: 'bottom-right'

    })
}


function msgLoginError(message) {
    $.toast({
        heading: 'Error',
        text: "<br>"+message,
        icon: 'error',
        loader: false,
        showHideTransition: 'slide',
        hideAfter: 4000,
        position: 'top-center'

    })
}

function msgSuccessExcel(message) {
    $.toast({
        heading: 'Operación exitosa',
        text: message,
        icon: 'success',
        loader: false,
        showHideTransition: 'slide',
        hideAfter: 4000,
        position: 'bottom-right'
    })

    $("#cargando").hide();
}
