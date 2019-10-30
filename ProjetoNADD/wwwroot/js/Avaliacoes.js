function ListarAvaliacoes() {
    if ($.fn.DataTable.isDataTable('#tabelaAvaliacoes')) {
        $('#tabelaAvaliacoes').DataTable().destroy();
    }
    $.ajax({
        type: "POST",
        url: "../Avaliacoes/GetAvaliacoes",
        success: function (dados) {
            $("#corpoTabela").html('')  
            if (dados.length > 0) {
                for (var i = 0; i < dados.length; i++) {
                    var envioQuestoes = {
                        id_Avaliacao : dados[i].id_Avaliacao,
                        nome_Avaliacao : dados[i].nome_Avaliacao
                    }
                    var observacoes = dados[i].observacoes_Avaliacao !== null ? dados[i].observacoes_Avaliacao : ''
                    $("#corpoTabela").append("<tr><td>" + dados[i].nome_Avaliacao + "</td><td>" + observacoes + "</td><td>" + dados[i].nome_Disciplina + "</td><td><a class='btn btn-success btn-xs' href='#' onclick='BuscaModal(" + dados[i].id_Avaliacao + ", \"SHOW\")' title='Visualizar'>Detalhes</a>&nbsp;<a class='btn btn-primary btn-xs' href='#' onclick='sendQuestoes(" + JSON.stringify(envioQuestoes)+")' title='Questões'>Quetões</a>&nbsp;<a class='btn btn-warning btn-xs glyphicon glyphicon-pencil' href='#' onclick='BuscaModal(" + dados[i].id_Avaliacao + ", \"EDIT\")' title='Editar'>Editar</a>&nbsp;<a class='btn btn-danger  glyphicon glyphicon-remove btn-xs'  href='#' onclick='BuscaModal(" + dados[i].id_Avaliacao + ", \"DEL\")' title='Excluir'>Excluir</a></td></tr>")
                }
            }
            $("#tabelaAvaliacoes").DataTable({
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
        Nome_Avaliacao: $('#myModal #Nome_Avaliacao').val(),
        ValorExplicitoProva_Avaliacao: $('#myModal #ValorExplicitoProva_Avaliacao').prop("checked") === true ? true : false,
        ValorExplicitoQuestoes_Avaliacao: $('#myModal #ValorExplicitoQuestoes_Avaliacao').prop("checked") === true ? true : false,
        SomatorioQuestoes_Avaliacao: $('#myModal #SomatorioQuestoes_Avaliacao').prop("checked") === true ? true : false,
        Referencias_Avaliacao: $('#myModal #Referencias_Avaliacao').prop("checked") === true ? true : false,
        NumeroQuestoes_Avaliacao: $('#myModal #NumeroQuestoes_Avaliacao').val(),
        EquilibrioValorQuestoes_Avaliacao: $('#myModal #EquilibrioValorQuestoes_Avaliacao').prop("checked") === true ? true : false,
        Diversificacao_Avaliacao: $('#myModal #Diversificacao_Avaliacao').prop("checked") === true ? true : false,
        Contextualidade_Avaliacao: $('#myModal #Contextualidade_Avaliacao').prop("checked") === true ? true : false,
        Observacoes_Avaliacao: $('#myModal #Observacoes_Avaliacao').val(),
        DisciplinaId: $('#myModal #DisciplinaId').val(),
        Clareza_Avaliacao: $('#myModal #Clareza_Avaliacao').prop("checked") === true ? true : false
    }
    $.ajax({
        type: "POST",
        url: "../Avaliacoes/Create",
        data: dataString,
        success: function (dados) {
            $("#myModal").modal('hide');
            ListarAvaliacoes();
        }
    });
}
function BuscaModal(ID, TIPO) {
    if (TIPO === 'CAD') {
        $.ajax({
            url: 'Avaliacoes/Create',
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-primary" onclick="Salvar()">Cadatrar Avaliação</button>')
                $('#myModal').modal('show')
            }
        });
    }
    else if (TIPO == 'SHOW') {
        $.ajax({
            url: 'Avaliacoes/Details/' + ID,
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
            url: 'Avaliacoes/Edit/' + ID,
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-primary" onclick="Editar()">Editar Avaliação</button>')
                $('#myModal').modal('show')
            }
        });
    }
    else if (TIPO === 'DEL') {
        $.ajax({
            url: 'Avaliacoes/Delete/' + ID,
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-danger" onclick="Excluir()">Excluir Avaliação</button>')
                $('#myModal').modal('show')
            }
        });
    }
}
function Editar() {
    var dataString = {
        Id_Avaliacao: $('#myModal #Id_Avaliacao').val(),
        Nome_Avaliacao: $('#myModal #Nome_Avaliacao').val(),
        ValorExplicitoProva_Avaliacao: $('#myModal #ValorExplicitoProva_Avaliacao').prop("checked") === true ? true : false,
        ValorExplicitoQuestoes_Avaliacao: $('#myModal #ValorExplicitoQuestoes_Avaliacao').prop("checked") === true ? true : false,
        SomatorioQuestoes_Avaliacao: $('#myModal #SomatorioQuestoes_Avaliacao').prop("checked") === true ? true : false,
        Referencias_Avaliacao: $('#myModal #Referencias_Avaliacao').prop("checked") === true ? true : false,
        NumeroQuestoes_Avaliacao: $('#myModal #NumeroQuestoes_Avaliacao').val(),
        EquilibrioValorQuestoes_Avaliacao: $('#myModal #EquilibrioValorQuestoes_Avaliacao').prop("checked") === true ? true : false,
        Diversificacao_Avaliacao: $('#myModal #Diversificacao_Avaliacao').prop("checked") === true ? true : false,
        Contextualidade_Avaliacao: $('#myModal #Contextualidade_Avaliacao').prop("checked") === true ? true : false,
        Observacoes_Avaliacao: $('#myModal #Observacoes_Avaliacao').val(),
        DisciplinaId: $('#myModal #DisciplinaId').val(),
        Clareza_Avaliacao: $('#myModal #Clareza_Avaliacao').prop("checked") === true ? true : false
    }
    $.ajax({
        type: "POST",
        url: "Avaliacoes/Edit",
        data: dataString,
        success: function (dados) {
            ListarAvaliacoes();
            $("#myModal").modal('hide');
        }
    });
}
function Excluir() {
    var curso = $('#Id_Avaliacao').val()
    $.ajax({
        type: "POST",
        url: "Avaliacoes/Delete",
        data: {
            id: curso
        },
        success: function (dados) {
            ListarAvaliacoes();
            $("#myModal").modal('hide');
        }
    });
}
window.sendQuestoes = function (obj) {
    //Define o formulário
    var myForm = document.createElement("form");
    myForm.action = "/Questoes";
    myForm.method = "POST";
    myForm.target = "_blank";
    myForm.enctype = "multipart/form-data"
    for (var key in obj) {
        var input = document.createElement("input");
        input.type = "hidden";
        input.value = obj[key];
        input.name = key;
        myForm.appendChild(input);
    }
    //Adiciona o form ao corpo do documento
    document.body.appendChild(myForm).visibility = "hidden";
    //Envia o formulário
    myForm.submit();
}