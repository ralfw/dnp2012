using System;
using System.Runtime.Serialization;

namespace InPlaceDistribution.contract.messagetypes
{
    [Serializable]
    public class HostInput
    {
        public string Portname;
        public string Data;
        public Guid CorrelationId;
        public string StandInEndpointAddress;
    }
}