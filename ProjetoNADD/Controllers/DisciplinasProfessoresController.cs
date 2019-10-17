using System;
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
    public class DisciplinasProfessoresController : Controller
    {
        private readonly ProjetoNADDContext _context;

        public DisciplinasProfessoresController(ProjetoNADDContext context)
        {
            _context = context;
        }

        // GET: DisciplinasProfessores
        public async Task<IActionResult> Index()
        {
            return View(await _context.DisciplinaProfessor.ToListAsync());
        }

        // GET: DisciplinasProfessores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinaProfessor = await _context.DisciplinaProfessor
                .FirstOrDefaultAsync(m => m.Disciplina_id == id);
            if (disciplinaProfessor == null)
            {
                return NotFound();
            }

            return View(disciplinaProfessor);
        }

        // GET: DisciplinasProfessores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DisciplinasProfessores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Professor_id,Disciplina_id")] DisciplinaProfessor disciplinaProfessor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplinaProfessor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disciplinaProfessor);
        }

        // GET: DisciplinasProfessores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinaProfessor = await _context.DisciplinaProfessor.FindAsync(id);
            if (disciplinaProfessor == null)
            {
                return NotFound();
            }
            return View(disciplinaProfessor);
        }

        // POST: DisciplinasProfessores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Professor_id,Disciplina_id")] DisciplinaProfessor disciplinaProfessor)
        {
            if (id != disciplinaProfessor.Disciplina_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplinaProfessor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplinaProfessorExists(disciplinaProfessor.Disciplina_id))
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
            return View(disciplinaProfessor);
        }

        // GET: DisciplinasProfessores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinaProfessor = await _context.DisciplinaProfessor
                .FirstOrDefaultAsync(m => m.Disciplina_id == id);
            if (disciplinaProfessor == null)
            {
                return NotFound();
            }

            return View(disciplinaProfessor);
        }

        // POST: DisciplinasProfessores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplinaProfessor = await _context.DisciplinaProfessor.FindAsync(id);
            _context.DisciplinaProfessor.Remove(disciplinaProfessor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplinaProfessorExists(int id)
        {
            return _context.DisciplinaProfessor.Any(e => e.Disciplina_id == id);
        }
    }
}
