using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsBlazorServerPlayGround.Data;
using MsBlazorServerPlayGround.Pages.BuiltInSamples;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
            var NOW = DateTime.Now;
            var values = new []{ new WeatherForecast
            {
                Date = NOW,
                Summary = "A Summary",
                TemperatureC = 100,
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


            var rows = component.FindAll("table tbody tr");
            rows.Count.Should().Be(1);

            var row = rows.Single();


            var expected = new StringBuilder();
            expected.AppendLine("<tr>");
            expected.AppendLine($"<td>{NOW.ToShortDateString()}</td>");
            expected.AppendLine("<td>100</td>");
            expected.AppendLine("<td>212</td>");
            expected.AppendLine("<td>A Summary</td>");
            expected.AppendLine("</tr>");

            row.MarkupMatches(expected.ToString());

            A.CallTo(serviceExpression).MustHaveHappenedOnceExactly();
        }
    }
}
