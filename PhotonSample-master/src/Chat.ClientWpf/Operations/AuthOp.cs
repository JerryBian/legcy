using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace Chat.ClientWpf.Operations
{
    public class AuthOp : IBaseOp
    {
        public byte OpCode => (byte)OpCodes.Auth;

        public void OnResponse(ChatPeerListener peerListener, OperationResponse response)
        {
            peerListener.SendOperation((byte)OpCodes.LobbyStats, new Dictionary<byte, object> { });
        }
    }
}
