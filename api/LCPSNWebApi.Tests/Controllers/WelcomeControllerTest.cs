using LCPSNWebApi.Library.Resources;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace LCPSNWebApi.Tests.Controllers
{
    public class WelcomeControllerTest
    {
        [Fact]
        public void GetWelcome_ReturnsOkResult()
        {
            // Arrange
            var mockLocalizer = new Mock<IStringLocalizer<MyResources>>();
            string key = "Welcome to LCPSNWebApi!";
            var localizedString = new LocalizedString(key, key);
            mockLocalizer.Setup(_ => _[key]).Returns(localizedString);

            var controller = new WelcomeController(mockLocalizer.Object);

            // Act
            var result = controller.GetWelcome();

            // Assert
            Assert.Equal(key, result.Msg);
        }
    }

    public class WelcomeController
    {
        private readonly IStringLocalizer<MyResources> _localizer;

        public WelcomeController(IStringLocalizer<MyResources> localizer)
        {
            _localizer = localizer;
        }

        public WelcomeResponse GetWelcome()
        {
            var key = "Welcome to LCPSNWebApi!";
            var res = new WelcomeResponse
            {
                Msg = _localizer[key]
            };
            return res;
        }
    }

    public class WelcomeResponse
    {
        public string? Msg { get; set; }
    }
}
