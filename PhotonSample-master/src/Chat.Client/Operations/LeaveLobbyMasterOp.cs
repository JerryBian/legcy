using System;
using ExitGames.Client.Photon;

namespace Chat.Client.Operations
{
    public class LeaveLobbyMasterOp : IBaseMasterOp
    {
        public byte OpCode => (byte)OpCodes.LeaveLobby;

        public void OnResponse(OperationResponse response)
        {
           
        }
    }
}
