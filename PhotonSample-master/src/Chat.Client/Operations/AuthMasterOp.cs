using ExitGames.Client.Photon;

namespace Chat.Client.Operations
{
    public class AuthMasterOp : IBaseMasterOp
    {
        public byte OpCode => (byte)OpCodes.Auth;

        public void OnResponse(OperationResponse response)
        {

        }
    }
}
