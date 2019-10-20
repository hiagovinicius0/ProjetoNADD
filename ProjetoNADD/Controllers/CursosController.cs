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
    public class CursosController : Controller
    {
        private readonly ProjetoNADDContext _context;

        public CursosController(ProjetoNADDContext context)
        {
            _context = context;
        }

        // GET: Cursos
        public async Task<IActionResult> Index()
        {
            var projetoNADDContext = _context.Curso.Include(c => c.Area);
            return View(await projetoNADDContext.ToListAsync());
        }
        [HttpPost]
        public object GetCursos()
        {
            var query = _context.Curso.Join(_context.Area, d => d.AreaId, c => c.Id_Area, (d, c) =>
            new {
                Id_Curso = d.Id_Curso,
                Nome_Curso = d.Nome_Curso,
                Nome_Area = c.Nome_Area
            }).ToList();
            return query;
        }
        // GET: Cursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso
                .Include(c => c.Area)
                .FirstOrDefaultAsync(m => m.Id_Curso == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // GET: Cursos/Create
        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(_context.Area, "Id_Area", "Nome_Area");
            return View();
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Create(string Nome_Curso, int AreaId)
        {
            Curso curso = new Curso();
            curso.Nome_Curso = Nome_Curso;
            curso.AreaId = AreaId;
            _context.Add(curso);
            _context.SaveChanges();
            return "SUCCESS";
        }

        // GET: Cursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            ViewData["AreaId"] = new SelectList(_context.Area, "Id_Area", "Nome_Area", curso.AreaId);
            return View(curso);
        }

        // POST: Cursos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Edit(int Id_Curso, string Nome_Curso, int AreaId)
        {
            Curso curso = _context.Curso.Where(d => d.Id_Curso == Id_Curso).FirstOrDefault<Curso>(); ;
            curso.Nome_Curso = Nome_Curso;
            curso.AreaId = AreaId;
            _context.Update(curso);
            _context.SaveChanges();
            return "SUCCESS";
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso
                .Include(c => c.Area)
                .FirstOrDefaultAsync(m => m.Id_Curso == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // POST: Cursos/Delete/5
        [HttpPost]
        public string Delete(int id)
        {
            var curso = _context.Curso.Where(c => c.Id_Curso == id).FirstOrDefault();
            _context.Curso.Remove(curso);
            _context.SaveChanges();
            return "SUCESS";
        }

        private bool CursoExists(int id)
        {
            return _context.Curso.Any(e => e.Id_Curso == id);
        }
    }
}
