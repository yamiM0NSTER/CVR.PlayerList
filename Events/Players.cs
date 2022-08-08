using ABI_RC.Core.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVR.PlayerListMod.Events {
  internal class Players {
    public static event Action<CVRPlayerEntity> OnRemotePlayerJoin;
    public static event Action<string> OnRemotePlayerLeave;

    public static void RemotePlayerJoin(CVRPlayerEntity player) {
      OnRemotePlayerJoin?.Invoke(player);
    }

    public static void RemotePlayerLeave(string playerId) {
      OnRemotePlayerLeave?.Invoke(playerId);
    }
  }
}
