using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using AeropuertoConlara.Models;
using AeropuertoConlara.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AeropuertoConlara.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Panel");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string NombreUsuario, string password)
        {
            // Verificar si el usuario existe
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == NombreUsuario);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuario no encontrado");
                return View();
            }

            // Verificar la contraseña
            var passwordHash = HashPassword(password);
            var passwordMatch = passwordHash == usuario.Password;

            if (!passwordMatch)
            {
                ModelState.AddModelError("", "Contraseña incorrecta");
                return View();
            }

            // Si llegamos aquí, las credenciales son correctas
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Panel");
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            ViewBag.ExisteUsuario = await _context.Usuarios.AnyAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string username, string password, string confirmPassword, string rol)
        {
            // Verificar si es el primer usuario
            var existeUsuario = await _context.Usuarios.AnyAsync();

            // Si no es el primer usuario, verificar que el usuario actual sea administrador
            if (existeUsuario && (!User.Identity.IsAuthenticated || !User.IsInRole("Admin")))
            {
                return RedirectToAction("Login");
            }

            // Validaciones
            if (string.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("username", "El nombre de usuario es obligatorio");
            }
            else if (username.Length < 3)
            {
                ModelState.AddModelError("username", "El nombre de usuario debe tener al menos 3 caracteres");
            }

            if (string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "La contraseña es obligatoria");
            }
            else if (password.Length < 6)
            {
                ModelState.AddModelError("password", "La contraseña debe tener al menos 6 caracteres");
            }

            if (string.IsNullOrEmpty(confirmPassword))
            {
                ModelState.AddModelError("confirmPassword", "La confirmación de contraseña es obligatoria");
            }
            else if (password != confirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Las contraseñas no coinciden");
            }

            if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario == username))
            {
                ModelState.AddModelError("username", "El nombre de usuario ya existe");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ExisteUsuario = existeUsuario;
                return View();
            }

            // Si es el primer usuario, forzar el rol de administrador
            if (!existeUsuario)
            {
                rol = "Admin";
            }

            // Crear el hash de la contraseña
            var passwordHash = HashPassword(password);

            var usuario = new Usuario
            {
                NombreUsuario = username,
                Password = passwordHash,
                Rol = rol,
                FechaCreacion = DateTime.Now
            };

            try
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                if (!existeUsuario)
                {
                    TempData["Mensaje"] = "Usuario administrador creado exitosamente. Por favor, inicie sesión.";
                    return RedirectToAction("Login");
                }

                TempData["Mensaje"] = "Usuario creado exitosamente";
                return RedirectToAction("Panel");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el usuario: " + ex.Message);
                ViewBag.ExisteUsuario = existeUsuario;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Panel()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }

            var rol = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            ViewBag.Rol = rol;
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            var inputHash = HashPassword(inputPassword);
            return inputHash == hashedPassword;
        }
    }
}
