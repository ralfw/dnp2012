using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using InPlaceDistribution.contract.messagetypes;

namespace InPlaceDistribution.Wcf.contract
{
    [ServiceContract]
    public interface IHostService
    {
        [OperationContract]
        void Process(HostInput input);
    }
}
