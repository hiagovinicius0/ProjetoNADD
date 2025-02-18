﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using ProjetoNADD.Data;
using ProjetoNADD.Models;
using System.Linq;

namespace ProjetoNADD.Controllers
{
    [Authorize]
    public class RelatoriosController : Controller
    {

        private readonly ProjetoNADDContext _context;

        public RelatoriosController(ProjetoNADDContext context)
        {
            _context = context;
        }
        public IActionResult Avaliacoes()
        {
            return View();
        }
        public IActionResult Professor()
        {
            return View();
        }
        public IActionResult Coordenador()
        {
            return View();
        }
        public IActionResult Reitoria()
        {
            return View();
        }
        [HttpPost]
        public object GetCursos()
        {
            var email = User.Identity.Name;
            var usuario = _context.Usuario.Where(u => u.UserName == email).First();
            int? curso = usuario.Curso;
            if (curso == null || curso == 0)
            {
                var cursos = from c in _context.Curso
                             select new
                             {
                                 Id = c.Id_Curso,
                                 Nome = c.Nome_Curso
                             };
                return cursos;
            }
            else
            {
                var cursos = from c in _context.Curso
                             where c.Id_Curso == usuario.Curso
                             select new
                             {
                                 Id = c.Id_Curso,
                                 Nome = c.Nome_Curso
                             };
                return cursos;
            }
        }
        [HttpPost]
        public object BuscaDisciplinas(int Id_Curso)
        {
            var query = _context.Disciplina.Where(d => d.CursoId == Id_Curso).Select(d => new
            {
                Id = d.Id_Disciplina,
                Nome = d.Nome_Disciplina
            }).ToList();
            return query;
        }
        [HttpPost]
        public object BuscaAvaliacao(int Id_Disciplina)
        {
            var query = _context.Avaliacao.Where(d => d.DisciplinaId == Id_Disciplina).Select(d => new
            {
                Id = d.Id_Avaliacao,
                Nome = d.Nome_Avaliacao
            }).ToList();
            return query;
        }
        [HttpPost]
        public object BuscaRelatorio(int Id_Curso, int Id_Disciplina)
        {
            var query = from av in _context.Avaliacao
                        join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                        join c in _context.Curso on d.CursoId equals c.Id_Curso
                        join ar in _context.Area on c.AreaId equals ar.Id_Area
                        where c.Id_Curso == Id_Curso
                        where d.Id_Disciplina == Id_Disciplina
                        select new
                        {
                            Nome = av.Nome_Avaliacao,
                            Ano = d.Ano_Disciplina,
                            Curso = c.Nome_Curso,
                            Periodo = d.Periodo_Disciplina,
                            Area = ar.Nome_Area,
                            Contextualidade = av.Contextualidade_Avaliacao,
                            Complexidade = av.Complexidade_Avaliacao,
                            Clareza = av.Clareza_Avaliacao
                        };
            return query;                
        }
        [HttpPost]
        public object BuscaRelatorioProfessor(int Id_Curso, int Id_Disciplina, int Id_Avaliacao)
        {
            var query = from av in _context.Avaliacao
                        join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                        join c in _context.Curso on d.CursoId equals c.Id_Curso
                        join ar in _context.Area on c.AreaId equals ar.Id_Area
                        join us in _context.Usuario on c.Id_Curso equals us.Curso
                        where c.Id_Curso == Id_Curso
                        where d.Id_Disciplina == Id_Disciplina
                        select new
                        {
                            Id = av.Id_Avaliacao,
                            Nome = av.Nome_Avaliacao,
                            Ano = d.Ano_Disciplina,
                            Curso = c.Nome_Curso,
                            Disciplina = d.Nome_Disciplina,
                            Professor = string.Join(",", JsonConvert.SerializeObject((from professor in _context.Professor
                                         join dp in _context.DisciplinaProfessor on professor.Id_Professor equals dp.Professor_id
                                         where dp.Disciplina_id == d.Id_Disciplina
                                         select new { Nome = professor.Nome_Professor} ))),
                            Periodo = d.Periodo_Disciplina,
                            Area = ar.Nome_Area,
                            Contextualidade = av.Contextualidade_Avaliacao,
                            Complexidade = av.Complexidade_Avaliacao,
                            Clareza = av.Clareza_Avaliacao,
                            ValorProvaExplicito = av.ValorExplicitoProva_Avaliacao,
                            ValorQuestoes = av.ValorExplicitoQuestoes_Avaliacao,
                            SomatorioQuestoes = av.SomatorioQuestoes_Avaliacao,
                            Referencias = av.Referencias_Avaliacao,
                            QuestoesMEED = av.QuestoesMEeD_Avaliacao,
                            ValorTotalProva = av.ValorProva_Avaliacao,
                            NumeroQuestoes = av.NumeroQuestoes_Avaliacao,
                            EquilibrioDistribuicaoValores = av.EquilibrioValorQuestoes_Avaliacao,
                            Diversificacao = av.Diversificacao_Avaliacao,
                            QuestaoContextualizada = av.Contextualidade_Avaliacao,
                            Observacoes = av.Observacoes_Avaliacao,
                            Coordenador = us.Nome_User,
                            Avaliador = (from u in _context.Usuario
                                         where u.Id == av.Avaliador_Avaliacao
                                         select new {Avaliador = u.Nome_User}
                                         )
                        };
            return query;
        }
        [HttpPost]
        public object BuscaQuestoes(int Id_Avaliacao)
        {
            var query = from q in _context.Questao
                        join c in _context.Complexidade on q.ComplexidadeID equals c.Id_Complexidade
                        where q.Id_Avaliacao == Id_Avaliacao
                        orderby q.Id_Numero ascending
                        select new
                        {
                            Numero = q.Id_Numero,
                            Contextualizacao = q.Contextualizacao_Questao,
                            Clareza = q.Clareza_Questao,
                            Complexidade = c.Nome_Complexidade
                        };
            return query;
        }
        [HttpPost]
        public object BuscaAnos(int Id_Curso)
        {
            var query = from anos in _context.Ano
                        join d in _context.Disciplina on anos.Ano_id equals d.Ano_Disciplina
                        where d.CursoId == Id_Curso
                        select new
                        {
                            Ano = anos.Ano_id
                        };
            return query;
        }
        [HttpPost]
        public object BuscaAnosReitoria()
        {
            var query = from anos in _context.Ano
                        join d in _context.Disciplina on anos.Ano_id equals d.Ano_Disciplina
                        select new
                        {
                            Ano = anos.Ano_id
                        };
            return query;
        }
        [HttpPost]
        public object BuscaRelatorioCoordenador(int Id_Curso, string Id_Ano)
        {
            var query = from av in _context.Avaliacao
                        join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                        join c in _context.Curso on d.CursoId equals c.Id_Curso
                        join a in _context.Area on c.AreaId equals a.Id_Area
                        where d.CursoId == Id_Curso
                        where d.Ano_Disciplina == Id_Ano
                        select new
                        {
                            Ano = d.Ano_Disciplina,
                            Curso = c.Nome_Curso,
                            Periodo = d.Periodo_Disciplina,
                            Area = a.Nome_Area,
                            Contextualizacao = av.Contextualidade_Avaliacao,
                            Clareza = av.Clareza_Avaliacao,
                            Complexidade = av.Complexidade_Avaliacao
                        };
            return query;
        }
        [HttpPost]
        public object BuscaRelatorioReitoria(string Id_Ano)
        {
            var query = from av in _context.Avaliacao
                        join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                        where d.Ano_Disciplina == Id_Ano
                        select new
                        {
                            ValorExplicitoProvaTrue = (from av1 in _context.Avaliacao
                                                   join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                   where d.Ano_Disciplina == Id_Ano
                                                   where av1.ValorExplicitoProva_Avaliacao == true
                                                   select av1
                                                   ).Count(),
                            ValorExplicitoProvaFalse = (from av1 in _context.Avaliacao
                                                   join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                   where d.Ano_Disciplina == Id_Ano
                                                   where av1.ValorExplicitoProva_Avaliacao == false
                                                   select av1
                                                   ).Count(),
                            ValorQuestoesExplicitoTrue = (from av1 in _context.Avaliacao
                                                   join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                   where d.Ano_Disciplina == Id_Ano
                                                   where av1.ValorExplicitoQuestoes_Avaliacao == true
                                                   select av1
                                                   ).Count(),
                            ValorQuestoesExplicitoFalse = (from av1 in _context.Avaliacao
                                                   join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                   where d.Ano_Disciplina == Id_Ano
                                                   where av1.ValorExplicitoQuestoes_Avaliacao == false
                                                   select av1
                                                   ).Count(),
                            SomatorioValoresTrue = (from av1 in _context.Avaliacao
                                                   join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                   where d.Ano_Disciplina == Id_Ano
                                                   where av1.SomatorioQuestoes_Avaliacao == true
                                                   select av1
                                                   ).Count(),
                            SomatorioValoresFalse = (from av1 in _context.Avaliacao
                                                   join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                   where d.Ano_Disciplina == Id_Ano
                                                   where av1.SomatorioQuestoes_Avaliacao == false
                                                   select av1
                                                   ).Count(),
                            ReferenciasTrue = (from av1 in _context.Avaliacao
                                                   join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                   where d.Ano_Disciplina == Id_Ano
                                                   where av1.Referencias_Avaliacao == true
                                                   select av1
                                                   ).Count(),
                            ReferenciasFalse = (from av1 in _context.Avaliacao
                                                   join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                   where d.Ano_Disciplina == Id_Ano
                                                   where av1.Referencias_Avaliacao == false
                                                   select av1
                                                   ).Count(),
                            EquilibrioTrue = (from av1 in _context.Avaliacao
                                                   join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                   where d.Ano_Disciplina == Id_Ano
                                                   where av1.EquilibrioValorQuestoes_Avaliacao == true
                                                   select av1
                                                   ).Count(),
                            EquilibrioFalse = (from av1 in _context.Avaliacao
                                                   join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                   where d.Ano_Disciplina == Id_Ano
                                                   where av1.EquilibrioValorQuestoes_Avaliacao == false
                                                   select av1
                                                   ).Count(),
                            QuestoesMeedTrue = (from av1 in _context.Avaliacao
                                                   join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                   where d.Ano_Disciplina == Id_Ano
                                                   where av1.QuestoesMEeD_Avaliacao == "Apresenta Questões de Múltipla Escolha e Discursivas" 
                                                   select av1
                                                   ).Count(),
                            QuestoesMeedFalse = (from av1 in _context.Avaliacao
                                                join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                where d.Ano_Disciplina == Id_Ano
                                                where av1.QuestoesMEeD_Avaliacao == "Apresenta Somente Questões de Múltipla Escolha" || av1.QuestoesMEeD_Avaliacao == "Apresenta Somente Questões Discursivas"
                                                select av1
                                                ).Count(),
                            DiversificacaoTrue = (from av1 in _context.Avaliacao
                                                join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                where d.Ano_Disciplina == Id_Ano
                                                where av1.Diversificacao_Avaliacao == true
                                                select av1
                                                ).Count(),
                            DiversificacaoFalse = (from av1 in _context.Avaliacao
                                                join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                where d.Ano_Disciplina == Id_Ano
                                                where av1.Diversificacao_Avaliacao == false
                                                select av1
                                                ).Count(),
                            ContextualizacaoTrue = (from av1 in _context.Avaliacao
                                                join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                where d.Ano_Disciplina == Id_Ano
                                                where av1.Contextualidade_Avaliacao == true
                                                select av1
                                                ).Count(),
                            ContextualizacaoFalse = (from av1 in _context.Avaliacao
                                                join d in _context.Disciplina on av.DisciplinaId equals d.Id_Disciplina
                                                where d.Ano_Disciplina == Id_Ano
                                                where av1.Contextualidade_Avaliacao == false
                                                select av1
                                                ).Count(),
                            Conhecimento = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 1
                                                select q1
                                                ).Count(),
                            ConhecimentoExatas = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 1
                                                where ar1.Nome_Area == "Exatas"
                                                select q1
                                                ).Count(),
                            ConhecimentoHumanas = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 1
                                                where ar1.Nome_Area == "Humanas"
                                                select q1
                                                ).Count(),
                            ConhecimentoSaude = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 1
                                                where ar1.Nome_Area == "Saúde"
                                                select q1
                                                ).Count(),
                            Compreensao = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 2
                                                select q1
                                                ).Count(),
                            CompreensaoExatas = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 2
                                                where ar1.Nome_Area == "Exatas"
                                                select q1
                                                ).Count(),
                            CompreensaoHumanas = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 2
                                                where ar1.Nome_Area == "Humanas"
                                                select q1
                                                ).Count(),
                            CompreensaoSaude = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 2
                                                where ar1.Nome_Area == "Saúde"
                                                select q1
                                                ).Count(),
                            Aplicacao = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 3
                                                select q1
                                                ).Count(),
                            AplicacaoExatas = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 3
                                                where ar1.Nome_Area == "Exatas"
                                                select q1
                                                ).Count(),
                            AplicacaoHumanas = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 3
                                                where ar1.Nome_Area == "Humanas"
                                                select q1
                                                ).Count(),
                            AplicacaoSaude = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 3
                                                where ar1.Nome_Area == "Saúde"
                                                select q1
                                                ).Count(),
                            Analise = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 4
                                                select q1
                                                ).Count(),
                            AnaliseExatas = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 4
                                                where ar1.Nome_Area == "Exatas"
                                                select q1
                                                ).Count(),
                            AnaliseHumanas = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 4
                                                where ar1.Nome_Area == "Humanas"
                                                select q1
                                                ).Count(),
                            AnaliseSaude = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 4
                                                where ar1.Nome_Area == "Saúde"
                                                select q1
                                                ).Count(),
                            Sintese = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 5
                                                select q1
                                                ).Count(),
                            SinteseExatas = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 5
                                                where ar1.Nome_Area == "Exatas"
                                                select q1
                                                ).Count(),
                            SinteseHumanas = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 5
                                                where ar1.Nome_Area == "Humanas"
                                                select q1
                                                ).Count(),
                            SinteseSaude = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 5
                                                where ar1.Nome_Area == "Saúde"
                                                select q1
                                                ).Count(),
                            Avaliacao = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 6
                                                select q1
                                                ).Count(),
                            AvaliacaoExatas = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 6
                                                where ar1.Nome_Area == "Exatas"
                                                select q1
                                                ).Count(),
                            AvaliacaoHumanas = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 6
                                                where ar1.Nome_Area == "Humanas"
                                                select q1
                                                ).Count(),
                            AvaliacaoSaude = (from q1 in _context.Questao
                                                join av1 in _context.Avaliacao on q1.Id_Avaliacao equals av1.Id_Avaliacao
                                                join d1 in _context.Disciplina on av1.DisciplinaId equals d1.Id_Disciplina
                                                join cur1 in _context.Curso on d1.CursoId equals cur1.Id_Curso
                                                join ar1 in _context.Area on cur1.AreaId equals ar1.Id_Area
                                                where d1.Ano_Disciplina == Id_Ano
                                                where q1.ComplexidadeID == 6
                                                where ar1.Nome_Area == "Saúde"
                                                select q1
                                                ).Count(),
                        };
            return query;
        }
    }
}