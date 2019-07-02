using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using Common;
using NLog;

namespace AngleSharpExample.Example
{
    class RSSFeed : ExampleBase
    { 
        public override Type ExampleType => GetType();

        public async override Task Run()
        {
            // We create a new configuration with the default loader
            var config = Configuration.Default.WithDefaultLoader();

            // We create a new context
            var context = BrowsingContext.New(config);

            // The address we want to use
            var address = Url.Create("https://rsshub.app/ncm/playlist/152306943");

            // We load the feed
            var feed = await context.OpenAsync(address);

            // We query the desired items
            var items = feed.QuerySelectorAll("item").Select(m => new
            {
                Link = m.QuerySelector("guid").TextContent,
                Title = m.QuerySelector("title").TextContent
            });

            Console.WriteLine("Available titles:");

            foreach (var item in items)
                Console.WriteLine("- {0} ({1})", item.Title, item.Link);
        }
    }
}
