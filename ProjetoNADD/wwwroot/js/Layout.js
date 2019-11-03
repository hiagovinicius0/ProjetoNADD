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
            }
            else if (res == "COORDENADOR") {

            }
            else if (res == "PRO-REITORIA") {

            }
        }
    });
}