using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KGUWiki.Controllers;
using KGUWiki.Models;
using KGUWiki.Models.Contexts;
using KGUWiki.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Unit.Tests
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewResultWithListOfUsers()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new UsersController(dbContext);

                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<List<User>>(viewResult.Model);
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
                var controller = new UsersController(dbContext);

                var result = await controller.Details(-1);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }

        [Fact]
        public async Task Details_ReturnsViewResultWithUser()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Roles.Add(new Role 
                {
                    Name = ""
                });
                dbContext.SaveChanges();
                dbContext.Users.Add(new User 
                {
                    Name = "",
                    Email = "",
                    Password = "",
                    RoleId = 1
                });
                dbContext.SaveChanges();
                var controller = new UsersController(dbContext);

                var result = await controller.Details(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<User>(viewResult.Model);
                Assert.NotNull(model);
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
                var controller = new UsersController(dbContext);

                var result = await controller.Edit(-1);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notFoundResult);
            }
        }

        [Fact]
        public async Task Edit_ReturnsViewResultWithUser()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Roles.Add(new Role 
                {
                    Name = ""
                });
                dbContext.SaveChanges();
                dbContext.Users.Add(new User 
                {
                    Name = "",
                    Email = "",
                    Password = "",
                    RoleId = 1
                });
                dbContext.SaveChanges();
                var controller = new UsersController(dbContext);

                var result = await controller.Edit(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<User>(viewResult.Model);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public async Task Edit_ReturnsVewResultWithUser()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new UsersController(dbContext);

                var result = await controller.Edit(-1, new User {Name = "", Email = "", Password = ""});

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<User>(viewResult.Model);
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
                var controller = new UsersController(dbContext);

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
                dbContext.Roles.Add(new Role 
                {
                    Name = ""
                });
                dbContext.SaveChanges();
                dbContext.Users.Add(new User 
                {
                    Name = "",
                    Email = "",
                    Password = "",
                    RoleId = 1
                });
                dbContext.SaveChanges();
                var controller = new UsersController(dbContext);

                var result = await controller.Delete(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<User>(viewResult.Model);
                Assert.NotNull(model);
            }
        }
    }
}