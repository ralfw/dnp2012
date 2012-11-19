using System;
using System.Collections.Concurrent;
using npantarhei.runtime.contract;
using npantarhei.runtime.messagetypes;

namespace GeneratorServer
{
    class SyncMuxDemux
    {
        private readonly ConcurrentDictionary<Guid, Action<IMessage>> _destinations = new ConcurrentDictionary<Guid, Action<IMessage>>();

        public void Process(string portname, object data, Action<IMessage> outputHandler)
        {
            var corrId = Guid.NewGuid();
            _destinations.AddOrUpdate(corrId, outputHandler, (guid, action) => null);

            Muxed(new Message(portname, data, corrId));

            _destinations.TryRemove(corrId, out outputHandler);
        }

        public event Action<IMessage> Muxed;


        public void Demux(IMessage msg)
        {
            Action<IMessage> outputHandler = null;
            _destinations.TryGetValue(msg.CorrelationId, out outputHandler);
            outputHandler(msg);
        }
    }
}