using System.ServiceModel;

namespace WcfOperations
{
    [ServiceContract]
    public interface IWcfOperationWrapperStub
    {
        [OperationContract]
        void Process(Request request);
    }
}