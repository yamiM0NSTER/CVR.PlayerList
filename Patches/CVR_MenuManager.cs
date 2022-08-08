using HarmonyLib;
using MenuManager = ABI_RC.Core.InteractionSystem.CVR_MenuManager;

namespace CVR.PlayerListMod.Patches {
  internal class CVR_MenuManager {
    [HarmonyPatch(typeof(MenuManager), nameof(MenuManager.ToggleQuickMenu))]
    class ToggleQuickMenu {
      static void Postfix(bool show) {
        Events.QuickMenu.OnToggleOpen(show);
      }
    }

    [HarmonyPatch(typeof(MenuManager), "Start")]
    class Start {
      static void Postfix(ref MenuManager __instance) {
        __instance.quickMenu.gameObject.AddComponent<Components.PlayerListLoader>();
      }
    }
  }
}
