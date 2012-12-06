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
        internal struct Channel
        {
            public IService<HostOutput> StandIn;
            public DateTime ExpiresAt;
        }

        readonly Dictionary<string, Channel> _cache = new Dictionary<string, Channel>();
        private int _gcCounter;
        private const int GC_FREQUENCY = 1000;


        public IService<HostOutput> Get(string standInEndpointAddress)
        {
            lock (_cache)
            {
                if (++_gcCounter % GC_FREQUENCY == 0) CollectGarbage();

                Channel ch;
                if (!_cache.TryGetValue(standInEndpointAddress, out ch))
                {
                    var cf = new ChannelFactory<IService<HostOutput>>(new NetTcpBinding(), "net.tcp://" + standInEndpointAddress);
                    ch = new Channel{StandIn = cf.CreateChannel(), ExpiresAt = DateTime.Now.AddSeconds(60)};
                    _cache.Add(standInEndpointAddress, ch);
                }
                return ch.StandIn;
            }
        } 


        internal void CollectGarbage()
        {
            var keysOfExprired = _cache.Select(_ => new {_.Key, _.Value.ExpiresAt})
                                       .Where(_ => _.ExpiresAt <= DateTime.Now)
                                       .Select(_ => _.Key)
                                       .ToArray();
            foreach (var key in keysOfExprired)
                _cache.Remove(key);
        }


        public void Dispose()
        {
            foreach(var standIn in _cache.Select(_ => _.Value.StandIn))
                (standIn as ICommunicationObject).Close();
        }
    }
}