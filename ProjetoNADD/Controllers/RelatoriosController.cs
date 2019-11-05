using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using ProjetoNADD.Data;
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
        [HttpPost]
        public object GetCursos()
        {
            var query = _context.Curso.Select(st => new {
                Id = st.Id_Curso,
                Nome = st.Nome_Curso
            }).ToList();
            return query;
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
    }
}