using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InPlaceDistribution.contract.messagetypes;
using npantarhei.runtime.contract;
using npantarhei.runtime.messagetypes;

namespace InPlaceDistribution.translators
{
    class HostTranslator
    {
        private readonly CorrelationCache<string> _cache = new CorrelationCache<string>();
 

        public void Process_remote_input(HostInput input)
        {
            _cache.Add(input.CorrelationId, input.StandInEndpointAddress);

            //TODO: deserialization of data
            var msg = new Message(input.Portname.StandInPortnameToInputPortname(), input.Data, input.CorrelationId);
            Translated_input(msg);
        }

        public event Action<IMessage> Translated_input;


        public void Process_local_output(IMessage message)
        {
            var standInEndpointAddress = _cache.Get(message.CorrelationId);
            //TODO: serialization of data
            var output = new HostOutput { Portname = message.Port.OutputPortToStandInPortname(), Data = message.Data.ToString(), CorrelationId = message.CorrelationId};
            Translated_output(new Tuple<string, HostOutput>(standInEndpointAddress, output));
        }

        public event Action<Tuple<string, HostOutput>> Translated_output;
    }
}
