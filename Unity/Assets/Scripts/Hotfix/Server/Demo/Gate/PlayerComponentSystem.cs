using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    [FriendOf(typeof(PlayerComponent))]
    public static partial class PlayerComponentSystem
    {
        public static void Add(this PlayerComponent self, Player player)
        {
            self.dictionary.Add(player.Account, player);
        }
        
        public static void Remove(this PlayerComponent self, Player player)
        {
            self.dictionary.Remove(player.Account);
            player.Dispose();
        }
        
        public static Player GetByAccount(this PlayerComponent self,  string account)
        {
            self.dictionary.TryGetValue(account, out var player);
            return player;
        }

        public static Dictionary<int, Player> GetByAllAccounts(this PlayerComponent self)
        {
            Dictionary<int, Player> playerDic = new Dictionary<int, Player>();
            int count = 0;
            foreach(var key in self.dictionary.Values)
            {
                playerDic.Add(count, key);
                count++;
            }
            return playerDic;
        }
    }
}