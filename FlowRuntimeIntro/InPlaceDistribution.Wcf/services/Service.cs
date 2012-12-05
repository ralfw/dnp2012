using System;
using System.ServiceModel;
using InPlaceDistribution.Wcf.contract;

namespace InPlaceDistribution.Wcf.services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class Service<T> : IService<T>
    {
        private readonly Action<T> _continueWith;

        public Service(Action<T> continueWith) { _continueWith = continueWith; }

        public void Process(T input)
        {
            _continueWith(input);
        }
    }
}