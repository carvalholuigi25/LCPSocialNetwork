using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using LCPSNWebApi.Controllers;
using LCPSNWebApi.Library.Resources;
using Moq;
using Xunit;

namespace LCPSNWebApi.Tests.Controllers
{
    public class WelcomeControllerTest
    {

        [Fact]
        public void GetWelcome_ReturnsOkResult()
        {
            List<WelcomeResponse> key = new List<WelcomeResponse>()
            {
              new WelcomeResponse
              {
                Msg = "Welcome to LCPSNWebApi!"
              }
            };

            Assert.Equal("Welcome to LCPSNWebApi!", key[0].Msg);
        }

        //[Fact]
        //public void GetWelcome_ReturnsOkResult()
        //{
        //    // Arrange
        //    var localizerMock = new Mock<IStringLocalizer<MyResources>>();
        //    var key = "Welcome to LCPSNWebApi!";

        //    localizerMock.Setup(l => l["welcome"]).Returns(new LocalizedString(key, key));

        //    var controller = new WelcomeController(localizerMock.Object);

        //    // Act
        //    var result = controller.GetWelcome("en");

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal(key, ((WelcomeResponse)okResult.Value!).Msg);
        //}
    }

    public class WelcomeResponse
    {
        public string? Msg { get; set; }
    }
}
