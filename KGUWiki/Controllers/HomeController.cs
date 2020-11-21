using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KGUWiki.Models;
using KGUWiki.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public IActionResult Index(string fullname)
        {
            IEnumerable<UniversityEmployee> employees = _context.UniversityEmployees;

            if(!String.IsNullOrEmpty(fullname))
            {
                employees = employees.Where(x => x.FullName.ToLower().Contains(fullname.ToLower()));
            }

            return View(employees.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Info(int? id)
        {
            if(id != null)
            {
                var employee = await _context.UniversityEmployees
                    .Include(x => x.Department)
                    .ThenInclude(x => x.Faculty)
                    .FirstOrDefaultAsync(x => x.Id == id);
                
                if(employee != null)
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
    }
}
