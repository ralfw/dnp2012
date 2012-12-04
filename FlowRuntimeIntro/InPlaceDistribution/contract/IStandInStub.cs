using System;
using InPlaceDistribution.contract.messagetypes;

namespace InPlaceDistribution.contract
{
    public interface IStandInStub : IDisposable
    {
        event Action<HostOutput> ReceivedFromHost;
        string StandInEndpointAddress { get; }
    }
}