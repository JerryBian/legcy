using System.Diagnostics;
using ExitGames.Client.Photon;

namespace Chat.ClientWpf.Operations
{
    public class CreateGameOp2 : IBaseOp
    {
        public byte OpCode => (byte)OpCodes.CreateGame2;

        public void OnResponse(ChatPeerListener peerListener, OperationResponse response)
        {
            Trace.TraceInformation("========================CREATED GAME(GAME SERVER)===========================");
        }
    }
}
