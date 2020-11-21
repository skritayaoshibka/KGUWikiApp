using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KGUWiki.Controllers;
using KGUWiki.Models;
using KGUWiki.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Unit.Tests
{
    public class FacultiesControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewResultWithListOfFaculties()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new FacultiesController(dbContext);

                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<List<Faculty>>(viewResult.Model);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task Deatails_ReturnsNotFoundNotNull()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new FacultiesController(dbContext);

                var result = await controller.Details(-1);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }

        [Fact]
        public async Task Deatails_ReturnsViewResultNotNullWithFaculty()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Add(new Faculty 
                {
                    FullName = "",
                    ShortName = ""
                });
                dbContext.SaveChanges();
                var controller = new FacultiesController(dbContext);

                var result = await controller.Details(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<Faculty>(viewResult.Model);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public void Create_ReturnsViewResultNotNull()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new FacultiesController(dbContext);

                var result = controller.Create();

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
            }
        }

        [Fact]
        public async Task Create_ReturnsRedirectToActionNotNull()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new FacultiesController(dbContext);

                var result = await controller.Create(new Faculty
                {
                    FullName = "",
                    ShortName = ""
                });

                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.NotNull(redirectToActionResult);
                Assert.Equal("Index", redirectToActionResult.ActionName);
            }
        }

        [Fact]
        public async Task Edit_ReturnsNotFoundResultNotNull()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.SaveChanges();
                var controller = new FacultiesController(dbContext);

                var result = await controller.Edit(1);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }

        [Fact]
        public async Task Edit_ReturnsViewResultNotNullWithFaculty()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Add(new Faculty 
                {
                    FullName = "",
                    ShortName = ""
                });
                dbContext.SaveChanges();
                var controller = new FacultiesController(dbContext);

                var result = await controller.Edit(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<Faculty>(viewResult.Model);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task Edit_ReturnsViewResultWithFaculty()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new FacultiesController(dbContext);

                var result = await controller.Edit(1, new Faculty {
                    Id = 2,
                    FullName = "",
                    ShortName = "" });

                var notFoundResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(notFoundResult);
                var model = Assert.IsAssignableFrom<Faculty>(notFoundResult.Model);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public  async Task Delete_ReturnsViewNotNullWithFaculty()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Add(new Faculty 
                {
                    FullName = "",
                    ShortName = ""
                });
                dbContext.SaveChanges();
                var controller = new FacultiesController(dbContext);

                var result = await controller.Delete(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<Faculty>(viewResult.Model);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public  async Task Delete_ReturnsNotFoundResultNotNull()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Add(new Faculty 
                {
                    FullName = "",
                    ShortName = ""
                });
                dbContext.SaveChanges();
                var controller = new FacultiesController(dbContext);

                var result = await controller.Delete(2);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }
    }
}