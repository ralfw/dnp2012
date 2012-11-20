using System.ServiceModel;
using npantarhei.runtime.contract;

namespace WcfOperations
{
    [ServiceContract]
    public interface IWcfOperationReplacement
    {
        [OperationContract]
        void Process(Response resp);
    }
}