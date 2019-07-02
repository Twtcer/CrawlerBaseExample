using AngleSharpExample.Example;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AngleSharpExample
{
    class Program
    {
        private readonly static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static IEnumerable<IExample> GetExamples()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var alltypes = assembly.GetTypes();
            var types = alltypes.Where(m => m.GetInterfaces().Contains(typeof(IExample)) && !m.IsAbstract);
            return types.Select(m => m.GetConstructor(Type.EmptyTypes).Invoke(null) as IExample);
        }
        static void Main(string[] args)
        {
            var exampes = GetExamples().Select(m => new
            {
                Name = m.GetType().Name,
                Run = (Func<Task>)m.Run
            }).ToList(); 
            var defaults = new
            {
                pause = false,
                clear = false,
                //selected = new[] { "UsingLinq" },
                selected = exampes.Select(m => m.Name).ToArray(),
            };
            var usepause = args.Contains("-p") || args.Contains("--pause") || defaults.pause;
            var clearscr = args.Contains("-c") || args.Contains("--clear") || defaults.clear;
            var pause = Switch(usepause, PauseConsole);
            var clear = Switch(clearscr, ClearConsole);
            RunSynchronously(async () =>
                {
                    foreach (var example in exampes)
                    {
                        if (defaults.selected.Contains(example.Name))
                        {
                            Console.WriteLine(">>> {0}", example.Name);
                            Console.WriteLine();

                            await example.Run();

                            Console.WriteLine();
                        }
                    }
                });
            PauseConsole();
        }

        static Action Switch(Boolean condition, Action active)
        {
            return condition ? active : () => { };
        }
        static void RunSynchronously(Func<Task> runner)
        {
            try
            {
                runner().Wait();
            }
            catch (Exception ex)
            {
                logger.Error($"run {runner.Method} has error :{ex.Message}");
            } 
        }

        static void ClearConsole()
        {
            Console.Clear();
        }

        static void PauseConsole()
        {
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey(true);
        }
    }
}
