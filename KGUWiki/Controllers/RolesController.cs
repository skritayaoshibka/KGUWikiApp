using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KGUWiki.Models;
using KGUWiki.Models.Contexts;
using Microsoft.AspNetCore.Authorization;

namespace KGUWiki.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var role = await _context.Roles.FindAsync(id);

                if (role != null)
                {
                    return View(role);
                }
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _context.Add(role);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var role = await _context.Roles.FindAsync(id);

                if (role != null)
                {
                    return View(role);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Role role)
        {
            if (ModelState.IsValid)
            {
                if (id == role.Id)
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                return View(role);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var role = await _context.Roles.FindAsync(id);

                if (role != null)
                {
                    return View(role);
                }
            }

            return NotFound();
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}