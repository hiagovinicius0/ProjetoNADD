$(document).ready(function () {
    GetRolesLogado();
});
function GetRolesLogado() {
    $.ajax({
        url: 'Usuarios/GetRolesLogado',
        type: 'POST',
        success: function (res) {
            console.log(res)
            if (res === "NADD") {
                $("#MenuCadastrar").css('display', 'block');
                $('#RelatoriosNADD').css('display', 'block');
            }
            else if (res == "Coordenador") {
                $('#RelatoriosCoordenador').css('display', 'block');
            }
            else if (res == "Pró-Reitoria") {
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
                    else if (res == "Coordenador") {
                        $('#RelatoriosCoordenador').css('display', 'block');
                    }
                    else if (res == "Pró-Reitoria") {
                        $('#RelatoriosReitoria').css('display', 'block');
                    }
                }
            });
        }
    });
}