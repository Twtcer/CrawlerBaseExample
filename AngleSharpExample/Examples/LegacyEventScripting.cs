﻿using AngleSharp;
using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AngleSharpExample.Example
{
    class LegacyEventScripting : ExampleBase
    {
        public override Type ExampleType => GetType();

        public async override Task Run()
        {
            // We require a custom configuration with JavaScript
            var config = Configuration.Default
                .WithJs()
                .WithConsoleLogger(context => new StandardConsoleLogger());

            // This is our sample source, we will trigger the load event
            var source = @"<!doctype html>
                                <html>
                                <head><title>Legacy event sample</title></head>
                                <body>
                                <script>
                                console.log('Before setting the handler via onload!');

                                document.onload = function() {
                                    console.log('Document loaded (legacy way)!');
                                };

                                console.log('After setting the handler via onload!');
                                </script>
                                </body>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source));

            // HTML should be output in the end
            Console.WriteLine(document.DocumentElement.OuterHtml);
        }
    }
}
