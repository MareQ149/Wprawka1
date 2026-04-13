using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    
    public IActionResult Register(User user, string password)
    {
        if (_context.Users.Any(u => u.Login == user.Login))
        {
            ModelState.AddModelError("Login", "Login już istnieje");
            return View(user);
        }

        if (ModelState.IsValid)
        {
            user.PasswordHash = PasswordHelper.Hash(password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new
                {
                    Field = x.Key,
                    Errors = x.Value.Errors.Select(e => e.ErrorMessage)
                });

            return Json(errors);
        }

        user.PasswordHash = PasswordHelper.Hash(password);

        _context.Users.Add(user);
        _context.SaveChanges();

        return Content("ZAREJESTROWANO");

        return View(user);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string login, string password)
    {
        var hash = PasswordHelper.Hash(password);

        var user = _context.Users
            .FirstOrDefault(u => u.Login == login && u.PasswordHash == hash);

        if (user != null)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddHours(1),
                HttpOnly = true
            };

            Response.Cookies.Append("UserLogin", user.Login, options);

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Błędny login lub hasło");
        return View();
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("UserLogin");
        return RedirectToAction("Index", "Home");
    }
}