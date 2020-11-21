using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KGUWiki.Models;
using KGUWiki.Models.Contexts;
using Microsoft.AspNetCore.Authorization;

namespace KGUWiki.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FacultiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacultiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Faculties.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var faculty = await _context.Faculties.FindAsync(id);

                if (faculty != null)
                {
                    return View(faculty);
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
        public async Task<IActionResult> Create(Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faculty);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(faculty);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var faculty = await _context.Faculties.FindAsync(id);
                if (faculty != null)
                {
                    return View(faculty);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                if (id == faculty.Id)
                {
                    _context.Faculties.Update(faculty);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                return View(faculty);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var faculty = await _context.Faculties.FindAsync(id);

                if (faculty != null)
                {
                    return View(faculty);
                }
            }

            return NotFound();
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
