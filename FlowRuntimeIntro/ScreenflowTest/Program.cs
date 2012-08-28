using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using npantarhei.runtime;

namespace ScreenflowTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new FlowRuntimeConfiguration();
            config.AddStreamsFrom("ScreenflowTest.flows.flow", Assembly.GetExecutingAssembly());

            config.AddFunc<string, string>("toUpper", s => s.ToUpper())
                  .AddFunc<string, string>("reverse", s => new string(s.ToCharArray().Reverse().ToArray()));

            using(var fr = new FlowRuntime(config))
            {
                fr.Process(".in", "hello");

                fr.WaitForResult(_ => Console.WriteLine(_.Data));
            }
        }
    }
}
