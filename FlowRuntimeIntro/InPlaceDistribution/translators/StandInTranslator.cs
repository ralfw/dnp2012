﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InPlaceDistribution.contract.messagetypes;
using npantarhei.runtime.contract;
using npantarhei.runtime.messagetypes;

namespace InPlaceDistribution.translators
{
    class StandInTranslator
    {
        class FlowContext
        {
            public string StandInOperationName;
            public Guid CorrelationId;
            public int Priority;
            public CausalityStack Causalities;
            public FlowStack FlowStack;
        }

        private readonly CorrelationCache<FlowContext> _cache = new CorrelationCache<FlowContext>();
        private readonly string _standInEndpointAddress;

        public StandInTranslator(string standInEndpointAddress)
        {
            _standInEndpointAddress = standInEndpointAddress;
        }


        public void Process_local_output(IMessage outputMsg)
        {
            var corrId = Guid.NewGuid();

            var ctx = new FlowContext {StandInOperationName = outputMsg.Port.OperationName, CorrelationId = outputMsg.CorrelationId, Priority = outputMsg.Priority, Causalities = outputMsg.Causalities, FlowStack = outputMsg.FlowStack};
            _cache.Add(corrId, ctx);
            var input = new HostInput {Portname = outputMsg.Port.OutputPortToRemotePortname(), Data = outputMsg.Data.Serialize(), CorrelationId = corrId, StandInEndpointAddress = _standInEndpointAddress};
            Translated_output(input);
        }

        public event Action<HostInput> Translated_output;


        public void Process_remote_input(HostOutput output)
        {
            var ctx = _cache.Get(output.CorrelationId);

            var port = output.Portname.RemotePortnameToInputPort(ctx.StandInOperationName);
            var inputMsg = new Message(port, output.Data.Deserialize(), ctx.CorrelationId)
                                      {Priority = ctx.Priority, 
                                       Causalities = ctx.Causalities, 
                                       FlowStack = ctx.FlowStack};
            Translated_input(inputMsg);
        }

        public event Action<IMessage> Translated_input;
    }
}
