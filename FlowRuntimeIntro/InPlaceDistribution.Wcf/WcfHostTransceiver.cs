using System;
using System.ServiceModel;
using System.Text;
using InPlaceDistribution.Wcf.contract;
using InPlaceDistribution.Wcf.services;
using InPlaceDistribution.contract;
using InPlaceDistribution.contract.messagetypes;

namespace InPlaceDistribution.Wcf
{
    public class WcfHostTransceiver : IHostStub, IStandInProxy
    {
        private readonly string _endpointAddress;
        private readonly ServiceHost _hostService;
        private readonly ChannelDispenser _channels = new ChannelDispenser();


        public WcfHostTransceiver(string endpointAddress)
        {
            _endpointAddress = endpointAddress;

            _hostService = new SingletonServiceHost(new Service<HostInput>(_ => ReceivedFromStandIn(_)));
            _hostService.AddServiceEndpoint(typeof(IService<HostInput>), new NetTcpBinding(), "net.tcp://" + endpointAddress);
            _hostService.Open();
        }


        #region IHostStub
        public event Action<HostInput> ReceivedFromStandIn;

        public string HostEndpointAddress
        {
            get { return _endpointAddress; }
        }
        #endregion


        #region IStandProxy
        public void SendToStandIn(Tuple<string, HostOutput> output)
        {
            _channels.Get(output.Item1).Process(output.Item2);
        }
        #endregion


        public void Dispose()
        {
            _hostService.Close();
        }
    }
}
