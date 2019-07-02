using AngleSharp.Js;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngleSharpExample.Example
{
    sealed class StandardConsoleLogger : IConsoleLogger
    {
        public void Log(Object[] values)
        {
            var elements = values.Select(m => (m ?? String.Empty).ToString());
            var content = String.Join(", ", elements);
            Console.WriteLine(content);
        }
    }
}
