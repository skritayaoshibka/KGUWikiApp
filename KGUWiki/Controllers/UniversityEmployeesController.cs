using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KGUWiki.Models;
using KGUWiki.Models.Contexts;
using KGUWiki.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace KGUWiki.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UniversityEmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public UniversityEmployeesController(ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        //public UniversityEmployeesController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.UniversityEmployees
                .Include(x => x.Department)
                .ThenInclude(x => x.Faculty)
                .ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var universityEmployee = await _context.UniversityEmployees
                    .Include(x => x.Department)
                    .ThenInclude(x => x.Faculty)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (universityEmployee != null)
                {
                    return View(universityEmployee);
                }
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            SelectList faculties = new SelectList(_context.Faculties, "Id", "FullName");
            ViewBag.Faculties = faculties;

            SelectList departments = new SelectList(_context.Departments, "Id", "FullName");
            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UniversityEmployeeViewModel uevm)
        {
            UniversityEmployee universityEmployee = new UniversityEmployee();
            if (ModelState.IsValid)
            {
                universityEmployee.FullName = uevm.FullName;
                universityEmployee.Position = uevm.Position;
                universityEmployee.Email = uevm.Email;
                universityEmployee.PhoneNumber = uevm.PhoneNumber;
                universityEmployee.Description = uevm.Description;
                universityEmployee.DepartmnetId = uevm.DepartmentId;
                universityEmployee.Department = await _context.Departments.FindAsync(uevm.DepartmentId);
                Console.WriteLine(uevm.Photo);

                if (uevm.Photo != null)
                {
                    using (var binaryReader = new BinaryReader(uevm.Photo.OpenReadStream()))
                    {
                        byte[] photoData = binaryReader.ReadBytes((int)uevm.Photo.Length);
                        universityEmployee.Photo = photoData;
                    }
                }
                else
                {
                    byte[] photoData = System.IO.File.ReadAllBytes("wwwroot/img/employee_image.jpg");
                    universityEmployee.Photo = photoData;
                }

                _context.UniversityEmployees.Add(universityEmployee);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(universityEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                SelectList faculties = new SelectList(_context.Faculties, "Id", "FullName");
                ViewBag.Faculties = faculties;

                SelectList departments = new SelectList(_context.Departments, "Id", "FullName");
                ViewBag.Departments = departments;

                var universityEmployee = await _context.UniversityEmployees
                    .Include(x => x.Department)
                    .ThenInclude(x => x.Faculty)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (universityEmployee != null)
                {
                    return View(universityEmployee);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UniversityEmployeeViewModel uevm)
        {
            var universityEmployee = await _context.UniversityEmployees.FindAsync(uevm.Id);

            if (universityEmployee == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                universityEmployee.FullName = uevm.FullName;
                universityEmployee.Position = uevm.Position;
                universityEmployee.Email = uevm.Email;
                universityEmployee.PhoneNumber = uevm.PhoneNumber;
                universityEmployee.Description = uevm.Description;
                universityEmployee.DepartmnetId = uevm.DepartmentId;
                universityEmployee.Department = await _context.Departments.FindAsync(uevm.DepartmentId);

                if (uevm.Photo != null)
                {
                    using (var binaryReader = new BinaryReader(uevm.Photo.OpenReadStream()))
                    {
                        byte[] photoData = binaryReader.ReadBytes((int)uevm.Photo.Length);
                        universityEmployee.Photo = photoData;
                    }
                }

                _context.UniversityEmployees.Update(universityEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(universityEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var universityEmployee = await _context.UniversityEmployees
                    .Include(x => x.Department)
                    .ThenInclude(x => x.Faculty)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (universityEmployee != null)
                {
                    return View(universityEmployee);
                }
            }

            return NotFound();
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var universityEmployee = await _context.UniversityEmployees.FindAsync(id);
            _context.UniversityEmployees.Remove(universityEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        private bool UniversityEmployeeExists(int id)
        {
            return _context.UniversityEmployees.Any(e => e.Id == id);
        }

        [HttpGet]
        public ActionResult GetDepartmentsFromFaculty(int facultyId)
        {
            return PartialView(_context.Departments.Where(x => x.FacultyId == facultyId).ToList());
        }
    }
}