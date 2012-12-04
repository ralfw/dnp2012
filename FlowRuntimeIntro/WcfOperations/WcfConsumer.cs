using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using npantarhei.runtime.contract;

namespace WcfOperations
{
    [ServiceContract]
    public interface IWcfPeer : IDisposable
    {
        // Rolle Consumer: Input annehmen, Rückrufadr registrieren, IMessage schnüren und Received() feuern
        // Rolle Producer: Output annehmen, Input dazu finden und Kontext wieder herstellen, IMessage schnüren und Received() feuern
        [OperationContract]
        void Receive(string portname, string data, Guid correlationId, string producerEndpointAddress);

        // Rolle Consumer: Input in remote Flow abgeben
        // Rolle Producer: Output von remote Flow zurückgeben
        event Action<IMessage> Received;

        // Rolle Consumer: Output annehmen, Rückruf Proxy bauen, Output an Producer zurückschicken
        // Rolle Producer: Input annehmen, Kontext registrieren, Consumer aufrufen
        void Send(IMessage message);
    }


    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public abstract class WcfPeer : IWcfPeer
    {
        private ServiceHost _serviceHost;
        private string _endpointAddress;


        //public static WcfPeer Create<T>(string endpointAddress) where T : IWcfPeerRole
        //{
        //    var peer = new WcfPeer(endpointAddress);
        //    var svh = new SingletonServiceHost(peer);
        //    peer._serviceHost = svh;
        //    svh.AddServiceEndpoint(typeof(IWcfPeer), new NetTcpBinding(), "net.tcp://" + endpointAddress);
        //    svh.Open();
        //    return peer;
        //}


        public WcfPeer(string endpointAddress) { _endpointAddress = endpointAddress; }


        public abstract void Receive(string portname, string data, Guid correlationId, string producerEndpointAddress);

        public event Action<IMessage> Received;

        public abstract void Send(IMessage output);


        public void Dispose()
        {
            _serviceHost.Close();
        }
    }
}
