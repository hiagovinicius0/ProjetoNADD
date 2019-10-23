using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoNADD.Data;
using ProjetoNADD.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoNADD.Controllers
{
    [Authorize]
    public class ProfessoresController : Controller
    {
        private readonly ProjetoNADDContext _context;

        public ProfessoresController(ProjetoNADDContext context)
        {
            _context = context;
        }

        // GET: Professores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Professor.ToListAsync());
        }
        [HttpPost]
        public object GetProfessores()
        {
            var query = _context.Professor.ToList();
            return query;
        }
        // GET: Professores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .FirstOrDefaultAsync(m => m.Id_Professor == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Create(string Nome_Professor)
        {
            Professor professor = new Professor();
            professor.Nome_Professor = Nome_Professor;
            _context.Add(professor);
            _context.SaveChanges();
            return "SUCCESS";
        }

        // GET: Professores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        // POST: Professores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Edit(int Id_Professor, string Nome_Professor)
        {
            Professor professor = _context.Professor.Where(d => d.Id_Professor == Id_Professor).FirstOrDefault<Professor>(); ;
            professor.Id_Professor = Id_Professor;
            professor.Nome_Professor = Nome_Professor;
            _context.Update(professor);
            _context.SaveChanges();
            return "SUCCESS";
        }

        // GET: Professores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .FirstOrDefaultAsync(m => m.Id_Professor == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professores/Delete/5
        [HttpPost]
        public string Delete(int id)
        {
            var professor = _context.Professor.Where(c => c.Id_Professor == id).FirstOrDefault();
            _context.Professor.Remove(professor);
            _context.SaveChanges();
            return "SUCESS";
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professor.Any(e => e.Id_Professor == id);
        }
    }
}
