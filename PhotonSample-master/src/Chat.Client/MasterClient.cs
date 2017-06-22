using System;
using System.Collections.Generic;
using System.Linq;
using Chat.Client.Operations;
using ExitGames.Client.Photon;
using Newtonsoft.Json;

namespace Chat.Client
{
    public class MasterClient : IPhotonPeerListener
    {
        private bool _connected;
        private IDictionary<byte, IBaseMasterOp> _operations;

        public PhotonPeer Peer { get; private set; }


        public MasterClient()
        {
            Peer = new PhotonPeer(this, ConnectionProtocol.Udp);
            _connected = false;

            _operations = new Dictionary<byte, IBaseMasterOp>();
            foreach (var item in typeof(IBaseMasterOp).Assembly.GetTypes().Where(_ => !_.IsAbstract && typeof(IBaseMasterOp).IsAssignableFrom(_)))
            {
                var obj = (IBaseMasterOp)Activator.CreateInstance(item);
                _operations[obj.OpCode] = obj;
            }
        }

        public void Service()
        {
            Peer.Service();
        }

        public void SendOperation(byte operationCode, Dictionary<byte, object> parameters)
        {
            Peer.OpCustom(operationCode, parameters, true);
        }

        public void Connect(string masterServerAddress, string masterServerAppName)
        {
            Peer.Connect(masterServerAddress, masterServerAppName);
            while (!_connected)
            {
                Peer.Service();
            }
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            Console.WriteLine(level + ": " + message);
        }

        public void OnEvent(EventData eventData)
        {
            Console.WriteLine("Event: " + eventData.Code);
            if (eventData.Code == 1)
            {
                Console.WriteLine("Chat: " + eventData.Parameters[1]);
            }
        }

        public void OnMessage(object messages)
        {
            throw new NotImplementedException();
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            Console.WriteLine($"Response({(OpCodes)operationResponse.OperationCode}): " + JsonConvert.SerializeObject(operationResponse));
            _operations[operationResponse.OperationCode].OnResponse(operationResponse);
        }

        public void OnStatusChanged(StatusCode statusCode)
        {
            if (statusCode == StatusCode.Connect)
            {
                _connected = true;
                Console.WriteLine("Connected.");
            }
            else
            {
                Console.WriteLine("Status: " + statusCode);
            }
        }
    }
}
