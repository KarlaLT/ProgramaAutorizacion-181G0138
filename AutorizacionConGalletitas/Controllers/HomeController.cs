using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using AutorizacionConGalletitas.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace AutorizacionConGalletitas.Controllers
{
    public class HomeController : Controller
    {
        public rolesContext Context { get; }
        public HomeController(rolesContext context)
        {
            Context = new rolesContext();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Publico()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string user, string password)
        {
            var usuario = Context.Registros.SingleOrDefault(x => x.Usuario == user
            && x.Contra == password);
            if (usuario != null)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, usuario.Usuario));
                claims.Add(new Claim(ClaimTypes.Role, usuario.Rol));

                var identidad = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(new ClaimsPrincipal(identidad));

                if (usuario.Rol == "admin")
                {
                    return View("Administrador");
                }
                else if (usuario.Rol == "usuario")
                {
                     return View("Usuario");
                }
                else
                    return View();
            }
            else
            {
                ModelState.AddModelError("", "El usuario o contraseña son incorrectos.");
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}
