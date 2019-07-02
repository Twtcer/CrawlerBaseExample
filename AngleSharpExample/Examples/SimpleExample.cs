using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using Common;
using NLog;

namespace AngleSharpExample.Example
{
  public  class SimpleExample : ExampleBase
    {  
        public override Type ExampleType => GetType();

        public async override Task Run()
        {
            // Create a new document from the given source
            var document = await BrowsingContext.New().OpenAsync(m => m.Content("<h1>Some example source</h1><p>This is a paragraph element"));

            // Do something with document like the following
            Console.WriteLine("Serializing the (original) document:");
            Console.WriteLine(document.DocumentElement.OuterHtml);

            var p = document.CreateElement("p");
            p.TextContent = "This is another paragraph.";

            Console.WriteLine("Inserting another element in the body ...");
            document.Body.AppendChild(p);

            Console.WriteLine("Serializing the document again:");
            Console.WriteLine(document.DocumentElement.OuterHtml);
        }
    }
}
