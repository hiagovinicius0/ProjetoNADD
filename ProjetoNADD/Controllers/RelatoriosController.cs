using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Avaliacoes()
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
    }
}