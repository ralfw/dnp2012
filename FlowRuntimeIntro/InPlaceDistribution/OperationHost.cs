using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InPlaceDistribution.contract;
using InPlaceDistribution.translators;
using npantarhei.runtime.contract;

namespace InPlaceDistribution
{
    public class OperationHost : IDisposable
    {
        private readonly IHostStub _hostStub;
        private readonly IStandInProxy _standInProxy;
        private readonly HostTranslator _translator;

        public OperationHost(IFlowRuntime runtime, IHostStub hostStub, IStandInProxy standInProxy)
        {
            _hostStub = hostStub;
            _standInProxy = standInProxy;

            _translator = new HostTranslator();

            hostStub.ReceivedFromStandIn += _translator.Process_remote_input;
            _translator.Translated_input += runtime.Process;
            runtime.Result += _translator.Process_local_output;
            _translator.Translated_output += standInProxy.SendToStandIn;
        }

        public void Dispose()
        {
            _hostStub.Dispose();
            _standInProxy.Dispose();
        }
    }
}
