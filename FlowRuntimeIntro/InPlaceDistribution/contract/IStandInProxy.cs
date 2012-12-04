using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InPlaceDistribution.contract.messagetypes;

namespace InPlaceDistribution.contract
{
    public interface IStandInProxy : IDisposable
    {
        void SendToStandIn(Tuple<string, HostOutput> output);
    }
}
