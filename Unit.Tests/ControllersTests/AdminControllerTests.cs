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
    public class AdminControllerTests
    {
        // [Fact]
        // public void IndexReturnViewResult()
        // {
        //     var dbName = Guid.NewGuid().ToString();
        //     var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName)
        //         .Options;
        //     using (var dbContext = new ApplicationDbContext(options))
        //     {
        //         var controller = new AdminController(dbContext);

        //         var result = controller.Index();

        //         Assert.IsType<ViewResult>(result);
        //     }
        // }
    }
}