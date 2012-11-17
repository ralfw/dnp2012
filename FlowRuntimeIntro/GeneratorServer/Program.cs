using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace GeneratorServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var svh = new ServiceHost(typeof(GeneratorStub));
            svh.AddServiceEndpoint(typeof (IGeneratorStub), new NetTcpBinding(), "net.tcp://localhost:8000");
            svh.Open();

            Console.Write("Generator Server running - until ENTER is pressed");
            Console.ReadLine();

            svh.Close();
        }
    }
}
