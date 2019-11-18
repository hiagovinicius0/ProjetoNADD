function BuscaAnos() {
    $.ajax({
        type: "POST",
        url: "../Relatorios/BuscaAnosReitoria",
        success: function (dados) {
            $('#Id_Ano').html('');
            $('#Id_Ano').append('<option value =""></option>');
            if (dados.length > 0) {
                var limite = dados.length
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
                var total = dados[0].conhecimento + dados[0].compreensao + dados[0].aplicacao + dados[0].analise + dados[0].sintese + dados[0].avaliacao;
                $('#conhecimentoNumero').html(dados[0].conhecimento)
                $('#compreensaoNumero').html(dados[0].compreensao)
                $('#aplicacaoNumero').html(dados[0].aplicacao)
                $('#analiseNumero').html(dados[0].analise)
                $('#sinteseNumero').html(dados[0].sintese)
                $('#avaliacaoNumero').html(dados[0].avaliacao)
                $('#totalGeralNumero').html(total)
                $('#totalGeralNumero2').html(total)

                //Tabela 3 - Coluna 2
                $('#conhecimentoPorcentagem').html(total === 0 ? 0 : ((dados[0].conhecimento / total) * 100).toFixed(2) + "%")
                $('#compreensaoPorcentagem').html(total === 0 ? 0 : ((dados[0].compreensao / total) * 100).toFixed(2) + "%")
                $('#aplicacaoPorcentagem').html(total === 0 ? 0 : ((dados[0].aplicacao / total) * 100).toFixed(2) + "%")
                $('#analisePorcentagem').html(total === 0 ? 0 : ((dados[0].analise / total) * 100).toFixed(2) + "%")
                $('#sintesePorcentagem').html(total === 0 ? 0 : ((dados[0].sintese / total) * 100).toFixed(2) + "%")
                $('#avaliacaoPorcentagem').html(total === 0 ? 0 : ((dados[0].avaliacao / total) * 100).toFixed(2) + "%")

                //Tabela 3 - Coluna 3
                var total13 = dados[0].conhecimento + dados[0].aplicacao + dados[0].compreensao
                var total23 = dados[0].analise + dados[0].sintese + dados[0].avaliacao
                $('#numero13').html(total13)
                $('#numero23').html(total23)

                //Tabela 3 - Coluna 4
                $('#numero13porcentagem').html(((total13 / total) * 100).toFixed(2) + "%")
                $('#numero23porcentagem').html(((total23 / total) * 100).toFixed(2) + "%")

                //Tabela 4 - Coluna 1
                var totalExatas = dados[0].conhecimentoExatas + dados[0].compreensaoExatas + dados[0].aplicacaoExatas + dados[0].analiseExatas + dados[0].sinteseExatas + dados[0].avaliacaoExatas;
                $('#conhecimentoNumeroExatas').html(dados[0].conhecimentoExatas)
                $('#compreensaoNumeroExatas').html(dados[0].compreensaoExatas)
                $('#aplicacaoNumeroExatas').html(dados[0].aplicacaoExatas)
                $('#analiseNumeroExatas').html(dados[0].analiseExatas)
                $('#sinteseNumeroExatas').html(dados[0].sinteseExatas)
                $('#avaliacaoNumeroExatas').html(dados[0].avaliacaoExatas)
                $('#totalGeralNumeroExatas').html(totalExatas)

                //Tabela 4 - Coluna 2
                $('#conhecimentoPorcentagemExatas').html(totalExatas === 0 ? (0).toFixed(2) + "%" : ((dados[0].conhecimentoExatas / totalExatas) * 100).toFixed(2) + "%")
                $('#compreensaoPorcentagemExatas').html(totalExatas === 0 ? (0).toFixed(2) + "%" : ((dados[0].compreensaoExatas / totalExatas) * 100).toFixed(2) + "%")
                $('#aplicacaoPorcentagemExatas').html(totalExatas === 0 ? (0).toFixed(2) + "%" : ((dados[0].aplicacaoExatas / totalExatas) * 100).toFixed(2) + "%")
                $('#analisePorcentagemExatas').html(totalExatas === 0 ? (0).toFixed(2) + "%" : ((dados[0].analiseExatas / totalExatas) * 100).toFixed(2) + "%")
                $('#sintesePorcentagemExatas').html(totalExatas === 0 ? (0).toFixed(2) + "%" : ((dados[0].sinteseExatas / totalExatas) * 100).toFixed(2) + "%")
                $('#avaliacaoPorcentagemExatas').html(totalExatas === 0 ? (0).toFixed(2) + "%" : ((dados[0].avaliacaoExatas / totalExatas) * 100).toFixed(2) + "%")

                //Tabela 4 - Coluna 3
                var total13Exatas = dados[0].conhecimentoExatas + dados[0].compreensaoExatas + dados[0].aplicacaoExatas
                var total23Exatas = dados[0].analiseExatas + dados[0].sinteseExatas + dados[0].avaliacaoExatas
                $('#numero13Exatas').html(((total13Exatas / totalExatas) * 100).toFixed(2) + "%")
                $('#numero23Exatas').html(((total23Exatas / totalExatas) * 100).toFixed(2) + "%")

                //Tabela 4 - Coluna 4
                var totalHumanas = dados[0].conhecimentoHumanas  + dados[0].compreensaoHumanas + dados[0].aplicacaoHumanas + dados[0].analiseHumanas + dados[0].sinteseHumanas + dados[0].sinteseHumanas;
                $('#conhecimentoNumeroHumanas').html(dados[0].conhecimentoHumanas)
                $('#compreensaoNumeroHumanas').html(dados[0].compreensaoHumanas)
                $('#aplicacaoNumeroHumanas').html(dados[0].aplicacaoHumanas)
                $('#analiseNumeroHumanas').html(dados[0].analiseHumanas)
                $('#sinteseNumeroHumanas').html(dados[0].sinteseHumanas)
                $('#avaliacaoNumeroHumanas').html(dados[0].avaliacaoHumanas)
                $('#totalGeralNumeroHumanas').html(totalHumanas)

                //Tabela 4 - Coluna 5
                $('#conhecimentoPorcentagemHumanas').html(totalHumanas === 0 ? (0).toFixed(2) + "%" : ((dados[0].conhecimentoHumanas / totalHumanas) * 100).toFixed(2) + "%")
                $('#compreensaoPorcentagemHumanas').html(totalHumanas === 0 ? (0).toFixed(2) + "%" : ((dados[0].compreensaoHumanas / totalHumanas) * 100).toFixed(2) + "%")
                $('#aplicacaoPorcentagemHumanas').html(totalHumanas === 0 ? (0).toFixed(2) + "%" : ((dados[0].aplicacaoHumanas / totalHumanas) * 100).toFixed(2) + "%")
                $('#analisePorcentagemHumanas').html(totalHumanas === 0 ? (0).toFixed(2) + "%" : ((dados[0].analiseHumanas / totalHumanas) * 100).toFixed(2) + "%")
                $('#sintesePorcentagemHumanas').html(totalHumanas === 0 ? (0).toFixed(2) + "%" : ((dados[0].sinteseHumanas / totalHumanas) * 100).toFixed(2) + "%")
                $('#avaliacaoPorcentagemHumanas').html(totalHumanas === 0 ? (0).toFixed(2) + "%" : ((dados[0].avaliacaoHumanas / totalHumanas) * 100).toFixed(2) + "%")

                //Tabela 4 - Coluna 6
                var total13Humanas = dados[0].conhecimentoHumanas + dados[0].compreensaoHumanas + dados[0].aplicacaoHumanas
                var total23Humanas = dados[0].analiseHumanas + dados[0].sinteseHumanas + dados[0].avaliacaoHumanas
                $('#numero13Humanas').html(totalHumanas === 0 ? (0).toFixed(2) + "%" : ((total13Humanas / totalHumanas) * 100).toFixed(2) + "%")
                $('#numero23Humanas').html(totalHumanas === 0 ? (0).toFixed(2) + "%" : ((total23Humanas / totalHumanas) * 100).toFixed(2) + "%")

                //Tabela 4 - Coluna 7
                var totalSaude = dados[0].conhecimentoSaude  + dados[0].compreensaoSaude + dados[0].aplicacaoSaude + dados[0].analiseSaude + dados[0].sinteseSaude + dados[0].avaliacaoSaude;
                $('#conhecimentoNumeroSaude').html(dados[0].conhecimentoSaude)
                $('#compreensaoNumeroSaude').html(dados[0].compreensaoSaude)
                $('#aplicacaoNumeroSaude').html(dados[0].aplicacaoSaude)
                $('#analiseNumeroSaude').html(dados[0].analiseSaude)
                $('#sinteseNumeroSaude').html(dados[0].sinteseSaude)
                $('#avaliacaoNumeroSaude').html(dados[0].avaliacaoSaude)
                $('#totalGeralNumeroSaude').html(totalSaude)

                //Tabela 4 - Coluna 8
                $('#conhecimentoPorcentagemSaude').html(totalSaude === 0 ? (0).toFixed(2) + "%" : ((dados[0].conhecimentoSaude / totalSaude) * 100).toFixed(2) + "%")
                $('#compreensaoPorcentagemSaude').html(totalSaude === 0 ? (0).toFixed(2) + "%" : ((dados[0].compreensaoSaude / totalSaude) * 100).toFixed(2) + "%")
                $('#aplicacaoPorcentagemSaude').html(totalSaude === 0 ? (0).toFixed(2) + "%" : ((dados[0].aplicacaoSaude / totalSaude) * 100).toFixed(2) + "%")
                $('#analisePorcentagemSaude').html(totalSaude === 0 ? (0).toFixed(2) + "%" : ((dados[0].analiseSaude / totalSaude) * 100).toFixed(2) + "%")
                $('#sintesePorcentagemSaude').html(totalSaude === 0 ? (0).toFixed(2) + "%" : ((dados[0].sinteseSaude / totalSaude) * 100).toFixed(2) + "%")
                $('#avaliacaoPorcentagemSaude').html(totalSaude === 0 ? (0).toFixed(2) + "%" : ((dados[0].avaliacaoSaude / totalSaude) * 100).toFixed(2) + "%")

                //Tabela 4 - Coluna 9
                var total13Saude = dados[0].conhecimentoSaude + dados[0].aplicacaoSaude + dados[0].compreensaoSaude
                var total23Saude = dados[0].analiseSaude + dados[0].sinteseSaude + dados[0].avaliacaoSaude
                $('#numero13Saude').html(totalSaude === 0 ? (0).toFixed(2) + "%" : ((total13Saude / totalSaude) * 100).toFixed(2) + "%")
                $('#numero23Saude').html(totalSaude === 0 ? (0).toFixed(2) + "%" : ((total23Saude / totalSaude) * 100).toFixed(2) + "%")

                $('#conteudoRelatorio').css('display', 'block');
            }
            else {
                $('#conteudoRelatorio').css('display', 'none');
            }
        }
    });
}