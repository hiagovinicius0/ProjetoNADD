function ListarUsuarios(){
    if ($.fn.DataTable.isDataTable('#tabelaUsuarios')) {
        $('#tabelaUsuarios').DataTable().destroy();
    }
    $.ajax({
        type: "POST",
        url: "../Usuarios/GetUsuarios",
        success: function (dados) {
            $("#corpoTabela").html('')
            if (dados.length > 0) {
                for (var i = 0; i < dados.length; i++) {
                    $("#corpoTabela").append("<tr><td>" + dados[i].nome_Usuario + "</td><td>" + dados[i].email_Usuario + "</td><td><a class='btn btn-success btn-xs' href='#' onclick='BuscaModal(\"" + dados[i].id_Usuario + "\", \"SHOW\")' title='Visualizar'>Detalhes</a>&nbsp;<a class='btn btn-danger  glyphicon glyphicon-remove btn-xs'  href='#' onclick='BuscaModal(\"" + dados[i].id_Usuario + "\", \"DEL\")' title='Excluir'>Excluir</a></td></tr>")
                }
            }
            $("#tabelaUsuarios").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf'
                ],
                "columnDefs": [
                    { "orderable": false, "targets": 2 }
                ],
                "language": {
                    "sEmptyTable": "Nenhum registro encontrado",
                    "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                    "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sInfoThousands": ".",
                    "sLengthMenu": "_MENU_ resultados por página",
                    "sLoadingRecords": "Carregando...",
                    "sProcessing": "Processando...",
                    "sZeroRecords": "Nenhum registro encontrado",
                    "sSearch": "Pesquisar",
                    "oPaginate": {
                        "sNext": "Próximo",
                        "sPrevious": "Anterior",
                        "sFirst": "Primeiro",
                        "sLast": "Último"
                    },
                    "oAria": {
                        "sSortAscending": ": Ordenar colunas de forma ascendente",
                        "sSortDescending": ": Ordenar colunas de forma descendente"
                    }
                }
            });
        }
    });
}
function Salvar() {
    var dataString = {
        Nome: $('#Nome').val(),
        Email: $('#Email').val(),
        Senha: $('#Senha').val()
    }
    $.ajax({
        type: "POST",
        url: "../Usuarios/Create",
        data: dataString,
        success: function (dados) {
            console.log(dados)
        }
    });
}
function BuscaModal(ID, TIPO) {
    if (TIPO === 'CAD') {
        $.ajax({
            url: 'Usuarios/Create',
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-primary" onclick="Salvar()">Cadatrar Usuário</button>')
                $('#myModal').modal('show')
            }
        });
    }
    else if (TIPO == 'SHOW') {
        $.ajax({
            url: 'Usuarios/Details/' + ID,
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>')
                $('#myModal').modal('show')
            }
        });
    }
    else if (TIPO == 'EDIT') {
        $.ajax({
            url: 'Usuarios/Edit/' + ID,
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-primary" onclick="Editar()">Editar Usuário</button>')
                $('#myModal').modal('show')
            }
        });
    }
    else if (TIPO === 'DEL') {
        $.ajax({
            url: 'Usuarios/Delete/' + ID,
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-danger" onclick="Excluir()">Excluir Usuário</button>')
                $('#myModal').modal('show')
            }
        });
    }
}
function Editar() {
    var dataString = {
        Id_Area: $('#Id_Area').val(),
        Nome_Area: $('#Nome_Area').val()
    }
    $.ajax({
        type: "POST",
        url: "Usuarios/Edit",
        data: dataString,
        success: function (dados) {
            ListarUsuarios();
            $("#myModal").modal('hide');
        }
    });
}
function Excluir() {
    var area = $('#Id_Usuario').val()
    $.ajax({
        type: "POST",
        url: "Usuarios/DeleteUser",
        data: {
            id: area
        },
        success: function (dados) {
            ListarUsuarios();
            $("#myModal").modal('hide');
        }
    });
}