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
    public class AnosController : Controller
    {
        private readonly ProjetoNADDContext _context;

        public AnosController(ProjetoNADDContext context)
        {
            _context = context;
        }

        // GET: Anos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ano.ToListAsync());
        }

        // GET: Anos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ano = await _context.Ano
                .FirstOrDefaultAsync(m => m.Ano_id == id);
            if (ano == null)
            {
                return NotFound();
            }

            return View(ano);
        }

        // GET: Anos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Anos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Create(string Ano)
        {
            var anoteste = _context.Ano.Where(a => a.Ano_id == Ano).ToList();
            if(anoteste.Count == 0)
            {
                var novoAno = new Ano();
                novoAno.Ano_id = Ano;
                _context.Add(novoAno);
                _context.SaveChanges();
                return "SUCCESS";
            }
            else
            {
                return "ERRO";
            }
        }

        // GET: Anos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ano = await _context.Ano.FindAsync(id);
            if (ano == null)
            {
                return NotFound();
            }
            return View(ano);
        }

        // POST: Anos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Ano_id")] Ano ano)
        {
            if (id != ano.Ano_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ano);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnoExists(ano.Ano_id))
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
            return View(ano);
        }

        // GET: Anos/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ano = await _context.Ano
                .FirstOrDefaultAsync(m => m.Ano_id == id);
            if (ano == null)
            {
                return NotFound();
            }

            return View(ano);
        }

        // POST: Anos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ano = await _context.Ano.FindAsync(id);
            _context.Ano.Remove(ano);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnoExists(string id)
        {
            return _context.Ano.Any(e => e.Ano_id == id);
        }
        [HttpPost]
        public object GetAnos() {
            var anos = from a in _context.Ano
                       orderby a.Ano_id
                       select new
                       {
                           Ano = a.Ano_id
                       };
            return anos;
        }
    }
}
