using AngleSharp;
using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AngleSharpExample.Example
{
    class Html5Test : ExampleBase
    {
        public override Type ExampleType => GetType();

        public async override Task Run()
        {
            // We require a custom configuration with JavaScript, CSS and the default loader
            var config = Configuration.Default
                                      .WithJs()
                                      .WithCss()
                                      .WithDefaultLoader();

            // We create a new context
            var context = BrowsingContext.New(config);

            // The address we want to use
            var address = Url.Create("http://html5test.com");

            // Load the document
            var document = await context.OpenAsync(address);

            try
            {
                // Get the scored points
                var points = document.QuerySelector("#score > .pointsPanel > h2 > strong").TextContent;

                // Print it out
                Console.WriteLine("AngleSharp received {0} points form HTML5Test.com", points);
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
    }
}
