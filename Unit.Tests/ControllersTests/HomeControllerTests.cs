using System;
using System.Collections.Generic;
using KGUWiki.Controllers;
using KGUWiki.Models;
using KGUWiki.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Unit.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResultNotNull()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new HomeController(dbContext);

                var result = controller.Index("", null, null);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
            }
        }

        [Fact]
        public void Index_ReturnsViewResulWithListOfUniversityEmployees()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new HomeController(dbContext);

                var result = controller.Index("", null, null);
                
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<UniversityEmployee>>(viewResult.Model);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public void About_ReturnsViewResultNotNull()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new HomeController(dbContext);

                var result = controller.About();
                
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
            }
        }

        public void 
    }
}