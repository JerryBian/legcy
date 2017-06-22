using ExitGames.Client.Photon;

namespace Chat.Client.Operations
{
    public class LobbyStatsMasterOp : IBaseMasterOp
    {
        public byte OpCode => (byte)OpCodes.LobbyStats;

        public void OnResponse(OperationResponse response)
        {
            
        }
    }
}
