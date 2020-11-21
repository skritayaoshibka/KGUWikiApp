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
    public class RolesControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewResultWithListOfRoles()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new RolesController(dbContext);

                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<List<Role>>(viewResult.Model);
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
                var controller = new RolesController(dbContext);

                var result = await controller.Details(-1);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }

        [Fact]
        public async Task Details_ReturnsViewResultWithRole()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Roles.Add(new Role {Name = ""});
                dbContext.SaveChanges();
                var controller = new RolesController(dbContext);

                var result = await controller.Details(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<Role>(viewResult.Model);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new RolesController(dbContext);

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
                var controller = new RolesController(dbContext);

                var result = await controller.Create(new Role { Name = ""});

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
                var controller = new RolesController(dbContext);

                var result = await controller.Edit(-1);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }

        [Fact]
        public async Task Edit_ReturnsViewResultWithRole()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Roles.Add(new Role {Name = ""});
                dbContext.SaveChanges();
                var controller = new RolesController(dbContext);

                var result = await controller.Edit(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<Role>(viewResult.Model);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task Edit_ReturnsVewResultWithRole()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new RolesController(dbContext);

                var result = await controller.Edit(-1, new Role {Name = ""});

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<Role>(viewResult.Model);
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
                var controller = new RolesController(dbContext);

                var result = await controller.Delete(-1);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }

        [Fact]
        public async Task Delete_ReturnsViewResultWithRole()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Roles.Add(new Role {Name = ""});
                dbContext.SaveChanges();
                var controller = new RolesController(dbContext);

                var result = await controller.Delete(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<Role>(viewResult.Model);
                Assert.NotNull(model);
            }
        }
    }
}