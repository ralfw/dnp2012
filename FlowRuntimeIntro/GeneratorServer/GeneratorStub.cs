using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using npantarhei.runtime;
using npantarhei.runtime.contract;

namespace GeneratorServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class GeneratorStub : IGeneratorStub, IDisposable
    {
        private readonly IFlowRuntime _fr;
        private readonly SyncMuxDemux _mdux;


        public GeneratorStub()
        {
            _fr = FlowRuntimeFactory.Beginner;
            _fr.Message += Console.WriteLine;
        
            _mdux = new SyncMuxDemux();
            _mdux.Muxed += _ => _fr.Process(_);

            _fr.Result += _mdux.Demux;
        }


        public string[] Generate(string request)
        {
                var results = new List<IMessage>();

                _mdux.Process(".in", request, results.Add);

                return results.Select(msg => (string) msg.Data).ToArray();
        }


        public void Dispose()
        {
            _fr.Dispose();
        }
    }
}