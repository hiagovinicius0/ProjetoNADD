function ListarAreas() {
    if ($.fn.DataTable.isDataTable('#tabelaAreas')) {
        $('#tabelaAreas').DataTable().destroy();
    }
    $.ajax({
        type: "POST",
        url: "../Areas/GetAreas",
        success: function (dados) {
            if (dados.length > 0) {
                $("#corpoTabela").html('')
                for (var i = 0; i < dados.length; i++) {
                    $("#corpoTabela").append("<tr><td>" + dados[i].nome_Area + "</td><td><a class='btn btn-success btn-xs' href='#' onclick='BuscaModal(" + dados[i].id_Area + ", \"SHOW\")' title='Visualizar'>Detalhes</a>&nbsp;<a class='btn btn-warning btn-xs glyphicon glyphicon-pencil' href='#' onclick='BuscaModal(" + dados[i].id_Area + ", \"EDIT\")' title='Editar'>Editar</a>&nbsp;<a class='btn btn-danger  glyphicon glyphicon-remove btn-xs'  href='#' onclick='BuscaModal(" + dados[i].id_Area + ", \"DEL\")' title='Excluir'>Excluir</a></td></tr>")
                }
            }
            $("#tabelaAreas").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf'
                ],
                "columnDefs": [
                    { "orderable": false, "targets": 1 }
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
        Nome_Area: $('#myModal #Nome_Area').val()
    }
    $.ajax({
        type: "POST",
        url: "../Areas/Create",
        data: dataString,
        success: function (dados) {
            $("#myModal").modal('hide');
            ListarAreas();
        }
    });
}
function BuscaModal(ID, TIPO) {
    if (TIPO === 'CAD') {
        $.ajax({
            url: 'Areas/Create',
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-primary" onclick="Salvar()">Cadatrar Área</button>')
                $('#myModal').modal('show')
            }
        });
    }
    else if (TIPO == 'SHOW') {
        $.ajax({
            url: 'Areas/Details/' + ID,
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
            url: 'Areas/Edit/' + ID,
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-primary" onclick="Editar()">Editar Área</button>')
                $('#myModal').modal('show')
            }
        });
    }
    else if (TIPO === 'DEL') {
        $.ajax({
            url: 'Areas/Delete/' + ID,
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-danger" onclick="Excluir()">Excluir Área</button>')
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
        url: "Areas/Edit",
        data: dataString,
        success: function (dados) {
            ListarAreas();
            $("#myModal").modal('hide');
        }
    });
}
function Excluir() {
    var area = $('#Id_Area').val()
    $.ajax({
        type: "POST",
        url: "Areas/Delete",
        data: {
            id: area
        },
        success: function (dados) {
            ListarAreas();
            $("#myModal").modal('hide');
        }
    });
}