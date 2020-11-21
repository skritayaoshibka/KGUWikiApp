using KGUWiki.Models;
using KGUWiki.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace KGUWiki.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments
                .Include(x => x.Faculty)
                .ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var department = await _context.Departments
                    .Include(x => x.Faculty)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (department != null)
                {
                    return View(department);
                }
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            SelectList faculties = new SelectList(_context.Faculties, "Id", "FullName");
            ViewBag.Faculties = faculties;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var department = await _context.Departments
                    .Include(x => x.Faculty)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (department != null)
                {
                    SelectList faculties = new SelectList(_context.Faculties, "Id", "FullName");
                    ViewBag.Faculties = faculties;

                    return View(department);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    _context.Departments.Update(department);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                return View(department);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var department = await _context.Departments
                    .Include(x => x.Faculty)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (department != null)
                {
                    return View(department);
                }
            }

            return NotFound();
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
