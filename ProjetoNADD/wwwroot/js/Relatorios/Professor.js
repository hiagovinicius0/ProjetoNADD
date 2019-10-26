﻿function GetCursos() {
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
    var disciplina = $('#Id_Disciplina').val();
    $.ajax({
        type: "POST",
        url: "../Relatorios/BuscaAvaliacao",
        data: {
            Id_Disciplina: disciplina
        },
        success: function (dados) {
            $('#Id_Avaliacao').html('');
            $('#Id_Avaliacao').append('<option value =""></option>');
            if (dados.length > 0) {
                var limite = dados.length;
                for (var i = 0; i < limite; i++) {
                    $('#Id_Avaliacao').append('<option value ="' + dados[i].id + '">' + dados[i].nome + '</option>');
                }
            }
            $("#Id_Avaliacao").select2({
                placeholder: "Selecione a Avaliação"
            });
            $('#divAvaliacao').css('display', 'block')
        }
    });
}
function BuscaRelatorio() {
    var disciplina = $('#Id_Disciplina').val();
    var curso = $('#Id_Curso').val();
    var avaliacao = $('#Id_Avaliacao').val();
    if (disciplina === "") {
        $('#conteudo_relatorio').css('display', 'none')
        return;
    }
    if (curso === "") {
        $('#conteudo_relatorio').css('display', 'none')
        return;
    }
    if (avaliacao === "") {
        $('#conteudo_relatorio').css('display', 'none')
        return;
    }
    $.ajax({
        type: "POST",
        url: "../Relatorios/BuscaRelatorioProfessor",
        data: {
            Id_Curso: curso,
            Id_Disciplina: disciplina,
            Id_Avaliacao: avaliacao
        },
        success: function (dados) {
            console.log(dados)
            $('#Semestre').html(dados[0].ano + "." + dados[0].periodo)
            $('#Curso').html(dados[0].curso)
            $('#Area').html(dados[0].area)
            $('#Disciplina').html(dados[0].disciplina)
            var stringProfessor = '';
            var professor = JSON.parse(dados[0].professor);
            if (professor.length > 0) {
                var tamanho = professor.length
                for (var i = 0; i < tamanho; i++) {
                    if (i === 0) {
                        stringProfessor += professor[i].Nome
                    }
                    else {
                        stringProfessor += "/"+professor[i].Nome
                    }
                }
            }
            $('#Professor').html(stringProfessor)
            $('#PeriodoAno').html(dados[0].periodo)
            $('#ValorExplicito').html(dados[0].valorProvaExplicito === true ? "Apresenta adequadamente" : "Não Apresenta Adequadamente")
            $('#ValorQuestoes').html(dados[0].valorQuestoes === true ? "Apresenta adequadamente" : "Não Apresenta Adequadamente")
            $('#Somatorio').html(dados[0].somatorioQuestoes === true ? "Apresenta adequadamente" : "Não Apresenta Adequadamente")
            $('#Referencias').html(dados[0].referencias === true ? "Apresenta adequadamente" : "Não Apresenta Adequadamente")
            $('#QuestoesMEED').html(dados[0].questoesMEED === true ? "Sim" : "Não")
            $('#ValorTotalProva').html(dados[0].valorTotalProva)
            $('#NumeroQuestoes').html(dados[0].numeroQuestoes)
            $('#EquilibrioValores').html(dados[0].equilibrioDistribuicaoValores === true ? "Apresenta adequadamente" : "Não Apresenta Adequadamente")
            $('#DiversificacaoAvaliacao').html(dados[0].diversificacao === true ? "Sim" : "Não")
            $('#QuestaoContextualizadaAvaliacao').html(dados[0].questaoContextualizada === true ? "Sim" : "Não")
            $('#ObservacoesAvaliacao').html(dados[0].Observacoes)
            $('#conteudo_relatorio').css('display', 'block')
        }
    });
}