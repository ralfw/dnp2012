using System;
using System.ServiceModel;

namespace InPlaceDistribution.Wcf.services
{
    class SingletonServiceHost : ServiceHost
    {
        public SingletonServiceHost(object singleton, params Uri[] baseAddresses) : base(singleton, baseAddresses) {}
    }
}