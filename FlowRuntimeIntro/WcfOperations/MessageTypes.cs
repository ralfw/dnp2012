using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WcfOperations
{
    [DataContract]
    public class Request
    {
        [DataMember]
        public string Portname { get; set; }
        [DataMember]
        public string Data { get; set; }
        [DataMember]
        public Guid CorrelationId { get; set; }
        [DataMember]
        public string OriginEndpointAddress { get; set; }
    }


    [DataContract]
    public class Response
    {
        [DataMember]
        public string Data { get; set; }
        [DataMember]
        public Guid CorrelationId { get; set; }
    }
}
