using KGUWiki.Models.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace KGUWiki.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //Получаем список таблиц в БД и сортируем его
            var model = _context.Model;
            var entityTypes = model.GetEntityTypes().ToList();
            List<string> tableNames = new List<string>();
            foreach (var entity in entityTypes)
            {
                tableNames.Add(entity.GetAnnotation("Relational:TableName").Value.ToString());
            }
            tableNames.Sort();

            return View(tableNames);
        }
    }
}
