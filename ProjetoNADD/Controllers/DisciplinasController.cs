using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoNADD.Data;
using ProjetoNADD.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoNADD.Controllers
{
    [Authorize]
    public class DisciplinasController : Controller
    {
        private readonly ProjetoNADDContext _context;

        public DisciplinasController(ProjetoNADDContext context)
        {
            _context = context;
        }

        // GET: Disciplinas
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public object GetDisciplinas()
        {
            var query = _context.Disciplina.Join(_context.Curso, d => d.CursoId, c => c.Id_Curso, (d, c) =>
            new {
                Id_Disciplina = d.Id_Disciplina,
                Nome_Disciplina = d.Nome_Disciplina,
                Periodo_Disciplina = d.Periodo_Disciplina,
                Ano_Disciplina = d.Ano_Disciplina,
                Nome_Curso = c.Nome_Curso
            }).ToList();
            return query;
        }

        // GET: Disciplinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplina
                .Include(d => d.Curso)
                .FirstOrDefaultAsync(m => m.Id_Disciplina == id);
            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        // GET: Disciplinas/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Curso, "Id_Curso", "Nome_Curso");
            ViewData["ProfessoresId"] = new SelectList(_context.Professor, "Id_Professor", "Nome_Professor");
            return View();
        }

        // POST: Disciplinas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Create(int[] professores, string Nome_Disciplina, int Periodo_Disciplina, int Ano_Disciplina, int CursoId)
        {
            Disciplina disciplina = new Disciplina();
            disciplina.Nome_Disciplina = Nome_Disciplina;
            disciplina.Periodo_Disciplina = Periodo_Disciplina;
            disciplina.Ano_Disciplina = Ano_Disciplina;
            disciplina.CursoId = CursoId;
            _context.Add(disciplina);
            _context.SaveChanges();
            if (professores.Count() > 0)
            {
                for (int i = 0; i < professores.Count(); i++)
                {
                    DisciplinaProfessor disciplinaProf = new DisciplinaProfessor();
                    disciplinaProf.Disciplina_id = disciplina.Id_Disciplina;
                    disciplinaProf.Professor_id = professores[i];
                    _context.Add(disciplinaProf);
                    _context.SaveChanges();
                }
            }
            return "SUCCESS";
        }
        [HttpPost]
        public object ProfessoresMarcados(int disciplina)
        {
            /*List<DisciplinaProfessor> result = _context.DisciplinaProfessor.FromSql("SELECT Professor_id FROM dbo.DisciplinaProfessor WHERE Disciplina_id = "+disciplina).ToList();
            return result.ToString();*/
            var query = _context.DisciplinaProfessor.Where(d => d.Disciplina_id == disciplina).Select(st => new {
                Id = st.Professor_id
            }).ToList();
            return query;
        }

        // GET: Disciplinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplina.FindAsync(id);
            if (disciplina == null)
            {
                return NotFound();
            }
            ViewData["ProfessoresId"] = new SelectList(_context.Professor, "Id_Professor", "Nome_Professor");
            ViewData["ProfessoresMarcados"] = new SelectList(_context.DisciplinaProfessor, "Professor_id", "", disciplina.Id_Disciplina);
            ViewData["CursoId"] = new SelectList(_context.Curso, "Id_Curso", "Nome_Curso", disciplina.CursoId);
            return View(disciplina);
        }

        // POST: Disciplinas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string  Edit(int Id_Disciplina, string Nome_Disciplina, int Periodo_Disciplina, int Ano_Disciplina, int CursoId, int[] professores)
        {
            Disciplina disciplina = _context.Disciplina.Where(d => d.Id_Disciplina == Id_Disciplina).FirstOrDefault<Disciplina>(); ;
            disciplina.Id_Disciplina = Id_Disciplina;
            disciplina.Nome_Disciplina = Nome_Disciplina;
            disciplina.Periodo_Disciplina = Periodo_Disciplina;
            disciplina.Ano_Disciplina = Ano_Disciplina;
            disciplina.CursoId = CursoId;
            _context.Update(disciplina);
            _context.SaveChanges();

            var cliente = _context.DisciplinaProfessor.Where(c => c.Disciplina_id == Id_Disciplina).ToList();

            _context.DisciplinaProfessor.RemoveRange(cliente);
            _context.SaveChanges();
            if (professores.Count() > 0)
            {
                for (int i = 0; i < professores.Count(); i++)
                {
                    DisciplinaProfessor disciplinaProf = new DisciplinaProfessor();
                    disciplinaProf.Disciplina_id = disciplina.Id_Disciplina;
                    disciplinaProf.Professor_id = professores[i];
                    _context.Add(disciplinaProf);
                    _context.SaveChanges();
                }
            }
            return "SUCCESS";
        }

        // GET: Disciplinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplina
                .Include(d => d.Curso)
                .FirstOrDefaultAsync(m => m.Id_Disciplina == id);
            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        // POST: Disciplinas/Delete/5
        [HttpPost]
        public string Delete(int id)
        {
            var cliente = _context.DisciplinaProfessor.Where(c => c.Disciplina_id == id).ToList();
            _context.DisciplinaProfessor.RemoveRange(cliente);
            _context.SaveChanges();
            var disciplina =  _context.Disciplina.Where(c => c.Id_Disciplina == id).FirstOrDefault();
            _context.Disciplina.Remove(disciplina);
            _context.SaveChanges();
            return "SUCESS";
        }

        private bool DisciplinaExists(int id)
        {
            return _context.Disciplina.Any(e => e.Id_Disciplina == id);
        }
    }
}
