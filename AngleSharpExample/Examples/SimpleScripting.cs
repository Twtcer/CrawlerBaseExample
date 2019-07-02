using AngleSharp;
using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AngleSharpExample.Example
{
    class SimpleScripting : ExampleBase
    {
        public override Type ExampleType => GetType();

        public async override Task Run()
        {
            // We require a custom configuration with JavaScript
            var config = Configuration.Default.WithJs();

            // This is our sample source, we will set the title and write on the document
            var source = @"<!doctype html>
                                <html>
                                <head><title>Sample</title></head>
                                <body>
                                <script>
                                document.title = 'Simple manipulation...';
                                document.write('<span class=greeting>Hello World!</span>');
                                </script>
                                </body>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source));

            // Modified HTML will be output
            Console.WriteLine(document.DocumentElement.OuterHtml);
        }
    }
}
