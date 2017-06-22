using Photon.Common.Authentication;
using Photon.Common.Authentication.Diagnostic;
using Photon.SocketServer;

namespace Chat.MasterServer
{
    public class ChatMasterClientAuthHandler : CustomAuthHandler
    {
        public ChatMasterClientAuthHandler(IHttpRequestQueueCountersFactory factory) : base(factory)
        {
            IsAnonymousAccessAllowed = false;
            IsClientAuthenticationEnabled = true;
        }

        protected override void OnAuthenticateClient(
            ICustomAuthPeer peer, 
            IAuthenticateRequest authRequest, 
            AuthSettings authSettings,
            SendParameters sendParameters, object state)
        {
            base.OnAuthenticateClient(peer, authRequest, authSettings, sendParameters, state);
        }
    }
}
