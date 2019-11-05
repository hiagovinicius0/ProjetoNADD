$(document).ready(function () {
    GetRolesLogado();
});
function GetRolesLogado() {
    $.ajax({
        url: 'Usuarios/GetRolesLogado',
        type: 'POST',
        success: function (res) {
            if (res === "NADD") {
                $("#MenuCadastrar").css('display', 'block');
                $('#RelatoriosNADD').css('display', 'block');
            }
            else if (res == "COORDENADOR") {
                $('#RelatoriosCoordenador').css('display', 'block');
            }
            else if (res == "PRO-REITORIA") {
                $('#RelatoriosReitoria').css('display', 'block');
            }
        },
        error: function () {
            $.ajax({
                url: '../Usuarios/GetRolesLogado',
                type: 'POST',
                success: function (res) {
                    if (res === "NADD") {
                        $("#MenuCadastrar").css('display', 'block');
                        $('#RelatoriosNADD').css('display', 'block');
                    }
                    else if (res == "COORDENADOR") {
                        $('#RelatoriosCoordenador').css('display', 'block');
                    }
                    else if (res == "PRO-REITORIA") {
                        $('#RelatoriosReitoria').css('display', 'block');
                    }
                }
            });
        }
    });
}