using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace MsBlazorServerPlayGround.Objects
{
    public class JsInAClass
    {
        private static readonly Random random = new();

        private readonly IJSRuntime javaScript;

        public JsInAClass(IJSRuntime javaScript)
        {
            this.javaScript = javaScript ?? throw new ArgumentNullException(nameof(javaScript));
        }

        public ValueTask<string> GetRandomNumberList()
        {
            var max = random.Next(0, 50);
            IList<int> numbers = new List<int>(max);
            for(var i = 0; i < max; i++)
            {
                numbers.Add(random.Next(0, max));
            }

            var message = javaScript.InvokeAsync<string>("CallMeInAClass", numbers);
            return message;
        }
    }
}
