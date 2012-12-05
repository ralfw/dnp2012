using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using InPlaceDistribution.Wcf.contract;
using InPlaceDistribution.contract.messagetypes;

namespace InPlaceDistribution.Wcf.services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class HostService : IHostService
    {
        private readonly Action<HostInput> _continueWith;

        public HostService(Action<HostInput> continueWith) { _continueWith = continueWith; }

        public void Process(HostInput input)
        {
            throw new NotImplementedException();
        }
    }
}
