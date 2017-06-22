using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using Chat.ClientWpf.Operations;
using ExitGames.Client.Photon;
using Newtonsoft.Json;

namespace Chat.ClientWpf
{
    public class ChatPeerListener : IPhotonPeerListener
    {
        private bool _connected;
        private readonly PhotonPeer _peer;
        private IDictionary<byte, IBaseOp> _operations;

        public PlayerInfo Player { get; private set; }

        public ChatClient ChatClient { get; private set; }

        public ChatPeerListener(ChatClient chatClient, PlayerInfo playerInfo, string serverAddress, string applicationName)
        {
            Player = playerInfo;
            ChatClient = chatClient;
            _connected = false;
            _operations = new Dictionary<byte, IBaseOp>();
            foreach (var item in typeof(IBaseOp).Assembly.GetTypes().Where(_ => !_.IsAbstract && typeof(IBaseOp).IsAssignableFrom(_)))
            {
                var obj = (IBaseOp)Activator.CreateInstance(item);
                _operations[obj.OpCode] = obj;
            }

            _peer = new PhotonPeer(this, ConnectionProtocol.Udp);
            _peer.Connect(serverAddress, applicationName);
            while (!_connected)
            {
                _peer.Service();
            }

            var t = new Timer
            {
                Enabled = true,
                Interval = 10
            };

            t.Elapsed += (sender, args) => _peer.Service();
            t.Start();
        }

        public void Service()
        {
            _peer.Service();
        }

        public void SendOperation(byte opCode, Dictionary<byte, object> parameters)
        {
            _peer.OpCustom(opCode, parameters, true);
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            throw new System.NotImplementedException();
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            Trace.TraceInformation($"Response({(OpCodes)operationResponse.OperationCode}): " + JsonConvert.SerializeObject(operationResponse));
            _operations[operationResponse.OperationCode].OnResponse(this, operationResponse);
        }

        public void OnStatusChanged(StatusCode statusCode)
        {
            Trace.TraceInformation($"Status Changed:: {statusCode}");
            switch (statusCode)
            {
                case StatusCode.Connect:
                    _connected = true;
                    break;
                default:
                    
                    break;
            }
        }

        public void OnEvent(EventData eventData)
        {
            Trace.TraceInformation($"OnEvent:: {JsonConvert.SerializeObject(eventData)}");
        }

        public void OnMessage(object messages)
        {
            throw new System.NotImplementedException();
        }
    }
}
