var login = {
    
    init: function () {
        login.setupButtons();
    },
    setupButtons: function () {
        $("#btnLogeo").on("click", login.fnClickLogin);
    },
    fnClickLogin: function() {
        if (login.fnValidarInput()) {
            login.fnLogin();
        }
    },
    fnLogin: function () {

        debugger;
        var params = JSON.stringify({ username: $("#txtUsermane").val(), password: $("#txtPassword").val() });

        $.ajax({
            type: "POST",
            url: "/Paginas/Login.aspx/validarUsuario",
            data: params,
            contentType: "application/json; charset=utf-8",  
            dataType: "json",
            success: login.fnLoginSuccess,
            error: function (request, status, error) {
                console.log(status + " " + error + " " + request.responseText);
            }
        });
    },
    fnLoginSuccess: function (data) {
  
        var obj = jQuery.parseJSON(data.d);
        
        if (obj.status == "error") {
            comun.msgError('idError', obj.msg, 'containerMsg');
        } else {
            window.location.href = '/Paginas/PanelAdmin.aspx';
        }
    },
    fnLoginError: function (request, status, error) {
        console.log(status + " " + error + " " + request.responseText);
    },
    fnValidarInput: function () {

        var usr = $("#txtUsermane").val();
        var pw = $("#txtPassword").val();
        var isValid = true;
        var msg = '';

        if (usr == '') {
            isValid = false;
            msg = "Debe ingresar un nombre de usuario";
        } else if (pw == '') {
            isValid = false;
            msg = "Debe ingresar una contraseña";
        }

        if (!isValid) {
            comun.msgError('idError',msg, 'containerMsg');
        }
        
        return isValid;
    }
};
$(document).ready(login.init);