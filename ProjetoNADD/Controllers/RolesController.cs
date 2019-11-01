using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetoNADD.Data;

namespace ProjetoNADD.Controllers
{
    public class RolesController : Controller
    {
        private readonly ProjetoNADDContext _context;

        public RolesController(ProjetoNADDContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Roles = _context.Roles.ToList();
            return View(Roles);
        }
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            _context.Roles.Add(Role);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}