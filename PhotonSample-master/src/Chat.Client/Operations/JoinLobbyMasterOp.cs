using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Client.Photon;

namespace Chat.Client.Operations
{
    public class JoinLobbyMasterOp : IBaseMasterOp
    {
        public byte OpCode => (byte)OpCodes.JoinLobby;

        public void OnResponse(OperationResponse response)
        {
            
        }
    }
}
