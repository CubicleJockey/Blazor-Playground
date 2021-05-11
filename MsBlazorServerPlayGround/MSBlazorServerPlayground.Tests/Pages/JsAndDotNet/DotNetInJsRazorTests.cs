using System;
using System.Threading.Tasks;
using AngleSharpWrappers;
using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsBlazorServerPlayGround.Pages.JsAndDotNet;
using TestContext = Bunit.TestContext;

namespace MSBlazorServerPlayground.Tests.Pages.JsAndDotNet
{
    [TestClass]
    public class DotNetInJsRazorTests
    {
        [TestMethod, Ignore]
        public async Task JsInvoke()
        {
            //Arrange
            using var testContext = new TestContext();
            var component = testContext.RenderComponent<DotNetInJs>();

            var button = (HtmlButtonElementWrapper)component.Find("#js-invoke-button");
            var paragraph = component.Find("#integers");


            //Act & Assert

            var text = paragraph.TextContent;
            text.MarkupMatches(string.Empty);

            button.DoClick();
            //await button.TriggerEventAsync("click", EventArgs.Empty);

            text = paragraph.TextContent;
            text.Should().NotBeNullOrWhiteSpace();

            var numbers = text.Split(',');
            foreach (var number in numbers)
            {
                var isNumber = int.TryParse(number,out _);
                if(!isNumber) { Assert.Fail("Numbers must be returned."); }
            }


        }
    }
}
