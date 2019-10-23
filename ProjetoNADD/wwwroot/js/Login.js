function Login() {
    var dataString = {
        Nome_Usuario: $('#inputEmail').val(),
        Senha_Usuario: $('#inputPassword').val()
    }
    $.ajax({
        type: "POST",
        url: "../Token/RequestToken",
        data: dataString,
        success: function (dados) {
            console.log(dados)
        }
    });
}