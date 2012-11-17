using System;
using System.Collections.Generic;
using System.Linq;
using npantarhei.runtime;
using npantarhei.runtime.contract;

namespace GeneratorServer
{
    public class GeneratorStub : IGeneratorStub
    {
        public string[] Generate(string request)
        {
            using(var fr = FlowRuntimeFactory.Beginner)
            {
                fr.Message += Console.WriteLine;

                var resultMsgs = new List<IMessage>();
                fr.Result += resultMsgs.Add;

                fr.Process(".in", request);

                return resultMsgs.Select(msg => (string) msg.Data).ToArray();
            }
        }
    }
}