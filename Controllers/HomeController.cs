using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TPN05.Models;
using try_catch_poc.Models;

namespace TPN05.Controllers;

public class HomeController : Controller
{
    private const string SessionUserKey = "NombreUsuario";

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    public IActionResult Login(string NombreUsuario, string Contraseña)
    {
        BD bd = new BD();
        Usuarios usuario = bd.GetUsuario(NombreUsuario, Contraseña);
        if (usuario != null)
        {
            HttpContext.Session.SetString(SessionUserKey, usuario.NombreUsuario);
            return RedirectToAction(nameof(Bienvenida));
        }
        else
        {
            ViewBag.ErrorMessage = "Nombre de usuario o contraseña incorrectos.";
            return View("Index");
        }

    }

    public IActionResult Bienvenida()
    {
        var nombreUsuario = HttpContext.Session.GetString(SessionUserKey);
        if (string.IsNullOrEmpty(nombreUsuario))
        {
            return RedirectToAction(nameof(Index));
        }

        ViewBag.NombreUsuario = nombreUsuario;
        return View();
    }

    public IActionResult Register(string NombreUsuario, string Contraseña, string Nombre, string Apellido, string TipoUsuario)
    {
        if (NombreUsuario == null || Contraseña == null || Nombre == null || Apellido == null || TipoUsuario == null)
        {
            ViewBag.ErrorMessage = "Todos los campos son obligatorios.";
            return View("Registrar");
        }
        int existingUserCount;
        BD bd = new BD();
        Usuarios usuario = new Usuarios
        {
            NombreUsuario = NombreUsuario,
            Contraseña = Contraseña,
            Nombre = Nombre,
            Apellido = Apellido,
            TipoUsuario = TipoUsuario
        };
        existingUserCount = bd.VerificarUsuarioExistente(NombreUsuario);
        if (existingUserCount > 0)
        {
            ViewBag.ErrorMessage = "El nombre de usuario ya está en uso. Por favor, elige otro.";
            return View("Index");
        }
        bd.RegistrarUsuario(usuario);
        return RedirectToAction("Bienvenida");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
}

