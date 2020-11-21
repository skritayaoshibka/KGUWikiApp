using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KGUWiki.Controllers;
using KGUWiki.Models;
using KGUWiki.Models.Contexts;
using KGUWiki.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Unit.Tests
{
    public class UniversityEmployeesControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewResultWithListOfUniversityEmployees()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var mock = new Mock<IWebHostEnvironment>();
                var controller = new UniversityEmployeesController(dbContext, mock.Object);

                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<List<UniversityEmployee>>(viewResult.Model);
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
                var mock = new Mock<IWebHostEnvironment>();
                var controller = new UniversityEmployeesController(dbContext, mock.Object);

                var result = await controller.Details(-1);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }

        [Fact]
        public async Task Details_ReturnsViewResultWithUniversityEmployee()
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
                dbContext.UniversityEmployees.Add( new UniversityEmployee
                {
                    DepartmnetId = 1
                });
                dbContext.SaveChanges();
                var mock = new Mock<IWebHostEnvironment>();
                var controller = new UniversityEmployeesController(dbContext, mock.Object);

                var result = await controller.Details(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<UniversityEmployee>(viewResult.Model);
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
                var mock = new Mock<IWebHostEnvironment>();
                var controller = new UniversityEmployeesController(dbContext, mock.Object);

                var result = controller.Create();

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
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
                var mock = new Mock<IWebHostEnvironment>();
                var controller = new UniversityEmployeesController(dbContext, mock.Object);

                var result = await controller.Edit(-1);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }

        [Fact]
        public async Task Edit_ReturnsViewResultWithUniversityEmployee()
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
                dbContext.UniversityEmployees.Add( new UniversityEmployee
                {
                    DepartmnetId = 1
                });
                dbContext.SaveChanges();
                var mock = new Mock<IWebHostEnvironment>();
                var controller = new UniversityEmployeesController(dbContext, mock.Object);

                var result = await controller.Edit(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<UniversityEmployee>(viewResult.Model);
                Assert.NotNull(model);
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
                var mock = new Mock<IWebHostEnvironment>();
                var controller = new UniversityEmployeesController(dbContext, mock.Object);

                var result = await controller.Edit(new UniversityEmployeeViewModel {FullName = ""});

                var viewResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(viewResult);
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
                var mock = new Mock<IWebHostEnvironment>();
                var controller = new UniversityEmployeesController(dbContext, mock.Object);

                var result = await controller.Delete(-1);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }

        [Fact]
        public async Task Delete_ReturnsViewResultWithUniversityEmployee()
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
                dbContext.UniversityEmployees.Add( new UniversityEmployee
                {
                    DepartmnetId = 1
                });
                dbContext.SaveChanges();
                var mock = new Mock<IWebHostEnvironment>();
                var controller = new UniversityEmployeesController(dbContext, mock.Object);

                var result = await controller.Delete(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<UniversityEmployee>(viewResult.Model);
                Assert.NotNull(model);
            }
        }
    }
}