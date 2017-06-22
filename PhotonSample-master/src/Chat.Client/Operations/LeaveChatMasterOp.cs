using ExitGames.Client.Photon;

namespace Chat.Client.Operations
{
    public class LeaveChatMasterOp : IBaseMasterOp
    {
        public byte OpCode => (byte)OpCodes.LeaveChat;

        public void OnResponse(OperationResponse response)
        {
            
        }
    }
}
