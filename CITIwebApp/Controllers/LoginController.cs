using CITIwebApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CITIwebApp.Controllers
{
    public class LoginController : Controller
    {
        //inyencion de dependencias
        private MiContext _miContext;

        public LoginController(MiContext miContext)
        {
            _miContext = miContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string correo, string contrasena)
        {
            var user = await _miContext.Usuarios
                            .Where(x => x.Email == correo && x.Password == contrasena)
                            .FirstOrDefaultAsync();
            if (user != null)
            {
                //ha encontrado el usuario y entra al sistema, a la pantalla inical
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["LoginError"] = "Correo o Contrasena incorrectos";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Logout() {
            //cambiar a la pantalla login
            return RedirectToAction("Index");
        }
    }
}
