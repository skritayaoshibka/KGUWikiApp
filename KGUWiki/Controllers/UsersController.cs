using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KGUWiki.Models;
using KGUWiki.Models.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace KGUWiki.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
                .Include(x => x.Role)
                .ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var user = await _context.Users
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (user != null)
                {
                    return View(user);
                }
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var user = await _context.Users
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (user != null)
                {
                    SelectList roles = new SelectList(_context.Roles, "Id", "Name");
                    ViewBag.Roles = roles;

                    return View(user);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, User user)
        {
            if (ModelState.IsValid)
            {
                if (id == user.Id)
                {
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                return View(user);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var user = await _context.Users
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (user != null)
                {
                    return View(user);
                }
            }

            return NotFound();
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}