using System;
using System.Runtime.Serialization;

namespace InPlaceDistribution.contract.messagetypes
{
    [Serializable]
    public class HostOutput
    {
        public string Portname;
        public byte[] Data;
        public Guid CorrelationId;
    }
}