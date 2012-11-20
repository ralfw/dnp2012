using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using npantarhei.runtime;
using npantarhei.runtime.contract;
using npantarhei.runtime.messagetypes;

namespace WcfOperations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WcfOperationWrapperStub : IWcfOperationWrapperStub
    {
        private readonly InputOutputCorrelator _corr;

        public WcfOperationWrapperStub(InputOutputCorrelator corr)
        {
            _corr = corr;
        }


        public void Process(Request request)
        {
            _corr.RegisterRequest(request);
            Received(new Message("." + request.Portname, request.Data, request.CorrelationId));
        }


        public event Action<IMessage> Received;
    }


    public class WcfOperationWrapperProxy
    {
        private readonly InputOutputCorrelator _corr;

        public WcfOperationWrapperProxy(InputOutputCorrelator corr)
        {
            _corr = corr;
        }


        public void Send(IMessage msg)
        {
            
        }
    }


    public class InputOutputCorrelator
    {
        private readonly ConcurrentDictionary<Guid, string> _originEndpoints = new ConcurrentDictionary<Guid, string>();

        public void RegisterRequest(Request req)
        {
            _originEndpoints.TryAdd(req.CorrelationId, req.OriginEndpointAddress);
        }
    }
}
