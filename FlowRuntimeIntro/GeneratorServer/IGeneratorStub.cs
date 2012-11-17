using System.ServiceModel;

namespace GeneratorServer
{
    [ServiceContract]
    public interface IGeneratorStub
    {
        [OperationContract]
        string[] Generate(string request);
    }
}