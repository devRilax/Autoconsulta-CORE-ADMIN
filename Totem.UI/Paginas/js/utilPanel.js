var panel = {
    init: function () {
        panel.setup();
        $("#cargando").hide();
    },
    setup: function () {
        $("#btnCargaExcel").on('click', panel.activateLoading);
    },
    activateLoading: function () {
        $("#cargando").show();
    }
};
$(document).ready(panel.init);