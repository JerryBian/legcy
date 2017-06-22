using Photon.Common.Authentication;
using Photon.Common.Authentication.CustomAuthentication;
using Photon.LoadBalancing.MasterServer;
using Photon.SocketServer;

namespace Chat.MasterServer
{
    public class ChatMasterClientPeer : MasterClientPeer
    {
        public ChatMasterClientPeer(InitRequest initRequest) : base(initRequest)
        {
            
            SetCurrentOperationHandler(new ChatMasterClientOperationHandler());
        }

        public override void OnCustomAuthenticationResult(CustomAuthenticationResult customAuthResult, IAuthenticateRequest authenticateRequest,
            SendParameters sendParameters, object state)
        {
            base.OnCustomAuthenticationResult(customAuthResult, authenticateRequest, sendParameters, state);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            base.OnOperationRequest(operationRequest, sendParameters);
        }
    }
}
