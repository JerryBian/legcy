using ExitGames.Client.Photon;

namespace Chat.Client.Operations
{
    public class CreateGameMasterOp : IBaseMasterOp
    {
        public byte OpCode => (byte)OpCodes.CreateGame;

        public void OnResponse(OperationResponse response)
        {
            
        }
    }
}
