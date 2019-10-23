using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetoNADD.Data;
using ProjetoNADD.Models;
using System.Threading.Tasks;

namespace ProjetoNADD.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ProjetoNADDContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(ProjetoNADDContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
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
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = "ADMINISTRADOR", Email = "administrador@email.com" };
                var result1 = await userManager.CreateAsync(user, "Ab12345@1");
                if (result1.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                }
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Senha, model.LembrarMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                ModelState.AddModelError(string.Empty, "Credenciais Inválidas");
            }
            return View(model);
        }
    }
}