using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoNADD.Data;
using ProjetoNADD.Models;

namespace ProjetoNADD.Controllers
{
    [Authorize]
    public class AreasController : Controller
    {
        private readonly ProjetoNADDContext _context;

        public AreasController(ProjetoNADDContext context)
        {
            _context = context;
        }

        // GET: Areas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Area.ToListAsync());
        }
        [HttpPost]
        public object GetAreas()
        {
            var query = _context.Area.ToList();
            return query;
        }
        // GET: Areas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Area
                .FirstOrDefaultAsync(m => m.Id_Area == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // GET: Areas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Create(string Nome_Area)
        {
            Area area = new Area();
            area.Nome_Area = Nome_Area;
            _context.Add(area);
            _context.SaveChanges();
            return "SUCCESS";
        }

        // GET: Areas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Area.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            return View(area);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Edit(int Id_Area, string Nome_Area)
        {
            Area area = _context.Area.Where(d => d.Id_Area == Id_Area).FirstOrDefault<Area>(); ;
            area.Id_Area = Id_Area;
            area.Nome_Area = Nome_Area;
            _context.Update(area);
            _context.SaveChanges();
            return "SUCCESS";
        }

        // GET: Areas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Area
                .FirstOrDefaultAsync(m => m.Id_Area == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // POST: Areas/Delete/5
        [HttpPost]
        public string Delete(int id)
        {
            var area = _context.Area.Where(c => c.Id_Area == id).FirstOrDefault();
            _context.Area.Remove(area);
            _context.SaveChanges();
            return "SUCESS";
        }

        private bool AreaExists(int id)
        {
            return _context.Area.Any(e => e.Id_Area == id);
        }
    }
}
