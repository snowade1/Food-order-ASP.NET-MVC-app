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
    public class CategoryControllerTests
    {
        private ApplicationDbContext _dbContext;
        private CategoryController _controller;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _controller = new CategoryController(_dbContext);
        }

        [TestMethod]
        public void Create_ValidCategory_RedirectsToIndex()
        {
            // Arrange
            var category = new Category { Name = "Test Category" };

            // Act
            var result = _controller.Create(category) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void Edit_ValidCategory_RedirectsToIndex()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Test Category" };

            // Act
            var result = _controller.Edit(category) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void DeletePost_ValidId_RedirectsToIndex()
        {
            // Arrange
            var category = new Category { Id = 2, Name = "Test Category" };
            _dbContext.Category.Add(category);
            _dbContext.SaveChanges();

            // Act
            var result = _controller.DeletePost(category.Id) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }
    }
}