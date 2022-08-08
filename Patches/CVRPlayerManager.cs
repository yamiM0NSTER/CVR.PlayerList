using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DarkRift;
using PlayerManager = ABI_RC.Core.Player.CVRPlayerManager;

namespace CVR.PlayerListMod.Patches {
  internal class CVRPlayerManager {
    public static void Patch() {
      var harmonyInstance = ModMain.Instance.HarmonyInstance;
      harmonyInstance.Patch(
                typeof(PlayerManager).GetMethod(nameof(PlayerManager.TryCreatePlayer)),
                new HarmonyLib.HarmonyMethod(typeof(CVRPlayerManager).GetMethod(nameof(TryCreatePlayer_Prefix), BindingFlags.Static | BindingFlags.NonPublic)),
                new HarmonyLib.HarmonyMethod(typeof(CVRPlayerManager).GetMethod(nameof(TryCreatePlayer_Postfix), BindingFlags.Static | BindingFlags.NonPublic))
            );

      harmonyInstance.Patch(
          typeof(PlayerManager).GetMethod(nameof(PlayerManager.TryDeletePlayer)),
          new HarmonyLib.HarmonyMethod(typeof(CVRPlayerManager).GetMethod(nameof(TryDeletePlayer_Prefix), BindingFlags.Static | BindingFlags.NonPublic)),
          new HarmonyLib.HarmonyMethod(typeof(CVRPlayerManager).GetMethod(nameof(TryDeletePlayer_Postfix), BindingFlags.Static | BindingFlags.NonPublic))
      );
    }

    static void TryCreatePlayer_Prefix(ref PlayerManager __instance, out int __state) {
      __state = __instance.NetworkPlayers.Count;
    }

    static void TryCreatePlayer_Postfix(ref PlayerManager __instance, int __state) {
      if(__state < __instance.NetworkPlayers.Count) {
        Events.Players.RemotePlayerJoin(__instance.NetworkPlayers[__state]);
      }
    }

    static void TryDeletePlayer_Prefix(ref PlayerManager __instance, out int __state) {
      __state = __instance.NetworkPlayers.Count;
    }

    static void TryDeletePlayer_Postfix(ref PlayerManager __instance, int __state, Message message) {
      if(__state == __instance.NetworkPlayers.Count) {
        return;
      }

      using(var reader = message.GetReader()) {
        string playerId = reader.ReadString();
        Events.Players.RemotePlayerLeave(playerId);
      }
    }
  }
}
