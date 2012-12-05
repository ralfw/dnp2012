using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using InPlaceDistribution.Wcf.contract;
using InPlaceDistribution.contract.messagetypes;

namespace InPlaceDistribution.Wcf.services
{
    class ChannelDispenser : IDisposable
    {
        readonly Dictionary<string, IService<HostOutput>> _cache = new Dictionary<string, IService<HostOutput>>();
 
        public IService<HostOutput> Get(string standInEndpointAddress)
        {
            IService<HostOutput> standIn;
            if (!_cache.TryGetValue(standInEndpointAddress, out standIn))
            {
                var cf = new ChannelFactory<IService<HostOutput>>(new NetTcpBinding(), "net.tcp://" + standInEndpointAddress);
                standIn = cf.CreateChannel();
                _cache.Add(standInEndpointAddress, standIn);
            }
            return standIn;
        } 

        public void Dispose()
        {
            foreach(var standIn in _cache.Select(_ => _.Value))
                (standIn as ICommunicationObject).Close();
        }
    }
}