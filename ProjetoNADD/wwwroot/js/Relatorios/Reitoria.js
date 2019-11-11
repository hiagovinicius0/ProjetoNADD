function BuscaAnos() {
    $.ajax({
        type: "POST",
        url: "../Relatorios/BuscaAnosReitoria",
        success: function (dados) {
            $('#Id_Ano').html('');
            $('#Id_Ano').append('<option value =""></option>');
            if (dados.length > 0) {
                var limite = dados.length;
                for (var i = 0; i < limite; i++) {
                    $('#Id_Ano').append('<option value ="' + dados[i].ano + '">' + dados[i].ano + '</option>');
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
    var ano = $('#Id_Ano').val();
    if (ano === "") {
        $('#conteudo_relatorio').css('display', 'none')
        return;
    }
    $.ajax({
        type: "POST",
        url: "../Relatorios/BuscaRelatorioReitoria",
        data: {
            Id_Ano: ano
        },
        success: function (dados) {
            console.log(dados)
            if (dados.length > 0) {
                //Tabela 1 - linha 2
                var valorExplicitoProvaTrue = dados[0].valorExplicitoProvaTrue;
                var valorExplicitoProvaFalse = dados[0].valorExplicitoProvaFalse;
                var valorExplicitoTotal = valorExplicitoProvaFalse + valorExplicitoProvaTrue;
                $('#valorExplicitoTrueNumero').html(valorExplicitoProvaTrue);
                $('#valorExplicitoTruePorcentagem').html(((valorExplicitoProvaTrue / valorExplicitoTotal) * 100).toFixed(2) + "%");
                $('#valorExplicitoFalseNumero').html(valorExplicitoProvaFalse);
                $('#valorExplicitoFalsePorcentagem').html(((valorExplicitoProvaFalse / valorExplicitoTotal) * 100).toFixed(2) + "%");
                $('#valorExplicitoTotal').html(valorExplicitoTotal)

                //Tabela 1 - Linha 3
                var valorQuestoesExplicitoTrue = dados[0].valorQuestoesExplicitoTrue;
                var valorQuestoesExplicitoFalse = dados[0].valorQuestoesExplicitoFalse;
                var valorQuestoesExplicitoTotal = valorQuestoesExplicitoFalse + valorQuestoesExplicitoTrue;
                $('#valorQuestoesExplicitoTrueNumero').html(valorQuestoesExplicitoTrue);
                $('#valorQuestoesExplicitoTruePorcentagem').html(((valorQuestoesExplicitoTrue / valorQuestoesExplicitoTotal) * 100).toFixed(2) + "%");
                $('#ValorQuestoesExplicitoFalseNumero').html(valorQuestoesExplicitoFalse);
                $('#ValorQuestoesExplicitoFalsePorcentagem').html(((valorQuestoesExplicitoFalse / valorQuestoesExplicitoTotal) * 100).toFixed(2) + "%");
                $('#ValorQuestoesExplicitoTotal').html(valorQuestoesExplicitoTotal)

                //Tabela 1 - Linha 4
                var somatorioValoresTrue = dados[0].somatorioValoresTrue;
                var somatorioValoresFalse = dados[0].somatorioValoresFalse;
                var somatorioValoresTotal = somatorioValoresFalse + somatorioValoresTrue;
                $('#somatorioValoresTrueNumero').html(somatorioValoresTrue);
                $('#somatorioValoresTruePorcentagem').html(((somatorioValoresTrue / somatorioValoresTotal) * 100).toFixed(2) + "%");
                $('#somatorioValoresFalseNumero').html(somatorioValoresFalse);
                $('#somatorioValoresFalsePorcentagem').html(((somatorioValoresFalse / somatorioValoresTotal) * 100).toFixed(2) + "%");
                $('#somatorioValoresTotal').html(somatorioValoresTotal)

                //Tabela 1 - Linha 5
                var referenciasTrue = dados[0].referenciasTrue;
                var referenciasFalse = dados[0].referenciasFalse;
                var referenciasTotal = referenciasFalse + referenciasTrue;
                $('#referenciasTrueNumero').html(referenciasTrue);
                $('#referenciasTruePorcentagem').html(((referenciasTrue / referenciasTotal) * 100).toFixed(2) + "%");
                $('#referenciasFalseNumero').html(referenciasFalse);
                $('#referenciasFalsePorcentagem').html(((referenciasFalse / referenciasTotal) * 100).toFixed(2) + "%");
                $('#referenciasTotal').html(referenciasTotal)

                //Tabela 1 - Linha 6
                var equilibrioTrue = dados[0].equilibrioTrue;
                var equilibrioFalse = dados[0].equilibrioFalse;
                var equilibrioTotal = equilibrioTrue + equilibrioFalse;
                $('#equilibrioTrueNumero').html(equilibrioTrue);
                $('#equilibrioTruePorcentagem').html(((equilibrioTrue / equilibrioTotal) * 100).toFixed(2) + "%");
                $('#equilibrioFalseNumero').html(equilibrioFalse);
                $('#equilibrioFalsePorcentagem').html(((equilibrioFalse / equilibrioTotal) * 100).toFixed(2) + "%");
                $('#equilibrioTotal').html(equilibrioTotal)

                //Tabela 2 - Linha 1
                var questoesMeedTrue = dados[0].questoesMeedTrue;
                var questoesMeedFalse = dados[0].questoesMeedFalse;
                var questoesMeedTotal = questoesMeedTrue + questoesMeedFalse;
                $('#questoesMEEDTrueNumero').html(questoesMeedTrue);
                $('#questoesMEEDTruePorcentagem').html(((questoesMeedTrue / questoesMeedTotal) * 100).toFixed(2) + "%");
                $('#questoesMEEDFalseNumero').html(equilibrioFalse);
                $('#questoesMEEDFalsePorcentagem').html(((questoesMeedFalse / questoesMeedTotal) * 100).toFixed(2) + "%");
                $('#questoesMEEDTotal').html(questoesMeedTotal)

                //Tabela 2 - Linha 2
                var diversificacaoTrue = dados[0].diversificacaoTrue;
                var diversificacaoFalse = dados[0].diversificacaoFalse;
                var diversificacaoTotal = diversificacaoTrue + diversificacaoFalse;
                $('#diversificacaoTrueNumero').html(diversificacaoTrue);
                $('#diversificacaoTruePorcentagem').html(((diversificacaoTrue / diversificacaoTotal) * 100).toFixed(2) + "%");
                $('#diversificacaoFalseNumero').html(diversificacaoFalse);
                $('#diversificacaoFalsePorcentagem').html(((diversificacaoFalse / diversificacaoTotal) * 100).toFixed(2) + "%");
                $('#diversificacaoTotal').html(diversificacaoTotal)

                //Tabela 2 - Linha 3
                var contextualizacaoTrue = dados[0].contextualizacaoTrue;
                var contextualizacaoFalse = dados[0].contextualizacaoFalse;
                var contextualizacaoTotal = contextualizacaoTrue + contextualizacaoFalse;
                $('#contextualizacaoTrueNumero').html(contextualizacaoTrue);
                $('#contextualizacaoTruePorcentagem').html(((contextualizacaoTrue / contextualizacaoTotal) * 100).toFixed(2) + "%");
                $('#contextualizacaoFalseNumero').html(contextualizacaoFalse);
                $('#contextualizacaoFalsePorcentagem').html(((contextualizacaoFalse / contextualizacaoTotal) * 100).toFixed(2) + "%");
                $('#contextualizacaoTotal').html(contextualizacaoTotal)

                //Tabela 3 - Coluna 1
                var total = dados[0].conhecimento + dados[0].aplicacao + dados[0].aplicacao + dados[0].analise + dados[0].sintese + dados[0].avaliacao;
                $('#conhecimentoNumero').html(dados[0].conhecimento)
                $('#compreensaoNumero').html(dados[0].compreensao)
                $('#aplicacaoNumero').html(dados[0].aplicacao)
                $('#analiseNumero').html(dados[0].analise)
                $('#sinteseNumero').html(dados[0].sintese)
                $('#avaliacaoNumero').html(dados[0].avaliacao)
                $('#totalGeralNumero').html(total)
                $('#totalGeralNumero2').html(total)

                //Tabela 3 - Coluna 2
                $('#conhecimentoPorcentagem').html(((dados[0].conhecimento / total) * 100).toFixed(2) + "%")
                $('#compreensaoPorcentagem').html(((dados[0].compreensao / total) * 100).toFixed(2) + "%")
                $('#aplicacaoPorcentagem').html(((dados[0].aplicacao / total) * 100).toFixed(2) + "%")
                $('#analisePorcentagem').html(((dados[0].analise / total) * 100).toFixed(2) + "%")
                $('#sintesePorcentagem').html(((dados[0].sintese / total) * 100).toFixed(2) + "%")
                $('#avaliacaoPorcentagem').html(((dados[0].avaliacao / total) * 100).toFixed(2) + "%")

                //Tabela 3 - Coluna 3
                var total13 = dados[0].conhecimento + dados[0].aplicacao + dados[0].aplicacao
                var total23 = dados[0].analise + dados[0].sintese + dados[0].avaliacao
                $('#numero13').html(total13)
                $('#numero23').html(total23)

                //Tabela 3 - Coluna 4
                $('#numero13porcentagem').html(((total13 / total) * 100).toFixed(2) + "%")
                $('#numero23porcentagem').html(((total23 / total) * 100).toFixed(2) + "%")
            }
        }
    });
}