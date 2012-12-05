using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using npantarhei.runtime.contract;

namespace InPlaceDistribution.Wcf
{
    public class WcfOperationHost : IDisposable
    {
        private readonly OperationHost _operationHost;

        public WcfOperationHost(IFlowRuntime runtime, string endpointAddress)
        {
            var transceiver = new WcfHostTransceiver(endpointAddress);
            _operationHost = new OperationHost(runtime, transceiver, transceiver);
        }

        public void Dispose()
        {
            _operationHost.Dispose();
        }
    }

}
