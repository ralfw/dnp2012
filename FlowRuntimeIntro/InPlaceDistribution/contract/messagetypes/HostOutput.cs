using System;
using System.Runtime.Serialization;

namespace InPlaceDistribution.contract.messagetypes
{
    [DataContract]
    public class HostOutput
    {
        [DataMember]
        public string Portname;
        [DataMember]
        public string Data;
        [DataMember]
        public Guid CorrelationId;
    }
}