using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace Chat.ClientWpf.Operations
{
    public class FindMatchedPlayerOp : IBaseOp
    {
        public byte OpCode => (byte)OpCodes.FindMatcherPlayer;

        public void OnResponse(ChatPeerListener peerListener, OperationResponse response)
        {
            var createGame = response.Parameters[223] as bool?;
            if (createGame != null && createGame.Value)
            {
                peerListener.SendOperation((byte) OpCodes.CreateGame,
                    new Dictionary<byte, object> {{255, response.Parameters[225]}, {239, true} });
            }
            else
            {
                peerListener.SendOperation((byte)OpCodes.JoinChat, new Dictionary<byte, object> { { 255, response.Parameters[225] }, {239, true} });
            }

            peerListener.Player.GameId = (string)response.Parameters[225];

            //peerListener.SendOperation((byte)OpCodes.CreateGame, new Dictionary<byte, object> { { 255, response.Parameters[225] }, { 215, (byte)1 } });
        }
    }
}
