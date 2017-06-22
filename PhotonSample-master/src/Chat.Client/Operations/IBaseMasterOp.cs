using ExitGames.Client.Photon;

namespace Chat.Client.Operations
{
    public interface IBaseMasterOp
    {
        byte OpCode { get; }

        void OnResponse(OperationResponse response);

        // void OnRequest(OperationRequest request);
    }
}
