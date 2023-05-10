using Microsoft.AspNetCore.Mvc;
using Session101.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using DevOne.Security.Cryptography.BCrypt;

namespace Session101.Controllers
{
    public class SessionController : Controller
    {
        private readonly Context _context;

        public SessionController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var acesso = HttpContext.Session.GetString("usuario_session");
            if (acesso != null)
                return View(_context.Usuarios);
            else
                return View("Login");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            usuario.Senha = BCryptHelper.HashPassword(usuario.Senha, BCryptHelper.GenerateSalt());

            _context.Usuarios.Add(usuario);
            //usuario.UsuarioID = _context.Usuarios.Select(u => u.UsuarioID).Max() + 1;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _context.Usuarios.Where(u => u.Email == email).FirstOrDefault();
            if (usuario == null)
                return RedirectToAction(nameof(Index));

            var confirma = _context.Usuarios.Where
                (u => u.Email.Equals(email) && BCryptHelper.CheckPassword(senha, usuario.Senha))
                .FirstOrDefault();

            if (confirma != null)
            {
                HttpContext.Session.SetString("usuario_session", confirma.Nome);
                return RedirectToAction("Correto");
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Correto()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            //Response.Cookies.Delete(".AspNetCore.Session");
            return RedirectToAction("Login");
        }
    }
}
