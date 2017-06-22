using System;
using System.Collections.Generic;

namespace Chat.ClientWpf
{
    public class PlayerFactory
    {
        private static readonly Dictionary<string, PlayerInfo> Repository = new Dictionary<string, PlayerInfo>(StringComparer.InvariantCultureIgnoreCase);

        public static void AddPlayer(string userId, PlayerInfo playerInfo)
        {
            if (Repository.ContainsKey(userId))
            {
                throw new ArgumentException();
            }

            Repository.Add(userId, playerInfo);
        }

        public static PlayerInfo GetPlayer(string userId)
        {
            if (!Repository.ContainsKey(userId))
            {
                throw new KeyNotFoundException();
            }

            return Repository[userId];
        }
    }
}
