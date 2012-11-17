using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using GeneratorServer;

namespace GeneratorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var cf = new ChannelFactory<IGeneratorStub>(new NetTcpBinding(), "net.tcp://localhost:8000");
            var generatorProxy = cf.CreateChannel();

            while(true)
            {
                Console.Write("req: "); var req = Console.ReadLine();
                if (req == "") break;

                var response = generatorProxy.Generate(req);

                response.ToList().ForEach(Console.WriteLine);
            }

            (generatorProxy as ICommunicationObject).Close();
        }
    }
}
