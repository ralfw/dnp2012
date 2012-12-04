using System;
using InPlaceDistribution.contract.messagetypes;

namespace InPlaceDistribution.contract
{
    public interface IHostProxy : IDisposable
    {
        void SendToHost(HostInput input);
    }
}