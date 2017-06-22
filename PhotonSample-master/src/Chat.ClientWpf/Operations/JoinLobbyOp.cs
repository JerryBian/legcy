using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace Chat.ClientWpf.Operations
{
    public class JoinLobbyOp : IBaseOp
    {
        public byte OpCode => (byte)OpCodes.JoinLobby;

        public void OnResponse(ChatPeerListener peerListener, OperationResponse response)
        {
            peerListener.SendOperation((byte)OpCodes.FindMatcherPlayer, new Dictionary<byte, object>());
        }
    }
}
