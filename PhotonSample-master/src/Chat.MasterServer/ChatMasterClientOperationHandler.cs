using Photon.Common;
using Photon.LoadBalancing.Common;
using Photon.LoadBalancing.MasterServer;
using Photon.LoadBalancing.Operations;
using Photon.SocketServer;

namespace Chat.MasterServer
{
    public class ChatMasterClientOperationHandler : OperationHandlerBase
    {
        public override void OnDisconnect(PeerBase peer)
        {
            throw new System.NotImplementedException();
        }

        public override OperationResponse OnOperationRequest(PeerBase peer, OperationRequest operationRequest, SendParameters sendParameters)
        {
            var clientPeer = (MasterClientPeer)peer;

            switch (operationRequest.OperationCode)
            {
                default:
                    return new OperationResponse(operationRequest.OperationCode)
                    {
                        ReturnCode = (short)ErrorCode.OperationInvalid,
                        DebugMessage = "UnknownOperationCode"
                    };

                case (byte)OperationCode.Authenticate:
                    return clientPeer.HandleAuthenticate(operationRequest, sendParameters);

                case (byte)OperationCode.JoinLobby:
                    return clientPeer.HandleJoinLobby(operationRequest, sendParameters);

                case (byte)OperationCode.LeaveLobby:
                    return clientPeer.HandleLeaveLobby(operationRequest);

                case (byte)OperationCode.CreateGame:
                    return clientPeer.HandleCreateGame(operationRequest, sendParameters);

                case (byte)OperationCode.JoinGame:
                    return clientPeer.HandleJoinGame(operationRequest, sendParameters);

                case (byte)OperationCode.JoinRandomGame:
                    return clientPeer.HandleJoinRandomGame(operationRequest, sendParameters);

                case (byte)OperationCode.FindFriends:
                    return clientPeer.HandleFindFriends(operationRequest, sendParameters);

                case (byte)OperationCode.LobbyStats:
                    return clientPeer.HandleLobbyStatsRequest(operationRequest, sendParameters);

                case (byte)OperationCode.Rpc:
                    return clientPeer.HandleRpcRequest(operationRequest, sendParameters);
            }

            switch (operationRequest.OperationCode)
            {
                default:
                    return new OperationResponse(operationRequest.OperationCode)
                    {
                        ReturnCode = (short)ErrorCode.OperationInvalid,
                        DebugMessage = "UnknownOperationCode"
                    };

                case (byte)OperationCode.Authenticate:
                    return ((MasterClientPeer)peer).HandleAuthenticate(operationRequest, sendParameters);

                case (byte)OperationCode.CreateGame:
                case (byte)OperationCode.JoinGame:
                case (byte)OperationCode.JoinLobby:
                case (byte)OperationCode.JoinRandomGame:
                case (byte)OperationCode.FindFriends:
                case (byte)OperationCode.LobbyStats:
                case (byte)OperationCode.LeaveLobby:
                case (byte)OperationCode.DebugGame:
                case (byte)OperationCode.Rpc:
                    return new OperationResponse(operationRequest.OperationCode)
                    {
                        ReturnCode = (int)Photon.Common.ErrorCode.OperationDenied,
                        DebugMessage = "NotAuthorized"
                    };
            }
        }
    }
}
