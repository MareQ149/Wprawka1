using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wprawka1.Models;

namespace Wprawka1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Strona g³ówna z list¹ postaci i filtrowaniem
        public IActionResult Index(int? guildId, string? characterClass)
        {
            // Dropdown dla gildii
            ViewBag.Guilds = new SelectList(_context.Guilds, "Id", "Name");

            // Pobierz postacie z relacj¹ do Guild
            var characters = _context.Characters.Include(c => c.Guild).AsQueryable();

            // Filtrowanie po gildii
            if (guildId.HasValue)
            {
                characters = characters.Where(c => c.GuildId == guildId.Value);
            }

            // Filtrowanie po klasie
            if (!string.IsNullOrEmpty(characterClass))
            {
                characters = characters.Where(c => c.Class == characterClass);
            }

            return View(characters.ToList());
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
    }
}