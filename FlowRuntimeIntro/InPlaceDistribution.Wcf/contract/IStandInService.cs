using System.ServiceModel;
using InPlaceDistribution.contract.messagetypes;

namespace InPlaceDistribution.Wcf.contract
{
    [ServiceContract]
    public interface IStandInService
    {
        [OperationContract]
        void Process(HostOutput input);
    }
}