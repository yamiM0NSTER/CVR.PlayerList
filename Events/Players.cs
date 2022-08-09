using ABI_RC.Core.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVR.PlayerListMod.Events {
  internal class Players {
    public static event Action<CVRPlayerEntity> RemotePlayerJoined;
    public static event Action<string> RemotePlayerLeft;

    public static void RemotePlayerJoin(CVRPlayerEntity player) {
      RemotePlayerJoined?.Invoke(player);
    }

    public static void RemotePlayerLeave(string playerId) {
      RemotePlayerLeft?.Invoke(playerId);
    }
  }
}
