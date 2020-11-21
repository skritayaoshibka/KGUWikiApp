using System;
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
    public class AccountControllerTests
    {
        [Fact]
        public void Login_ReturnsViewResultNotNull()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new AccountController(dbContext);

                var result = controller.Login();

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
            }
        }

        [Fact]
        public async Task Login_ReturnsViewResultWithNotNullWithLoginViewModel()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new AccountController(dbContext);

                var result = await controller.Login(new LoginViewModel{ Email = "1", Password = "1"});

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<LoginViewModel>(viewResult.Model);
                Assert.NotNull(model);
            }
        }

        [Fact]
        public void Register_ReturnsViewResultNotNull()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new AccountController(dbContext);

                var result = controller.Register();

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
            }
        }

        [Fact]
        public async Task Register_ReturnsViewResultWithNotNullWithRegisterViewModel()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Roles.Add(new Role {Name = "Default User"});
                dbContext.Users.Add(new User {
                    Name = "1",
                    Email = "1",
                    Password = "1"});
                dbContext.SaveChanges();
                var controller = new AccountController(dbContext);

                var result = await controller.Register(new RegisterViewModel{ Name = "1", Email = "1", Password = "1", ConfirmPassword = "1"});

                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.NotNull(viewResult);
                var model = Assert.IsAssignableFrom<RegisterViewModel>(viewResult.Model);
                Assert.NotNull(model);
            }
        }
    }
}