using ExitGames.Client.Photon;

namespace Chat.Client.Operations
{
    public class FindFriendMasterOp : IBaseMasterOp
    {
        public byte OpCode => (byte)OpCodes.FindFriend;

        public void OnResponse(OperationResponse response)
        {
            
        }
    }
}
