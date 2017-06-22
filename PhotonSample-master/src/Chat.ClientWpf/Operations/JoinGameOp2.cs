using System.Collections.Generic;
using System.Diagnostics;
using ExitGames.Client.Photon;

namespace Chat.ClientWpf.Operations
{
    public class JoinGameOp2 : IBaseOp
    {
        public byte OpCode => (byte) OpCodes.JoinChat2;

        public void OnResponse(ChatPeerListener peerListener, OperationResponse response)
        {
            Trace.TraceInformation("======================JOIN GAME(GAME SERVER)==============");
            peerListener.SendOperation(1, new Dictionary<byte, object> { { 255, peerListener.Player.GameId } });
        }
    }
}
