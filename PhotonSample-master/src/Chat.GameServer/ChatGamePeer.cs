using Photon.LoadBalancing.GameServer;
using Photon.SocketServer;

namespace Chat.GameServer
{
    public class ChatGamePeer : GameClientPeer
    {
        public ChatGamePeer(InitRequest initRequest, GameApplication application) : base(initRequest, application)
        {
        }
    }
}
