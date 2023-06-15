using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snowflake.Controllers;
using Snowflake.Data;
using Snowflake.Models;
using System.Collections.Generic;
using System.Linq;

namespace Snowflake.Tests
{
    [TestClass]
    public class ProductTypeControllerTests
    {
        private TestDbContext _dbContext;
        private ProductTypeController _controller;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            _dbContext = new TestDbContext(options);
            _controller = new ProductTypeController(_dbContext);
        }

        [TestMethod]
        public void Create_ValidProductType_RedirectsToIndex()
        {
            // Arrange
            var productType = new ProductType { Name = "Test Product" };

            // Act
            var result = _controller.Create(productType) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void Edit_ValidProductType_RedirectsToIndex()
        {
            // Arrange
            var productType = new ProductType { Id = 1, Name = "Test Product" };

            // Act
            var result = _controller.Edit(productType) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void DeletePost_ValidId_RedirectsToIndex()
        {
            // Arrange
            var productType = new ProductType { Id = 1, Name = "Test Product" };
            _dbContext.ProductType.Add(productType);
            _dbContext.SaveChanges();

            // Act
            var result = _controller.DeletePost(productType.Id) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

    }

    // Фиктивная реализация ApplicationDbContext
    public class TestDbContext : ApplicationDbContext
    {
        public TestDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            // Вместо сохранения изменений, просто возвращаем количество измененных записей.
            return 0;
        }
    }
}