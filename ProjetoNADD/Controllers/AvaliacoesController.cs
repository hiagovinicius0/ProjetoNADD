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
        [HttpPost]
        public object GetAvaliacoes()
        {
            var query = _context.Avaliacao.Join(_context.Disciplina, d => d.DisciplinaId, c => c.Id_Disciplina, (d, c) =>
            new {
                Id_Avaliacao = d.Id_Avaliacao,
                Nome_Avaliacao = d.Nome_Avaliacao,
                Observacoes_Avaliacao = d.Observacoes_Avaliacao,
                Nome_Disciplina = c.Nome_Disciplina
            }).ToList();
            return query;
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
        public string Create(string Nome_Avaliacao, bool ValorExplicitoProva_Avaliacao, bool ValorExplicitoQuestoes_Avaliacao, bool SomatorioQuestoes_Avaliacao, bool Referencias_Avaliacao, bool QuestoesMEeD_Avaliacao, double ValorProva_Avaliacao, int NumeroQuestoes_Avaliacao, bool EquilibrioValorQuestoes_Avaliacao, bool Diversificacao_Avaliacao, bool Contextualidade_Avaliacao, string Observacoes_Avaliacao, int DisciplinaId)
        {
            Avaliacao avaliacao = new Avaliacao();
            avaliacao.Nome_Avaliacao = Nome_Avaliacao;
            avaliacao.ValorExplicitoProva_Avaliacao = ValorExplicitoProva_Avaliacao;
            avaliacao.ValorExplicitoQuestoes_Avaliacao = ValorExplicitoQuestoes_Avaliacao;
            avaliacao.SomatorioQuestoes_Avaliacao = SomatorioQuestoes_Avaliacao;
            avaliacao.Referencias_Avaliacao = Referencias_Avaliacao;
            avaliacao.QuestoesMEeD_Avaliacao = QuestoesMEeD_Avaliacao;
            avaliacao.ValorProva_Avaliacao = ValorProva_Avaliacao;
            avaliacao.NumeroQuestoes_Avaliacao = NumeroQuestoes_Avaliacao;
            avaliacao.EquilibrioValorQuestoes_Avaliacao = EquilibrioValorQuestoes_Avaliacao;
            avaliacao.Diversificacao_Avaliacao = Diversificacao_Avaliacao;
            avaliacao.Contextualidade_Avaliacao = Contextualidade_Avaliacao;
            avaliacao.Observacoes_Avaliacao = Observacoes_Avaliacao;
            avaliacao.DisciplinaId = DisciplinaId;
            _context.Add(avaliacao);
            _context.SaveChanges();
            return "SUCCESS";
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
        public string Edit(int Id_Avaliacao, string Nome_Avaliacao, bool ValorExplicitoProva_Avaliacao, bool ValorExplicitoQuestoes_Avaliacao, bool SomatorioQuestoes_Avaliacao, bool Referencias_Avaliacao, bool QuestoesMEeD_Avaliacao, double ValorProva_Avaliacao, int NumeroQuestoes_Avaliacao, bool EquilibrioValorQuestoes_Avaliacao, bool Diversificacao_Avaliacao, bool Contextualidade_Avaliacao, string Observacoes_Avaliacao, int DisciplinaId)
        {
            Avaliacao avaliacao = _context.Avaliacao.Where(d => d.Id_Avaliacao == Id_Avaliacao).FirstOrDefault<Avaliacao>(); ;
            avaliacao.Nome_Avaliacao = Nome_Avaliacao;
            avaliacao.ValorExplicitoProva_Avaliacao = ValorExplicitoProva_Avaliacao;
            avaliacao.ValorExplicitoQuestoes_Avaliacao = ValorExplicitoQuestoes_Avaliacao;
            avaliacao.SomatorioQuestoes_Avaliacao = SomatorioQuestoes_Avaliacao;
            avaliacao.Referencias_Avaliacao = Referencias_Avaliacao;
            avaliacao.QuestoesMEeD_Avaliacao = QuestoesMEeD_Avaliacao;
            avaliacao.ValorProva_Avaliacao = ValorProva_Avaliacao;
            avaliacao.NumeroQuestoes_Avaliacao = NumeroQuestoes_Avaliacao;
            avaliacao.EquilibrioValorQuestoes_Avaliacao = EquilibrioValorQuestoes_Avaliacao;
            avaliacao.Diversificacao_Avaliacao = Diversificacao_Avaliacao;
            avaliacao.Contextualidade_Avaliacao = Contextualidade_Avaliacao;
            avaliacao.Observacoes_Avaliacao = Observacoes_Avaliacao;
            avaliacao.DisciplinaId = DisciplinaId;
            _context.Update(avaliacao);
            _context.SaveChanges();
            return "SUCCESS";
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
        [HttpPost]
        public string Delete(int id)
        {
            var avaliacao = _context.Avaliacao.Where(c => c.Id_Avaliacao == id).FirstOrDefault();
            _context.Avaliacao.Remove(avaliacao);
            _context.SaveChanges();
            return "SUCESS";
        }

        private bool AvaliacaoExists(int id)
        {
            return _context.Avaliacao.Any(e => e.Id_Avaliacao == id);
        }
    }
}
