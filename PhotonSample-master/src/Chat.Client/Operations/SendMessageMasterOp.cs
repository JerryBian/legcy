using ExitGames.Client.Photon;

namespace Chat.Client.Operations
{
    public class SendMessageMasterOp : IBaseMasterOp
    {
        public byte OpCode => (byte)OpCodes.SendMessage;

        public void OnResponse(OperationResponse response)
        {
            
        }
    }
}
