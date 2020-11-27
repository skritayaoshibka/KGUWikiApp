using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KGUWiki.Models;
using KGUWiki.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KGUWiki.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string fullname, int? facultyId, int? departmentId)
        {
            IEnumerable<UniversityEmployee> employees = _context.UniversityEmployees
                .Include(x => x.Department)
                .ThenInclude(x => x.Faculty);

            SelectList faculties = new SelectList(_context.Faculties, "Id", "FullName");
            ViewBag.Faculties = faculties;

            SelectList departments = new SelectList(_context.Departments, "Id", "FullName");
            ViewBag.Departments = departments;

            if (!String.IsNullOrEmpty(fullname))
            {
                employees = FilterUniversityEmployeesByFullName(fullname, employees);
            }

            if (departmentId != null)
            {
                employees = FilterUniversityEmployeesByDepartment(departmentId, employees);
            }
            else
            {
                if (facultyId != null)
                {
                    employees = FilterUniversityEmployeesByFaculty(facultyId, employees);
                }
            }

            return View(employees.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Info(int? id)
        {
            if (id != null)
            {
                var employee = await _context.UniversityEmployees
                    .Include(x => x.Department)
                    .ThenInclude(x => x.Faculty)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (employee != null)
                {
                    return View(employee);
                }
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        private IEnumerable<UniversityEmployee> FilterUniversityEmployeesByFaculty(int? facultyId, 
                                                                                   IEnumerable<UniversityEmployee> employees)
        {
            if (facultyId != null)
            {
                return employees.Where(x => x.Department.Faculty.Id == facultyId);
            }

            return employees;
        }

        private IEnumerable<UniversityEmployee> FilterUniversityEmployeesByDepartment(int? departmentId, 
                                                                                      IEnumerable<UniversityEmployee> employees)
        {
            if (departmentId != null)
            {
                return employees.Where(x => x.Department.Id == departmentId);
            }

            return employees;
        }

        private IEnumerable<UniversityEmployee> FilterUniversityEmployeesByFullName(string fullname, 
                                                                                    IEnumerable<UniversityEmployee> employees)
        {
            if (!String.IsNullOrEmpty(fullname))
            {
                return employees.Where(x => x.FullName.ToLower().Contains(fullname.ToLower()));
            }

            return employees;
        }
    }
}
