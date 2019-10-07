using ProjetoNADD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoNADD.Data
{
    public class ProjetoNADDDbInitializer
    {
        public static void Initialize(ProjetoNADDContext context)
        {
            context.Database.EnsureCreated();
            if (context.Area.Any())
            {
                return;
            }
            var areas = new Area[]
            {
                new Area{Id_Area = 1, Nome_Area = "Exatas"},
                new Area{Id_Area = 2, Nome_Area = "Humanas"},
                new Area{Id_Area = 3, Nome_Area = "Saúde"}
            };
            var professores = new Professor[]
            {
                new Professor{Id_Professor = 1, Nome_Professor = "Rosenclever"},
                new Professor{Id_Professor = 2, Nome_Professor = "Adilson"}
            };
            var cursos = new Curso[]
            {
                new Curso{Id_Curso = 1, Nome_Curso="Sistemas de Informaçao", AreaId = 1},
                new Curso{Id_Curso = 2, Nome_Curso="Enfermagem", AreaId = 3}
            };
            var disciplinas = new Disciplina[]
            {
                new Disciplina{Id_Disciplina = 1, Nome_Disciplina = "Banco de Dados", Ano_Disciplina = 2019, Periodo_Disciplina = 0, CursoId = 1},
                new Disciplina{Id_Disciplina = 2, Nome_Disciplina = "Gestão de Projetos", Ano_Disciplina = 2018, Periodo_Disciplina = 0, CursoId = 1}
            };
            var disciplinasProfessores = new DisciplinaProfessor[]
            {
                new DisciplinaProfessor{Disciplina_id = 1, Professor_id = 1},
                new DisciplinaProfessor{Disciplina_id = 2, Professor_id = 2}
            };
            var avaliacoes = new Avaliacao[]
            {
                new Avaliacao{Id_Avaliacao = 1, Nome_Avaliacao = "Prova Banco de dados 2019 1º Bimestre", ValorExplicitoProva_Avaliacao = true, ValorExplicitoQuestoes_Avaliacao = true, SomatorioQuestoes_Avaliacao = true, Referencias_Avaliacao = false, QuestoesMEeD_Avaliacao = true, ValorProva_Avaliacao = 7.0, NumeroQuestoes_Avaliacao = 30, EquilibrioValorQuestoes_Avaliacao = false, Diversificacao_Avaliacao = true, Contextualidade_Avaliacao = false, Observacoes_Avaliacao = "Faltou alguns detalhes na Avaliação mas no geral está bom", DisciplinaId = 1},
                new Avaliacao{Id_Avaliacao = 2, Nome_Avaliacao = "Prova Banco de Gestão de Projetos 2019 2º Bimestre", ValorExplicitoProva_Avaliacao = false, ValorExplicitoQuestoes_Avaliacao = false, SomatorioQuestoes_Avaliacao = false, Referencias_Avaliacao = true, QuestoesMEeD_Avaliacao = false, ValorProva_Avaliacao = 8.0, NumeroQuestoes_Avaliacao = 20, EquilibrioValorQuestoes_Avaliacao = true, Diversificacao_Avaliacao = false, Contextualidade_Avaliacao = true, Observacoes_Avaliacao = "Precisa melhorar", DisciplinaId = 2}
            };
            var questoes = new Questao[]
            {
                new Questao{Id_Numero = 1, Id_Avaliacao = 1, Clareza_Questao = true, Complexidade_Questao = true, Contextualizacao_Questao = true, Observacoes_Questao = "Questão Bem Elaborada"},
                new Questao{Id_Numero = 1, Id_Avaliacao = 2, Clareza_Questao = false, Complexidade_Questao = false, Contextualizacao_Questao = false, Observacoes_Questao = "Questão Mal Elaborada"},
                new Questao{Id_Numero = 2, Id_Avaliacao = 1, Clareza_Questao = false, Complexidade_Questao = false, Contextualizacao_Questao = false, Observacoes_Questao = "Questão Mal Elaborada"},
                new Questao{Id_Numero = 2, Id_Avaliacao = 2, Clareza_Questao = true, Complexidade_Questao = true, Contextualizacao_Questao = true, Observacoes_Questao = "Questão Bem Elaborada"}
            };
            foreach (Area area in areas)
            {
                context.Area.Add(area);
            }
            foreach (Professor professor in professores)
            {
                context.Professor.Add(professor);
            }
            foreach (Curso curso in cursos)
            {
                context.Curso.Add(curso);
            }
            foreach (Disciplina disciplina in disciplinas)
            {
                context.Disciplina.Add(disciplina);
            }
            foreach (DisciplinaProfessor disciplinaProfessor in disciplinasProfessores)
            {
                context.DisciplinaProfessor.Add(disciplinaProfessor);
            }
            foreach (Avaliacao avaliacao in avaliacoes)
            {
                context.Avaliacao.Add(avaliacao);
            }
            foreach (Questao questao in questoes)
            {
                context.Questao.Add(questao);
            }
            context.SaveChanges();
        }
    }
}
