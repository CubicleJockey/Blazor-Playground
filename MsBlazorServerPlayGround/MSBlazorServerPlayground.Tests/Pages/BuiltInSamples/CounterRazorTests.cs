using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsBlazorServerPlayGround.Pages.BuiltInSamples;
using TestContext = Bunit.TestContext;

namespace MSBlazorServerPlayground.Tests.Pages.BuiltInSamples
{
    [TestClass]
    public class CounterRazorTests
    {
        [TestMethod]
        [Description("Testing that the button click increments the counter value.")]
        public void CounterShouldIncrementWhenSelected()
        {
            //Arrange
            using var testContext = new TestContext();
            var counterComponent = testContext.RenderComponent<Counter>();
            var paragraphElement = counterComponent.Find("p");
            var button = counterComponent.Find("button");

            //Act & Assert

            var text = paragraphElement.TextContent;

            text.MarkupMatches("Current count: 0");

            for (var i = 0; i < 10; i++)
            {
                button.Click();
                text = paragraphElement.TextContent;
                text.MarkupMatches($"Current count: {i + 1}");
            }
        }
    }
}
