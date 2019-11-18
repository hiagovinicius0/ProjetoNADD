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
                    $('#Id_Curso').append('<option value ="' + dados[i].id + '">' + dados[i].nome + '</option>');
                }
            }
            $("#Id_Curso").select2({
                placeholder: "Selecione o Curso"
            });
        }
    });
}

function BuscaAnos() {
    var curso = $('#Id_Curso').val();
    $.ajax({
        type: "POST",
        url: "../Relatorios/BuscaAnos",
        data: {
            Id_Curso: curso
        },
        success: function (dados) {
            $('#Id_Ano').html('');
            $('#Id_Ano').append('<option value =""></option>');
            if (dados.length > 0) {
                var limite = dados.length;
                var anos = new Array();
                for (var i = 0; i < limite; i++) {
                    anos[i] = dados[i].ano
                }
                anos = anos.filter(function (elem, pos, self) {
                    return self.indexOf(elem) == pos;
                })
                for (var i = 0; i < anos.length; i++) {
                    $('#Id_Ano').append('<option value ="' + anos[i] + '">' + anos[i] + '</option>');
                }
            }
            $("#Id_Ano").select2({
                placeholder: "Selecione o Ano"
            });
            $('#divAno').css('display', 'block')
        }
    });
}

function BuscaRelatorio() {
    var curso = $('#Id_Curso').val();
    var ano = $('#Id_Ano').val();
    if (curso === "") {
        $('#conteudo_relatorio').css('display', 'none')
        return;
    }
    if (ano === "") {
        $('#conteudo_relatorio').css('display', 'none')
        return;
    }
    $.ajax({
        type: "POST",
        url: "../Relatorios/BuscaRelatorioCoordenador",
        data: {
            Id_Curso: curso,
            Id_Ano: ano
        },
        success: function (dados) {
            if ($.fn.DataTable.isDataTable('#tabelaRelatorio')) {
                $('#tabelaRelatorio').DataTable().destroy();
            }
            $('#conteudo_relatorio').css('display', 'block')
            $("#corpoTabela").html('')
            if (dados.length > 0) {
                for (var i = 0; i < dados.length; i++) {
                    var contextualizacao = dados[i].contextualizacao === true ? "Sim" : "Não"
                    var clareza = dados[i].clareza === true ? "Sim" : "Não"
                    $("#corpoTabela").append("<tr><td>" + dados[i].ano + "</td><td>" + dados[i].curso + "</td><td>" + dados[i].periodo + "</td><td>" + dados[i].area + "</td><td>" + contextualizacao + "</td><td>" + clareza + "</td><td>" + dados[i].complexidade+"</td></tr>")
                }
            }
            $("#tabelaRelatorio").DataTable({
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