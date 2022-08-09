using DarkRift;
using HarmonyLib;
using PlayerManager = ABI_RC.Core.Player.CVRPlayerManager;

namespace CVR.PlayerListMod.Patches {
  internal class CVRPlayerManager {
    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.TryCreatePlayer))]
    class TryCreatePlayer {
      static void Prefix(ref PlayerManager __instance, out int __state) {
        __state = __instance.NetworkPlayers.Count;
      }
      static void Postfix(ref PlayerManager __instance, int __state) {
        if(__state < __instance.NetworkPlayers.Count) {
          Events.Players.RemotePlayerJoin(__instance.NetworkPlayers[__state]);
        }
      }
    }

    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.TryDeletePlayer))]
    class TryDeletePlayer {
      static void Prefix(ref PlayerManager __instance, out int __state) {
        __state = __instance.NetworkPlayers.Count;
      }

      // This is highly susceptible to breaking
      static void Postfix(ref PlayerManager __instance, int __state, Message message) {
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
}
