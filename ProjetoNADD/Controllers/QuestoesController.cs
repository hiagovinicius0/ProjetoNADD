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
    public class QuestoesController : Controller
    {
        private readonly ProjetoNADDContext _context;

        public QuestoesController(ProjetoNADDContext context)
        {
            _context = context;
        }

        // GET: Questoes
        public async Task<IActionResult> Index()
        {
            var projetoNADDContext = _context.Questao.Include(q => q.Avaliacao);
            return View(await projetoNADDContext.ToListAsync());
        }

        // GET: Questoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questao = await _context.Questao
                .Include(q => q.Avaliacao)
                .FirstOrDefaultAsync(m => m.Id_Questao == id);
            if (questao == null)
            {
                return NotFound();
            }

            return View(questao);
        }

        // GET: Questoes/Create
        public IActionResult Create()
        {
            ViewData["Id_Avaliacao"] = new SelectList(_context.Avaliacao, "Id_Avaliacao", "Nome_Avaliacao");
            return View();
        }

        // POST: Questoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Questao,Id_Numero,Id_Avaliacao,Contextualizacao_Questao,Clareza_Questao,Complexidade_Questao,Observacoes_Questao")] Questao questao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Avaliacao"] = new SelectList(_context.Avaliacao, "Id_Avaliacao", "Nome_Avaliacao", questao.Id_Avaliacao);
            return View(questao);
        }

        // GET: Questoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questao = await _context.Questao.FindAsync(id);
            if (questao == null)
            {
                return NotFound();
            }
            ViewData["Id_Avaliacao"] = new SelectList(_context.Avaliacao, "Id_Avaliacao", "Nome_Avaliacao", questao.Id_Avaliacao);
            return View(questao);
        }

        // POST: Questoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Questao,Id_Numero,Id_Avaliacao,Contextualizacao_Questao,Clareza_Questao,Complexidade_Questao,Observacoes_Questao")] Questao questao)
        {
            if (id != questao.Id_Questao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestaoExists(questao.Id_Questao))
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
            ViewData["Id_Avaliacao"] = new SelectList(_context.Avaliacao, "Id_Avaliacao", "Nome_Avaliacao", questao.Id_Avaliacao);
            return View(questao);
        }

        // GET: Questoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questao = await _context.Questao
                .Include(q => q.Avaliacao)
                .FirstOrDefaultAsync(m => m.Id_Questao == id);
            if (questao == null)
            {
                return NotFound();
            }

            return View(questao);
        }

        // POST: Questoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questao = await _context.Questao.FindAsync(id);
            _context.Questao.Remove(questao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestaoExists(int id)
        {
            return _context.Questao.Any(e => e.Id_Questao == id);
        }
    }
}
