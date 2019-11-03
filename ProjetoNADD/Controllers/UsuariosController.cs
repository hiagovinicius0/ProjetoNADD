using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuariosController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ProjetoNADDContext context, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var roles = _roleManager.Roles;
            ViewBag.Roles = new SelectList(_context.Roles.ToList(), "Name", "Name");
            return View(roles);
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
            var usuario2 = _context.Usuario.Where(u => u.Id == id).First();
            ViewBag.Roles = string.Join(",",await userManager.GetRolesAsync(usuario2));
            return View(usuario);
        }
        [HttpPost]
        public async Task<string> GetRolesLogado()
        {
            var Usuariouser = await userManager.GetUserAsync(User);

            var roles =  string.Join(",", await userManager.GetRolesAsync(Usuariouser));
            return roles;
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
        public async  Task<object> Create(string Email, string Nome, string Senha, string Role, int Curso)
        {
            var user = new Usuario
            {
                UserName = Email,
                Nome_User = Nome,
                Email = Email,
                Curso = Curso
            };
            var result = await userManager.CreateAsync(user, Senha);
            if (result.Succeeded)
            {
                if (!await userManager.IsInRoleAsync(user, Role))
                {
                    var userResult = await userManager.AddToRoleAsync(user, Role);
                    return userResult;
                }
                else
                {
                    return result;
                }
            }
            else
            {
                return user;
            }
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
            var usuario2 = _context.Usuario.Where(u => u.Id == id).First();
            ViewBag.Roles = string.Join(",", await userManager.GetRolesAsync(usuario2));
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
            var roles = _context.Roles.ToList();
            if (roles.Count() == 0)
            {
                var role = new IdentityRole[]
                {
                    new IdentityRole{Name = "NADD", NormalizedName = "NADD"},
                    new IdentityRole{Name = "Coordenador", NormalizedName = "COORDENADOR"},
                    new IdentityRole{Name = "Pró-Reitoria", NormalizedName = "PRO-REITORIA"}
                };
                foreach (IdentityRole comp1 in role)
                {
                    _context.Roles.Add(comp1);
                }
                _context.SaveChanges();
            }
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
                if (result1.Succeeded)
                {
                    if (!await userManager.IsInRoleAsync(usuario, "NADD"))
                    {
                        var userResult = await userManager.AddToRoleAsync(usuario, "NADD");
                    }
                }
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