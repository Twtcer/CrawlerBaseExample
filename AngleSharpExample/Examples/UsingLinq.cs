using AngleSharp;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Common;

namespace AngleSharpExample.Example
{
    public class UsingLinq : ExampleBase
    { 
        public override Type ExampleType => this.GetType();

        public override async Task Run()
        {
            base.Start();
            var document = await BrowsingContext.New().OpenAsync(a => a.Content("<ul><li>First item<li>Second item<li class='blue'>Third item!<li class='blue red'>Last item!</ul>"));

            //css selector
            var blueCssList = document.QuerySelectorAll("li.blue");

            //linq selector
            var blueLinqList = document.All.Where(a => a.LocalName == "li" && a.ClassList.Contains("blue"));

            Logger.Trace("Comparing both ways ...");

            Logger.Trace(" Css Selector :"); 
            foreach (var item in blueCssList)
                Logger.Trace(item.Text());

            Logger.Trace(" Linq Selector :");
            foreach (var item in blueLinqList)
                Logger.Trace(item.TextContent);

            base.Stop();
        }
    }
}
