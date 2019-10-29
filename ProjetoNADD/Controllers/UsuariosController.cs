using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoNADD.Data;
using ProjetoNADD.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoNADD.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ProjetoNADDContext _context;

        public UsuariosController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ProjetoNADDContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        public object GetUsuarios()
        {
            object p = _context.Usuario.Select(u => new
            {
                Nome_Usuario = u.Nome_User,
                Id_Usuario = u.Id,
                Email_Usuario = u.Email
            });
            return p;
        }
        [HttpPost]
        public async  Task<object> Create(string Email, string Nome, string Senha)
        {
            var user = new Usuario()
            {
                UserName = Email,
                Nome_User = Nome,
                Email = Email
            };
            var result = await userManager.CreateAsync(user, Senha);
            return result;
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        public string DeleteUser(string id)
        {
            var usuario = _context.Usuario.Where(c => c.Id == id).FirstOrDefault();
            _context.Usuario.Remove(usuario);
            _context.SaveChanges();
            return "SUCESS";
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            var query = _context.Usuario.Where(u => u.Email == "administrador@email.com").ToList();
            if (query.Count == 0)
            {
                string senhaAdmin = "Ab12345@1";
                var usuario = new Usuario
                {
                    UserName = "administrador@email.com",
                    Nome_User = "ADMINISTRADOR",
                    Email = "administrador@email.com"
                };
                var result1 = await userManager.CreateAsync(usuario, senhaAdmin);
            }
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Senha, model.LembrarMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("index", "home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Usuarios");
        }
    }
}