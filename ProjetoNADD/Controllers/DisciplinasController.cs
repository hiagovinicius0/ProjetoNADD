using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoNADD.Data;
using ProjetoNADD.Models;

namespace ProjetoNADD.Controllers
{
    public class DisciplinasController : Controller
    {
        private readonly ProjetoNADDContext _context;

        public DisciplinasController(ProjetoNADDContext context)
        {
            _context = context;
        }

        // GET: Disciplinas
        public async Task<IActionResult> Index()
        {
            var projetoNADDContext = _context.Disciplina.Include(d => d.Curso);
            return View(await projetoNADDContext.ToListAsync());
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
        /*[ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("Id_Disciplina,Nome_Disciplina,Periodo_Disciplina,Ano_Disciplina,CursoId")] Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var lstProfessor = Request.Form["Professor_id"];
            if (!string.IsNullOrEmpty(lstProfessor))
            {
                int[] splProfessor = lstProfessor.Select(int.Parse).ToArray();
                if(splProfessor.Count() > 0)
                {
                    for (int i = 0; i < splProfessor.Count(); i++)
                    {
                        var Disciplinaprof = new DisciplinaProfessor();
                        Disciplinaprof.Disciplina_id = disciplina.Id_Disciplina;
                        Disciplinaprof.Professor_id = splProfessor[i];
                        _context.Add(Disciplinaprof);
                        _context.SaveChanges();
                    }
                    
                    
                }
            }

            ViewData["CursoId"] = new SelectList(_context.Curso, "Id_Curso", "Nome_Curso", disciplina.CursoId);
            string professores = 
            return View(disciplina);
        }*/

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Disciplina,Nome_Disciplina,Periodo_Disciplina,Ano_Disciplina,CursoId")] Disciplina disciplina)
        {
            if (id != disciplina.Id_Disciplina)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplinaExists(disciplina.Id_Disciplina))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Curso, "Id_Curso", "Nome_Curso", disciplina.CursoId);
            return View(disciplina);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplina = await _context.Disciplina.FindAsync(id);
            _context.Disciplina.Remove(disciplina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplinaExists(int id)
        {
            return _context.Disciplina.Any(e => e.Id_Disciplina == id);
        }
    }
}
