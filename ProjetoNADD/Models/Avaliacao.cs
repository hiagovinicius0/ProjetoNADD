﻿using ProjetoNADD.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjetoNADD.Models
{
    public class Avaliacao
    {
        [Key]
        [Display(Name = "Id")]
        public int Id_Avaliacao { get; set; }
        [Display(Name = "Nome")]
        public string Nome_Avaliacao { get; set; }
        [Display(Name = "Valor Explícito da Prova")]
        public bool ValorExplicitoProva_Avaliacao { get; set; } //Valor da prova explícito
        [Display(Name = "Valor Explícito das Questões")]
        public bool ValorExplicitoQuestoes_Avaliacao { get; set; } //Valor das questões explícito
        [Display(Name = "Somatório das Questões")]
        public bool SomatorioQuestoes_Avaliacao { get; set; } //Somatório dos valores das questões
        [Display(Name = "Referências")]
        public bool Referencias_Avaliacao { get; set; } //Referências bibliográficas
        [Display(Name = "Questões Diversificadas")]
        public string QuestoesMEeD_Avaliacao { get; set; } //Possui questões de Múltipla escolha e Discursivas
        [Display(Name = "Valor da Avaliação")]
        public double ValorProva_Avaliacao { get; set; } //Valor total da prova
        [Display(Name = "Número de Questões")]
        public int NumeroQuestoes_Avaliacao { get; set; } //Número de questões
        [Display(Name = "Equilíbrio dos Valores")]
        public bool EquilibrioValorQuestoes_Avaliacao { get; set; } //Equilíbrio na distribuição dos valores das questões
        [Display(Name = "Diversificação")]
        public bool Diversificacao_Avaliacao { get; set; } //Apresenta, explicitamente, diversificação na avaliação?
        [Display(Name = "Contextualização")]
        public bool Contextualidade_Avaliacao { get; set; } //A prova possui, pelo menos, uma questão contextualizada?
        [Display(Name = "Clareza")]
        public bool Clareza_Avaliacao { get; set; } //A prova possui, pelo menos, uma questão contextualizada?
        [Display(Name = "Complexidade")]
        public string Complexidade_Avaliacao { get; set; } //A prova possui, pelo menos, uma questão contextualizada?
        [Display(Name = "Observações")]
        public string Observacoes_Avaliacao { get; set; } //Observações
        [Display(Name = "Disciplina")]
        public int DisciplinaId { get; set; } // Disciplina
        [Display(Name = "Avaliador")]
        public string Avaliador_Avaliacao { get; set; } // Avaliador
        public Disciplina Disciplina { get; set; }
        public ICollection<Questao> Questoes { get; set; }
    }
}
