using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Newsletter.Api.Controllers;
using Sadra.Newsletter.Application.DTOs;
using Sadra.Newsletter.Application.Services;
using Xunit.Abstractions;

namespace Newsletter.Api.Tests
{
    public class NewsletterControllerTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public NewsletterControllerTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public void AliveTest()
        {
            _testOutputHelper.WriteLine($".Net Core Web API 1");
        }

        [Fact]
        public async Task Post_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<INewsletterService>();
            var controller = new NewsletterController(mockService.Object);

            var datatype = "json";
            var dto = new NewsLetterDTO {Content="content",Title="title"};

            // Act
            var result = await controller.Post(dto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task Get_ById_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<INewsletterService>();
            var controller = new NewsletterController(mockService.Object);

            var id = 1; // Specify a valid IDll
            // Act
            var result = await controller.Get(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
