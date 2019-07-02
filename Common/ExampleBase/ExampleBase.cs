using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class ExampleBase : IExample
    {
        public Logger Logger { get; set; }
        public abstract Type ExampleType { get; }
        //public Type ExampleType { get; set; }
        public abstract Task Run();

        public ExampleBase( )
        { 
            Logger = NLog.LogManager.GetLogger($"{ExampleType.FullName}");
        }

        public void Start()
        {
            Logger.Info($"Start Example:{ExampleType.Name}");
        }

        public void Stop()
        {
            Logger.Info($"Stop Example:{ExampleType.Name}");
        }
    }
}
