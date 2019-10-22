function ListarQuestoes(id) {
    if ($.fn.DataTable.isDataTable('#tabelaQuestoes')) {
        $('#tabelaQuestoes').DataTable().destroy();
    }
    $.ajax({
        type: "POST",
        url: "../Questoes/GetQuestoes",
        data: { Id_Avaliacao: id},
        success: function (dados) {
            $("#corpoTabela").html('')
            if (dados.length > 0) {
                for (var i = 0; i < dados.length; i++) {
                    $("#corpoTabela").append("<tr><td>" + dados[i].nome_Avaliacao + "</td><td>" + dados[i].id_Numero + "</td><td>" + dados[i].observacoes_Questao + "</td><td><a class='btn btn-success btn-xs' href='#' onclick='BuscaModal(" + id + "," + dados[i].id_Questao + ", \"SHOW\")' title='Visualizar'>Detalhes</a>&nbsp;<a class='btn btn-warning btn-xs glyphicon glyphicon-pencil' href='#' onclick='BuscaModal(" + id + "," + dados[i].id_Questao + ", \"EDIT\")' title='Editar'>Editar</a>&nbsp;<a class='btn btn-danger  glyphicon glyphicon-remove btn-xs'  href='#' onclick='BuscaModal(" + id + "," + dados[i].id_Questao + ", \"DEL\")' title='Excluir'>Excluir</a></td></tr>")
                }
            }
            $("#tabelaQuestoes").DataTable({
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
    var id = $('#myModal #Id_Avaliacao').val();
    var dataString = {
        Id_Numero: $('#myModal #Id_Numero').val(),
        Id_Avaliacao: $('#myModal #Id_Avaliacao').val(),
        Contextualizacao_Questao: $('#myModal #Contextualizacao_Questao').prop("checked") === true ? true : false,
        Clareza_Questao: $('#myModal #Clareza_Questao').prop("checked") === true ? true : false,
        Complexidade_Questao: $('#myModal #Complexidade_Questao').prop("checked") === true ? true : false,
        Observacoes_Questao: $('#myModal #Observacoes_Questao').val()
    }
    $.ajax({
        type: "POST",
        url: "../Questoes/Create",
        data: dataString,
        success: function (dados) {
            $("#myModal").modal('hide');
            ListarQuestoes(id);
        }
    });
}
function BuscaModal(IDAvaliacao, ID, TIPO) {
    if (TIPO === 'CAD') {
        $.ajax({
            url: 'Questoes/Create',
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-body").append('<input type="hidden" id="Id_Avaliacao" value="'+IDAvaliacao+'"/>')
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-primary" onclick="Salvar()">Cadatrar Questão</button>')
                $('#myModal').modal('show')
            }
        });
    }
    else if (TIPO == 'SHOW') {
        $.ajax({
            url: 'Questoes/Details/' + ID,
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
            url: 'Questoes/Edit/' + ID,
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-primary" onclick="Editar(' + IDAvaliacao+')">Editar Questão</button>')
                $('#myModal').modal('show')
            }
        });
    }
    else if (TIPO === 'DEL') {
        $.ajax({
            url: 'Questoes/Delete/' + ID,
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-danger" onclick="Excluir(' + IDAvaliacao+')">Excluir Questão</button>')
                $('#myModal').modal('show')
            }
        });
    }
}
function Editar(avaliacao) {
    var dataString = {
        Id_Questao: $('#myModal #Id_Questao').val(),
        Id_Numero: $('#myModal #Id_Numero').val(),
        Id_Avaliacao: $('#myModal #Id_Avaliacao').val(),
        Contextualizacao_Questao: $('#myModal #Contextualizacao_Questao').prop("checked") === true ? true : false,
        Clareza_Questao: $('#myModal #Clareza_Questao').prop("checked") === true ? true : false,
        Complexidade_Questao: $('#myModal #Complexidade_Questao').prop("checked") === true ? true : false,
        Observacoes_Questao: $('#myModal #Observacoes_Questao').val()
    }
    $.ajax({
        type: "POST",
        url: "Questoes/Edit",
        data: dataString,
        success: function (dados) {
            ListarQuestoes(avaliacao)
            $("#myModal").modal('hide');
        }
    });
}
function Excluir(IdAvaliacao) {
    var curso = $('#Id_Questao').val()
    $.ajax({
        type: "POST",
        url: "Questoes/Delete",
        data: {
            id: curso
        },
        success: function (dados) {
            ListarQuestoes(IdAvaliacao)
            $("#myModal").modal('hide');
        }
    });
}
function BuscaAvaliacao() {
    return $('#Id_AvaliacaoDiv').val()
}