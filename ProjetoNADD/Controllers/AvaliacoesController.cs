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
    public class AvaliacoesController : Controller
    {
        private readonly ProjetoNADDContext _context;

        public AvaliacoesController(ProjetoNADDContext context)
        {
            _context = context;
        }

        // GET: Avaliacoes
        public async Task<IActionResult> Index()
        {
            var projetoNADDContext = _context.Avaliacao.Include(a => a.Disciplina);
            return View(await projetoNADDContext.ToListAsync());
        }

        // GET: Avaliacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacao
                .Include(a => a.Disciplina)
                .FirstOrDefaultAsync(m => m.Id_Avaliacao == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // GET: Avaliacoes/Create
        public IActionResult Create()
        {
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplina, "Id_Disciplina", "Nome_Disciplina");
            return View();
        }

        // POST: Avaliacoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Avaliacao,Nome_Avaliacao,ValorExplicitoProva_Avaliacao,ValorExplicitoQuestoes_Avaliacao,SomatorioQuestoes_Avaliacao,Referencias_Avaliacao,QuestoesMEeD_Avaliacao,ValorProva_Avaliacao,NumeroQuestoes_Avaliacao,EquilibrioValorQuestoes_Avaliacao,Diversificacao_Avaliacao,Contextualidade_Avaliacao,Observacoes_Avaliacao,DisciplinaId")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avaliacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplina, "Id_Disciplina", "Nome_Disciplina", avaliacao.DisciplinaId);
            return View(avaliacao);
        }

        // GET: Avaliacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacao.FindAsync(id);
            if (avaliacao == null)
            {
                return NotFound();
            }
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplina, "Id_Disciplina", "Nome_Disciplina", avaliacao.DisciplinaId);
            return View(avaliacao);
        }

        // POST: Avaliacoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Avaliacao,Nome_Avaliacao,ValorExplicitoProva_Avaliacao,ValorExplicitoQuestoes_Avaliacao,SomatorioQuestoes_Avaliacao,Referencias_Avaliacao,QuestoesMEeD_Avaliacao,ValorProva_Avaliacao,NumeroQuestoes_Avaliacao,EquilibrioValorQuestoes_Avaliacao,Diversificacao_Avaliacao,Contextualidade_Avaliacao,Observacoes_Avaliacao,DisciplinaId")] Avaliacao avaliacao)
        {
            if (id != avaliacao.Id_Avaliacao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avaliacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvaliacaoExists(avaliacao.Id_Avaliacao))
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
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplina, "Id_Disciplina", "Nome_Disciplina", avaliacao.DisciplinaId);
            return View(avaliacao);
        }

        // GET: Avaliacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacao
                .Include(a => a.Disciplina)
                .FirstOrDefaultAsync(m => m.Id_Avaliacao == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // POST: Avaliacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avaliacao = await _context.Avaliacao.FindAsync(id);
            _context.Avaliacao.Remove(avaliacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvaliacaoExists(int id)
        {
            return _context.Avaliacao.Any(e => e.Id_Avaliacao == id);
        }
    }
}
