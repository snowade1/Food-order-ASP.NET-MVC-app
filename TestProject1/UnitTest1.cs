using Snowflake.Controllers;
using Snowflake.Data;
using Snowflake.Models;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TestProject1
{
    [TestClass]
    public class CategoryControllerTests
    {
        public ApplicationDbContext context;
        private static DbContextOptions<ApplicationDbContext> dbContexOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;


        [TestInitialize]
        public void Setup()
        {
            context = new ApplicationDbContext(dbContexOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var category = new Category { Name = "Test", DisplayOrder = 10 };
            context.Category.AddRange(category);

            var product = new Product
            {
                Name = "ProductTest",
                Category = category,
                CategoryId = 1,
                Description = "Description test",
                Id = 12,
                Image = "",
                Price = 12.0,
                ShortDescription = "Short desc"
            };
            context.Product.AddRange(product);

            var applicationUser = new ApplicationUser { FullName = "Shaba Test" };
            context.ApplicationUser.Add(applicationUser);
            context.SaveChanges();
        }


        [TestCleanup]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void Index_DataIsNotNullTest()
        {
            var controller = new CategoryController(context);
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData);
        }

        [TestMethod]
        public void CreateViewTest()
        {
            var controller = new CategoryController(context);
            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewName, "Create");
        }

        [TestMethod]
        public void DeleteDataTest()
        {
            var controller = new CategoryController(context);
            var result = controller.Delete(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData);
        }


    }
    [TestClass]
    public class HomeControllerTests
    {
        public ApplicationDbContext context;
        private static DbContextOptions<ApplicationDbContext> dbContexOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;


        [TestInitialize]
        public void Setup()
        {
            context = new ApplicationDbContext(dbContexOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var category = new Category { Name = "Test", DisplayOrder = 10 };
            context.Category.AddRange(category);

            var product = new Product
            {
                Name = "ProductTest",
                Category = category,
                CategoryId = 1,
                Description = "Description test",
                Id = 12,
                Image = "",
                Price = 12.0,
                ShortDescription = "Short desc"
            };
            context.Product.AddRange(product);

            var applicationUser = new ApplicationUser { FullName = "Shaba Test" };
            context.ApplicationUser.Add(applicationUser);
            context.SaveChanges();
        }


        [TestCleanup]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void Index_DataIsNotNullTest()
        {

            var mock = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> logger = mock.Object;

            var controller = new HomeController(logger, context);
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData);
        }
    }
}