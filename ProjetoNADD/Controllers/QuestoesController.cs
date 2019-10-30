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
    public class QuestoesController : Controller
    {
        private readonly ProjetoNADDContext _context;

        public QuestoesController(ProjetoNADDContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Index(int id_Avaliacao, string nome_Avaliacao)
        {
            var complexidade = _context.Complexidade.ToList();
            if (complexidade.Count() == 0)
            {
                var comp = new Complexidade[]
                {
                    new Complexidade{Nome_Complexidade = "Conhecimento"},
                    new Complexidade{Nome_Complexidade = "Compreensão"},
                    new Complexidade{Nome_Complexidade = "Aplicação"},
                    new Complexidade{Nome_Complexidade = "Análise"},
                    new Complexidade{Nome_Complexidade = "Síntese e Avaliação"}
                };
                foreach (Complexidade comp1 in comp)
                {
                    _context.Complexidade.Add(comp1);
                }
                _context.SaveChanges();
            }
            var TipoQuestao = _context.TipoQuestao.ToList();
            if (TipoQuestao.Count() == 0)
            {
                var tipo = new TipoQuestao[]
                {
                    new TipoQuestao{Nome_TipoQuestao = "Discursiva"},
                    new TipoQuestao{Nome_TipoQuestao = "Múltipla Escolha"}
                };
                foreach (TipoQuestao tipo1 in tipo)
                {
                    _context.TipoQuestao.Add(tipo1);
                }
                _context.SaveChanges();
            }
            ViewBag.Id = id_Avaliacao;
            ViewBag.Nome = nome_Avaliacao;
            return View();
        }
        [HttpPost]
        public JsonResult GetQuestoes(int Id_Avaliacao)
        {
            var query = _context.Questao.Where(c => c.Id_Avaliacao == Id_Avaliacao).Join(_context.Avaliacao, d => d.Id_Avaliacao, c => c.Id_Avaliacao, (d, c) =>
            new
            {
                Nome_Avaliacao = c.Nome_Avaliacao,
                Id_Numero = d.Id_Numero,
                Observacoes_Questao = d.Observacoes_Questao,
                Id_Questao = d.Id_Questao
            }).ToList();
            return Json(query);
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
                    .Include(q => q.TipoQuestao)
                    .Include(q => q.Complexidade)
                    .FirstOrDefaultAsync(m => m.Id_Questao == id);
            var tipo = (from q in _context.Questao
                        join tp in _context.TipoQuestao on q.TipoID equals tp.Id_TipoQuestao
                        where q.Id_Questao == id
                        select new
                        {
                            Nome_tipo = tp.Nome_TipoQuestao
                        }).FirstOrDefault();
            if (questao == null)
            {
                return NotFound();
            }
            ViewBag.Tipo = tipo.Nome_tipo;
            return View(questao);
        }

        // GET: Questoes/Create
        public IActionResult Create()
        {
            ViewData["ComplexidadeID"] = new SelectList(_context.Complexidade, "Id_Complexidade", "Nome_Complexidade");
            ViewData["TipoQuestao"] = new SelectList(_context.TipoQuestao, "Id_TipoQuestao", "Nome_TipoQuestao");
            ViewData["Id_Avaliacao"] = new SelectList(_context.Avaliacao, "Id_Avaliacao", "Nome_Avaliacao");
            return View();
        }

        // POST: Questoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Create(int Id_Numero, int Id_Avaliacao, bool Contextualizacao_Questao, bool Clareza_Questao, int Complexidade_Questao, string Observacoes_Questao, int TipoID)
        {
            Questao questao = new Questao();
            questao.Id_Numero = Id_Numero;
            questao.Id_Avaliacao = Id_Avaliacao;
            questao.Contextualizacao_Questao = Contextualizacao_Questao;
            questao.Clareza_Questao = Clareza_Questao;
            questao.ComplexidadeID = Complexidade_Questao;
            questao.Observacoes_Questao = Observacoes_Questao;
            questao.TipoID = TipoID;
            _context.Add(questao);
            _context.SaveChanges();
            return "SUCCESS";
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
            ViewData["ComplexidadeID"] = new SelectList(_context.Complexidade, "Id_Complexidade", "Nome_Complexidade", questao.ComplexidadeID);
            ViewData["TipoID"] = new SelectList(_context.TipoQuestao, "Id_TipoQuestao", "Nome_TipoQuestao", questao.TipoID);
            ViewData["Id_Avaliacao"] = new SelectList(_context.Avaliacao, "Id_Avaliacao", "Nome_Avaliacao", questao.Id_Avaliacao);
            return View(questao);
        }

        // POST: Questoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Edit(int Id_Questao, int Id_Numero, int Id_Avaliacao, bool Contextualizacao_Questao, bool Clareza_Questao, int Complexidade_Questao, string Observacoes_Questao, int TipoID)
        {
            Questao questao = _context.Questao.Where(d => d.Id_Questao == Id_Questao).FirstOrDefault<Questao>(); ;
            questao.Id_Numero = Id_Numero;
            questao.Contextualizacao_Questao = Contextualizacao_Questao;
            questao.Clareza_Questao = Clareza_Questao;
            questao.ComplexidadeID = Complexidade_Questao;
            questao.Observacoes_Questao = Observacoes_Questao;
            questao.TipoID = TipoID;
            _context.Update(questao);
            _context.SaveChanges();
            return "SUCCESS";
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
                .Include(q => q.TipoQuestao)
                .Include(q => q.Complexidade)
                .FirstOrDefaultAsync(m => m.Id_Questao == id);
            var tipo = (from q in _context.Questao
                       join tp in _context.TipoQuestao on q.TipoID equals tp.Id_TipoQuestao
                       where q.Id_Questao == id
                       select new
                       {
                           Nome_tipo = tp.Nome_TipoQuestao
                       }).FirstOrDefault();
            if (questao == null)
            {
                return NotFound();
            }
            ViewBag.Tipo = tipo.Nome_tipo;
            return View(questao);
        }

        // POST: Questoes/Delete/5
        [HttpPost]
        public string Delete(int id)
        {
            var questao = _context.Questao.Where(c => c.Id_Questao == id).FirstOrDefault();
            _context.Questao.Remove(questao);
            _context.SaveChanges();
            return "SUCESS";
        }

        private bool QuestaoExists(int id)
        {
            return _context.Questao.Any(e => e.Id_Questao == id);
        }
    }
}
