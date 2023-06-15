using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Snowflake.Controllers;
using Snowflake.Models;

namespace IntegrationTests
{
    [TestClass]
    public class ApiTests
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var webAppFactory = new WebApplicationFactory<CategoryController>();
            var httpClient = webAppFactory.CreateDefaultClient();

            Category category = new Category() { DisplayOrder = 65, Name = "Test_Name" };
            var jsonBody = JsonConvert.SerializeObject(category);
            var httpContent = new StringContent(jsonBody);
            var response = await httpClient.PostAsync("Category/Create", httpContent);
            var stringResult = await response.Content.ReadAsStringAsync();


            Assert.IsNotNull(stringResult);
        }
    }
}