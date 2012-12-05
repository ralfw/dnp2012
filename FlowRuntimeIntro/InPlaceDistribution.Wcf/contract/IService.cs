using System.ServiceModel;

namespace InPlaceDistribution.Wcf.contract
{
    [ServiceContract]
    public interface IService<in T>
    {
        [OperationContract]
        void Process(T input);
    }
}