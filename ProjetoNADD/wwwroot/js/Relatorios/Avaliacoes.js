function GetCursos() {
    $.ajax({
        type: "POST",
        url: "../Relatorios/GetCursos",
        success: function (dados) {
            $('#Id_Curso').html('');
            $('#Id_Curso').append('<option value =""></option>');
            if (dados.length > 0) {
                var limite = dados.length;
                for (var i = 0; i < limite; i++) {
                    $('#Id_Curso').append('<option value ="'+dados[i].id+'">'+dados[i].nome+'</option>');
                }
            }
            $("#Id_Curso").select2({
                placeholder: "Selecione o Curso"
            });
        }
    });
}
function BuscaDisciplinas() {
    var curso = $('#Id_Curso').val();
    $.ajax({
        type: "POST",
        url: "../Relatorios/BuscaDisciplinas",
        data: {
            Id_Curso: curso
        },
        success: function (dados) {
            $('#Id_Disciplina').html('');
            $('#Id_Disciplina').append('<option value =""></option>');
            if (dados.length > 0) {
                var limite = dados.length;
                for (var i = 0; i < limite; i++) {
                    $('#Id_Disciplina').append('<option value ="' + dados[i].id + '">' + dados[i].nome + '</option>');
                }
            }
            $("#Id_Disciplina").select2({
                placeholder: "Selecione a Disciplina"
            });
            $('#divDIsciplinas').css('display', 'block')
        }
    });
}
function BuscaAvaliacao() {
    var curso = $('#Id_Disciplina').val();
    $.ajax({
        type: "POST",
        url: "../Relatorios/BuscaDisciplinas",
        data: {
            Id_Curso: curso
        },
        success: function (dados) {
            $('#Id_Disciplina').html('');
            $('#Id_Disciplina').append('<option value =""></option>');
            if (dados.length > 0) {
                var limite = dados.length;
                for (var i = 0; i < limite; i++) {
                    $('#Id_Disciplina').append('<option value ="' + dados[i].id + '">' + dados[i].nome + '</option>');
                }
            }
            $("#Id_Disciplina").select2({
                placeholder: "Selecione a Disciplina"
            });
            $('#divDIsciplinas').css('display', 'block')
        }
    });
}
function BuscaRelatorio() {
    var disciplina = $('#Id_Disciplina').val();
    var curso = $('#Id_Curso').val();
    if (disciplina === "") {
        $('#conteudo_relatorio').css('display', 'none')
        return;
    }
    if (curso === "") {
        $('#conteudo_relatorio').css('display', 'none')
        return;
    }
    if ($.fn.DataTable.isDataTable('#tabelaRelatorio')) {
        $('#tabelaRelatorio').DataTable().destroy();
    }
    $.ajax({
        type: "POST",
        url: "../Relatorios/BuscaRelatorio",
        data: {
            Id_Curso: curso,
            Id_Disciplina: disciplina
        },
        success: function (dados) {
            $('#corpoTabelaRelatorio').html('');
            console.log(dados)
            if (dados.length > 0) {
                var limite = dados.length
                for (var i = 0; i < limite; i++) {
                    var contextualizacao = dados[i].contextualidade !== true ? "Sim" : "Não"
                    var clareza = dados[i].clareza === true ? "Sim" : "Não"
                    var complexidade = dados[i].complexidade !== null ? dados[i].complexidade : ""
                    $('#corpoTabelaRelatorio').append('<tr><td>' + dados[i].nome + '</td><td>' + dados[i].ano + '</td><td>' + dados[i].curso + '</td><td>' + dados[i].periodo + '</td><td>' + dados[i].area + '</td><td>' + contextualizacao + '</td><td>' + clareza + '</td><td>' + complexidade +'</td></tr>');
                }
            }
            $("#tabelaRelatorio").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf'
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
            $('#conteudo_relatorio').css('display', 'block')
        }
    });
}