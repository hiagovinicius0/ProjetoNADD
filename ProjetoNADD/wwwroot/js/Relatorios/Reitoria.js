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
                //linha 2
                var valorExplicitoProvaTrue = dados[0].valorExplicitoProvaTrue;
                var valorExplicitoProvaFalse = dados[0].valorExplicitoProvaFalse;
                var valorExplicitoTotal = valorExplicitoProvaFalse + valorExplicitoProvaTrue;
                $('#valorExplicitoTrueNumero').html(valorExplicitoProvaTrue);
                $('#valorExplicitoTruePorcentagem').html(((valorExplicitoProvaTrue / valorExplicitoTotal) * 100).toFixed(2) + "%");
                $('#valorExplicitoFalseNumero').html(valorExplicitoProvaFalse);
                $('#valorExplicitoFalsePorcentagem').html(((valorExplicitoProvaFalse / valorExplicitoTotal) * 100).toFixed(2) + "%");
                $('#valorExplicitoTotal').html(valorExplicitoTotal)

                //Linha 3
                var valorQuestoesExplicitoTrue = dados[0].valorQuestoesExplicitoTrue;
                var valorQuestoesExplicitoFalse = dados[0].valorQuestoesExplicitoFalse;
                var valorQuestoesExplicitoTotal = valorQuestoesExplicitoFalse + valorQuestoesExplicitoTrue;
                $('#valorQuestoesExplicitoTrueNumero').html(valorQuestoesExplicitoTrue);
                $('#valorQuestoesExplicitoTruePorcentagem').html(((valorQuestoesExplicitoTrue / valorQuestoesExplicitoTotal) * 100).toFixed(2) + "%");
                $('#ValorQuestoesExplicitoFalseNumero').html(valorQuestoesExplicitoFalse);
                $('#ValorQuestoesExplicitoFalsePorcentagem').html(((valorQuestoesExplicitoFalse / valorQuestoesExplicitoTotal) * 100).toFixed(2) + "%");
                $('#ValorQuestoesExplicitoTotal').html(valorQuestoesExplicitoTotal)

                //Linha 4
                var somatorioValoresTrue = dados[0].somatorioValoresTrue;
                var somatorioValoresFalse = dados[0].somatorioValoresFalse;
                var somatorioValoresTotal = somatorioValoresFalse + somatorioValoresTrue;
                $('#somatorioValoresTrueNumero').html(somatorioValoresTrue);
                $('#somatorioValoresTruePorcentagem').html(((somatorioValoresTrue / somatorioValoresTotal) * 100).toFixed(2) + "%");
                $('#somatorioValoresFalseNumero').html(somatorioValoresFalse);
                $('#somatorioValoresFalsePorcentagem').html(((somatorioValoresFalse / somatorioValoresTotal) * 100).toFixed(2) + "%");
                $('#somatorioValoresTotal').html(somatorioValoresTotal)

                //Linha 5
                var referenciasTrue = dados[0].referenciasTrue;
                var referenciasFalse = dados[0].referenciasFalse;
                var referenciasTotal = referenciasFalse + referenciasTrue;
                $('#referenciasTrueNumero').html(referenciasTrue);
                $('#referenciasTruePorcentagem').html(((referenciasTrue / referenciasTotal) * 100).toFixed(2) + "%");
                $('#referenciasFalseNumero').html(referenciasFalse);
                $('#referenciasFalsePorcentagem').html(((referenciasFalse / referenciasTotal) * 100).toFixed(2) + "%");
                $('#referenciasTotal').html(referenciasTotal)

                //Linha 6
                var equilibrioTrue = dados[0].equilibrioTrue;
                var equilibrioFalse = dados[0].equilibrioFalse;
                var equilibrioTotal = equilibrioTrue + equilibrioFalse;
                $('#equilibrioTrueNumero').html(equilibrioTrue);
                $('#equilibrioTruePorcentagem').html(((equilibrioTrue / equilibrioTotal) * 100).toFixed(2) + "%");
                $('#equilibrioFalseNumero').html(equilibrioFalse);
                $('#equilibrioFalsePorcentagem').html(((equilibrioFalse / equilibrioTotal) * 100).toFixed(2) + "%");
                $('#equilibrioTotal').html(equilibrioTotal)

            }
        }
    });
}