using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using InPlaceDistribution;
using InPlaceDistribution.Wcf;
using npantarhei.runtime;
using npantarhei.runtime.config;

namespace GeneratorServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generator service...");

            var transceiver = new WcfHostTransceiver("localhost:8000");

            var config = new FlowRuntimeConfiguration()
                                .AddOperations(new AssemblyCrawler(Assembly.GetExecutingAssembly()))
                                .AddPushCausality("pushc")
                                .AddPopCausality("popc")
                                .AddFunc<Exception, string>("ExToString", ex => string.Format("{0}({1})", ex.InnerException.GetType().Name, ex.InnerException.Message))
                                .AddStreamsFrom("GeneratorServer.root.flow", Assembly.GetExecutingAssembly());

            using(var fr = new FlowRuntime(config))
            using(new OperationHost(fr, transceiver, transceiver))
            {
                Console.WriteLine("[running]");

                //fr.Message += Console.WriteLine;
                //fr.UnhandledException += Console.WriteLine;

                Console.ReadLine();
            }
        }
    }
}
