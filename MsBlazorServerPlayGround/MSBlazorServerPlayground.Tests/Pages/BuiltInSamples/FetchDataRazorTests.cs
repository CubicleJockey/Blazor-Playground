using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsBlazorServerPlayGround.Data;
using MsBlazorServerPlayGround.Pages.BuiltInSamples;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestContext = Bunit.TestContext;

namespace MSBlazorServerPlayground.Tests.Pages.BuiltInSamples
{
    [TestClass]
    public class FetchDataRazorTests
    {
        private readonly IWeatherForecastService service;
        private readonly Expression<Func<Task<WeatherForecast[]>>> serviceExpression;

        public FetchDataRazorTests()
        {
            service = A.Dummy<IWeatherForecastService>();
            serviceExpression = () => service.GetForecastAsync(A<DateTime>.Ignored);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Fake.ClearRecordedCalls(service);
        }

        [TestMethod]
        [Description("Dependency Injector returns null service")]
        public void WeatherForecastServiceIsNull()
        {
            //Arrange
            WeatherForecast[] values = null;
            A.CallTo(serviceExpression).ReturnsLazily(() => values);

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

            A.CallTo(serviceExpression).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        [Description("Dependency Injector returns null service")]
        public void WeatherForecastServiceIsValid()
        {
            //Arrange
            var values = new WeatherForecast[]{ new WeatherForecast
            {
                Date = DateTime.Now,
                Summary = "A Summary",
                TemperatureC = 100
            } };

            A.CallTo(serviceExpression).ReturnsLazily(() => values);

            using var testContext = new TestContext();
            testContext.Services.AddSingleton(service);

            //Act
            var component = testContext.RenderComponent<FetchData>();

            //Assert
            
            var table = component.Find(".table");
            var innerTable = table.TextContent;
            innerTable.Should().NotBeNullOrWhiteSpace();

            A.CallTo(serviceExpression).MustHaveHappenedOnceExactly();
        }
    }
}
