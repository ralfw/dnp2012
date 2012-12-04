using System;
using System.Runtime.Serialization;

namespace InPlaceDistribution.contract.messagetypes
{
    [DataContract]
    public class HostInput
    {
        [DataMember]
        public string Portname;
        [DataMember]
        public string Data;
        [DataMember]
        public Guid CorrelationId;
        [DataMember]
        public string StandInEndpointAddress;
    }
}