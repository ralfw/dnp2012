using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GeneratorServer;
using InPlaceDistribution;
using InPlaceDistribution.Wcf;
using npantarhei.runtime;
using npantarhei.runtime.config;

namespace GeneratorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generator client...");

            var portnumber = 8100 + DateTime.Now.Second;
            var config = new FlowRuntimeConfiguration()
                                .AddOperations(new AssemblyCrawler(Assembly.GetExecutingAssembly()))
                                .AddStreamsFrom("GeneratorClient.root.flow", Assembly.GetExecutingAssembly())
                                .AddOperation(new WcfStandInOperation("proxy", "localhost:"+portnumber, "localhost:8000"));

            using(var fr = new FlowRuntime(config))
            {
                Console.WriteLine("[running @ {0}]", portnumber);

                fr.Message += Console.WriteLine;
                fr.UnhandledException += Console.WriteLine;

                fr.Process(".run");

                fr.WaitForResult();
            }
        }
    }
}
