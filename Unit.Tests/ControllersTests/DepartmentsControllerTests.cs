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
    public class DepartmentsControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewResultWithListOfDepartments()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new DepartmentsController(dbContext);

                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<List<Department>>(viewResult.Model);
                Assert.NotNull(model);

            }
        }

        [Fact]
        public async Task Details_ReturnsNotFoundResult()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new DepartmentsController(dbContext);

                var result = await controller.Details(-1);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }

        [Fact]
        public async Task Details_ReturnsViewResultWithDepartment()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Faculties.Add(new Faculty 
                {
                    FullName = "", 
                    ShortName = "",
                });
                dbContext.SaveChanges();
                dbContext.Departments.Add(new Department 
                {
                    FullName = "", 
                    ShortName = "", 
                    FacultyId = 1
                });
                dbContext.SaveChanges();
                var controller = new DepartmentsController(dbContext);

                var result = await controller.Details(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<Department>(viewResult.Model);
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
                var controller = new DepartmentsController(dbContext);

                var result = controller.Create();

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
            }
        }

        [Fact]
        public async Task Create_ReturnsRedirectToActionResult()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.SaveChanges();
                var controller = new DepartmentsController(dbContext);

                var result = await controller.Create(new Department
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
        public async Task Edit_ReturnsNotFoundResult()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new DepartmentsController(dbContext);

                var result = await controller.Edit(-1);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }

        [Fact]
        public async Task Edit_ReturnsViewResultWithDepartment()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Faculties.Add(new Faculty 
                {
                    FullName = "", 
                    ShortName = "",
                });
                dbContext.SaveChanges();
                dbContext.Departments.Add(new Department 
                {
                    FullName = "", 
                    ShortName = "", 
                    FacultyId = 1
                });
                dbContext.SaveChanges();
                var controller = new DepartmentsController(dbContext);

                var result = await controller.Edit(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<Department>(viewResult.Model);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task Edit_ReturnsVewResultWithDepartment()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new DepartmentsController(dbContext);

                var result = await controller.Edit(-1, new Department {FullName = "", ShortName = ""});

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<Department>(viewResult.Model);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundResult()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new DepartmentsController(dbContext);

                var result = await controller.Delete(-1);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }

        [Fact]
        public async Task Delete_ReturnsViewResultWithDepartment()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Faculties.Add(new Faculty 
                {
                    FullName = "", 
                    ShortName = "",
                });
                dbContext.SaveChanges();
                dbContext.Departments.Add(new Department 
                {
                    FullName = "", 
                    ShortName = "", 
                    FacultyId = 1
                });
                dbContext.SaveChanges();
                var controller = new DepartmentsController(dbContext);

                var result = await controller.Delete(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<Department>(viewResult.Model);
                Assert.NotNull(model);
            }
        }
    }
}