using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InPlaceDistribution.contract;
using InPlaceDistribution.contract.messagetypes;
using InPlaceDistribution.translators;
using NUnit.Framework;
using npantarhei.runtime.contract;
using npantarhei.runtime.messagetypes;
using npantarhei.runtime.patterns;

namespace InPlaceDistribution
{
    [ActiveOperation]
    public class StandInOperation : AOperation, IDisposable
    {
        private Action<IMessage> _continueWith;
        private readonly StandInTranslator _translator;

        private readonly IHostProxy _hostProxy;
        private readonly IStandInStub _standInStub;

        public StandInOperation(string name, IHostProxy hostProxy, IStandInStub standInStub) : base(name)
        {
            _hostProxy = hostProxy;
            _standInStub = standInStub;

            _translator = new StandInTranslator(standInStub.StandInEndpointAddress);
            _translator.Translated_output += _hostProxy.SendToHost;

            _standInStub.ReceivedFromHost += _translator.Process_remote_input;
            _translator.Translated_input += _ => _continueWith(_);
        }

        protected override void Process(IMessage input, Action<IMessage> continueWith, Action<FlowRuntimeException> unhandledException)
        {
            if (input is ActivationMessage)
                _continueWith = continueWith;
            else
                _translator.Process_local_output(input);
        }

        public void Dispose()
        {
            _hostProxy.Dispose();
            _standInStub.Dispose();
        }
    }
}
