using ExitGames.Client.Photon;

namespace Chat.ClientWpf.Operations
{
    public interface IBaseOp
    {
        byte OpCode { get; }

        void OnResponse(ChatPeerListener peerListener, OperationResponse response);
    }
}
