using System;
using System.Linq;
using System.Threading.Tasks;
using Photon.LoadBalancing.MasterServer;

namespace Chat.MasterServer
{
    public class ChatMasterApplication : MasterApplication
    {
        private readonly Random _random;

        public ChatMasterApplication() : base()
        {
            _random = new Random(Environment.TickCount);
        }

        protected override void Setup()
        {
            base.Setup();
        }

        protected override void Initialize()
        {
            base.Initialize();
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var lobbies = DefaultApplication.LobbyFactory.GetLobbies(int.MaxValue);
                    Parallel.ForEach(lobbies, lobby =>
                    {
                        var currentPeers = lobby.Peers.Where(_ => _ is MasterClientPeer).Cast<MasterClientPeer>().Where(_ => string.IsNullOrEmpty(_.GameId)).OrderBy(_ => _.ConnectionId).ToList();
                        while (currentPeers.Count > 1)
                        {
                            var currentPeer = currentPeers.First();
                            var matchedPeer = currentPeers.ElementAt(_random.Next(1, currentPeers.Count));
                            var gameId = Guid.NewGuid().ToString();
                            currentPeer.GameId = matchedPeer.GameId = gameId;
                            currentPeers.Remove(currentPeer);
                            currentPeers.Remove(matchedPeer);
                        }
                    });
                    await Task.Delay(TimeSpan.FromMilliseconds(10));
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
