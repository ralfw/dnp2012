using System;
using System.ServiceModel;
using npantarhei.runtime.contract;
using npantarhei.runtime.patterns;

namespace WcfOperations
{
    [ActiveOperation]
    public class WcfOperationReplacement : AOperation, IDisposable
    {
        private readonly IWcfOperationWrapperStub _remoteOperationWrapperService;
        private ServiceHost _localOperationReplacementService;
        private readonly string _originEndpointAddress;
        private Action<IMessage> _continueWith;


        #region AOperation
        public WcfOperationReplacement(string name, string originEndpointAddress, string destinationEndpointAddress)
            : base(name)
        {
            //_originEndpointAddress = originEndpointAddress;

            //var cf = new ChannelFactory<IWcfOperationWrapperStub>(new NetTcpBinding(), "net.tcp://" + destinationEndpointAddress);
            //_remoteOperationWrapperService = cf.CreateChannel();

            //var svh = new ServiceHost(typeof(GeneratorStub));
            //svh.AddServiceEndpoint(typeof(IGeneratorStub), new NetTcpBinding(), "net.tcp://" + originEndpointAddress);
            //svh.Open();
        }

        protected override void Process(IMessage input, System.Action<IMessage> continueWith, System.Action<FlowRuntimeException> unhandledException)
        {
            if (input is ActivationMessage) { _continueWith = continueWith; return; }


        }
        #endregion


        #region IWcfOperationReplacement
        public void Process(Response resp)
        {
            throw new System.NotImplementedException();
        }
        #endregion


        public void Dispose()
        {
            (_remoteOperationWrapperService as ICommunicationObject).Close();
        }
    }
}