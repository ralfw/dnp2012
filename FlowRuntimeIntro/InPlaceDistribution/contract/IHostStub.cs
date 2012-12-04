using System;
using InPlaceDistribution.contract.messagetypes;

namespace InPlaceDistribution.contract
{
    public interface IHostStub : IDisposable
    {
        event Action<HostInput> ReceivedFromStandIn;
        string HostEndpointAddress { get; }
    }
}