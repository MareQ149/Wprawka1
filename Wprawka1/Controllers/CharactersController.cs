using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Wprawka1.Controllers
{
    public class CharactersController : Controller
    {
        private readonly AppDbContext _context;

        public CharactersController(AppDbContext context)
        {
            _context = context;
        }

        // 🔐 CHECK LOGIN
        private bool IsLoggedIn()
        {
            return Request.Cookies["UserLogin"] != null;
        }

        // GET: Characters
        public async Task<IActionResult> Index()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var appDbContext = _context.Characters.Include(c => c.Guild);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            if (id == null)
                return NotFound();

            var character = await _context.Characters
                .Include(c => c.Guild)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (character == null)
                return NotFound();

            return View(character);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            ViewData["GuildId"] = new SelectList(_context.Guilds, "Id", "Name");
            return View();
        }

        // POST: Characters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Class,Level,GuildId")] Character character)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Add(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["GuildId"] = new SelectList(_context.Guilds, "Id", "Name", character.GuildId);
            return View(character);
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            if (id == null)
                return NotFound();

            var character = await _context.Characters.FindAsync(id);

            if (character == null)
                return NotFound();

            ViewData["GuildId"] = new SelectList(_context.Guilds, "Id", "Name", character.GuildId);
            return View(character);
        }

        // POST: Characters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Class,Level,GuildId")] Character character)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            if (id != character.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(character);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["GuildId"] = new SelectList(_context.Guilds, "Id", "Name", character.GuildId);
            return View(character);
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            if (id == null)
                return NotFound();

            var character = await _context.Characters
                .Include(c => c.Guild)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (character == null)
                return NotFound();

            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var character = await _context.Characters.FindAsync(id);

            if (character != null)
                _context.Characters.Remove(character);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}