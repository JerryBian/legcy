using ExitGames.Client.Photon;

namespace Chat.Client.Operations
{
    public class JoinChatMasterOp : IBaseMasterOp
    {
        public byte OpCode => (byte)OpCodes.JoinChat;

        public void OnResponse(OperationResponse response)
        {
            
        }
    }
}
