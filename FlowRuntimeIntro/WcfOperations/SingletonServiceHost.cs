using System;
using System.ServiceModel;

namespace WcfOperations
{
    class SingletonServiceHost : ServiceHost
    {
        public SingletonServiceHost(object singleton, params Uri[] baseAddresses) : base(singleton, baseAddresses) {}
    }
}