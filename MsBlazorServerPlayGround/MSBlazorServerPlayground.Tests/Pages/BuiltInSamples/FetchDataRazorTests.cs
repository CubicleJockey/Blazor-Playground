using System;
using System.Threading.Tasks;
using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsBlazorServerPlayGround.Data;
using MsBlazorServerPlayGround.Pages.BuiltInSamples;
using TestContext = Bunit.TestContext;

namespace MSBlazorServerPlayground.Tests.Pages.BuiltInSamples
{
    [TestClass]
    public class FetchDataRazorTests
    {
        [TestMethod]
        [Description("Dependency Injector returns null service")]
        public void WeatherForecastServiceIsNull()
        {
            //Arrange
            var service = A.Dummy<IWeatherForecastService>();
            WeatherForecast[] values = null;
            A.CallTo(() => service.GetForecastAsync(A<DateTime>.Ignored)).ReturnsLazily(() => values);

            using var testContext = new TestContext();
            testContext.Services.AddSingleton(service); 

            //Act
            var component = testContext.RenderComponent<FetchData>();

            //Assert
            var paragraph = component.Find("#loading-forecast em");
            paragraph.Should().NotBeNull();

            var text = paragraph.TextContent;
            text.Should().NotBeNullOrWhiteSpace();
            text.MarkupMatches("Loading...");
        }
    }
}
