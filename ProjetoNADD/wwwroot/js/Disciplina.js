function ListarDisciplinas() {
    if ($.fn.DataTable.isDataTable('#tabelaDisciplinas')) {
        $('#tabelaDisciplinas').DataTable().destroy();
    }
    $.ajax({
        type: "POST",
        url: "../Disciplinas/GetDisciplinas",
        success: function (dados) {
            if (dados.length > 0) {
                $("#corpoTabela").html('')
                for (var i = 0; i < dados.length; i++) {
                    $("#corpoTabela").append("<tr><td>" + dados[i].nome_Disciplina + "</td><td>" + dados[i].periodo_Disciplina + "</td><td>" + dados[i].ano_Disciplina + "</td><td>" + dados[i].nome_Curso + "</td><td><a class='btn btn-success btn-xs' href='#' onclick='BuscaModal(" + dados[i].id_Disciplina + ", \"SHOW\")' title='Visualizar'>Detalhes</a>&nbsp;<a class='btn btn-warning btn-xs glyphicon glyphicon-pencil' href='#' onclick='BuscaModal(" + dados[i].id_Disciplina + ", \"EDIT\")' title='Editar'>Editar</a>&nbsp;<a class='btn btn-danger  glyphicon glyphicon-remove btn-xs'  href='#' onclick='BuscaModal(" + dados[i].id_Disciplina+", \"DEL\")' title='Excluir'>Excluir</a></td></tr>")
                }
            }
            $("#tabelaDisciplinas").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf'
                ],
                "columnDefs": [
                    { "orderable": false, "targets": 4 }
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
function SelectProfessor() {
    $(document).ready(function () {
        var res = $("#myModal #Professor_id").val();
        if (res !== undefined) {
            $("#myModal #Professor_id").select2({
                placeholder: "Selecione os professores"
            });
        }
    })
}
function ProfessoresMarcados(ID) {
    $.ajax({
        url: 'Disciplinas/ProfessoresMarcados',
        type: 'POST',
        data: {
            disciplina: ID
        },
        success: function (res) {
            if (res.length > 0) {
                var valores = []
                for (var i = 0; i < res.length; i++) {
                    valores.push(res[i].id)
                }
                $("#Professor_id").val(valores).trigger("change")
            }
        }
    });
}
function Salvar() {
    var professores = $('#Professorcad').val()
    var nome
    if (professores.length > 0) {
        professores2 = [];
        for (var i = 0; i < professores.length; i++) {
            professores2.push(Number(professores[i]))
        }
        professores = professores2;
    }

    var dataString = {
        professores: professores,
        Nome_Disciplina: $('#Nome_Disciplinacad').val(),
        Periodo_Disciplina: $('#Periodo_Disciplinacad').val(),
        Ano_Disciplina: $('#Ano_Disciplinacad').val(),
        CursoId: $('#CursoIdcad').val()
    }
    $.ajax({
        type: "POST",
        url: "../Disciplinas/Create",
        data: dataString,
        success: function (dados) {
        }
    });
    $("#myModal").modal('hide');
}
function BuscaModal(ID, TIPO) {
    if (TIPO === 'CAD') {
        $.ajax({
            url: 'Disciplinas/Create',
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-primary" onclick="Salvar()">Cadatrar Disciplina</button>')
                $('#myModal').modal('show')
                SelectProfessor()
            }
        });
    }
    else if (TIPO == 'SHOW') {
        $.ajax({
            url: 'Disciplinas/Details/' + ID,
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
            url: 'Disciplinas/Edit/' + ID,
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-primary" onclick="Editar()">Editar Disciplina</button>')
                $('#myModal').modal('show')
                SelectProfessor()
                ProfessoresMarcados(ID);
            }
        });
    }
    else if (TIPO === 'DEL') {
        $.ajax({
            url: 'Disciplinas/Delete/' + ID,
            type: 'GET',
            success: function (res) {
                var cabecalho = $(res).find("#cabecalho")
                $("#myModal .modal-title").html(cabecalho)
                var conteudo = $(res).find("#conteudo_create")
                $("#myModal .modal-body").html(conteudo);
                $("#myModal .modal-footer").html('<button type = "button" class= "btn btn-danger" onclick="Excluir()">Excluir Disciplina</button>')
                $('#myModal').modal('show')
                SelectProfessor()
                ProfessoresMarcados(ID);
            }
        });
    }
}
$(document).ready(function () {
    jQuery('#Professor_id').select2({
        placeholder: "Selecione os Professores",
        allowClear: true
    });
});
function Editar() {
    var professores = $('#Professor_id').val()
    var nome
    if (professores.length > 0) {
        professores2 = [];
        for (var i = 0; i < professores.length; i++) {
            professores2.push(Number(professores[i]))
        }
        professores = professores2;
    }
    var dataString = {
        Id_Disciplina: $('#Id_Disciplina').val(),
        professores: professores,
        Nome_Disciplina: $('#Nome_Disciplina').val(),
        Periodo_Disciplina: $('#Periodo_Disciplina').val(),
        Ano_Disciplina: $('#Ano_Disciplina').val(),
        CursoId: $('#CursoId').val()
    }
    $.ajax({
        type: "POST",
        url: "Disciplinas/Edit",
        data: dataString,
        success: function (dados) {
            ListarDisciplinas();
            $("#myModal").modal('hide');
        }
    });
}
function Excluir() {
    var disciplina = $('#Id_Disciplina').val()
    $.ajax({
        type: "POST",
        url: "Disciplinas/Delete",
        data: {
            id: disciplina
        },
        success: function (dados) {
            ListarDisciplinas();
            $("#myModal").modal('hide');
        }
    });
}