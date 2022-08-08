using System.Reflection;
using MenuManager = ABI_RC.Core.InteractionSystem.CVR_MenuManager;

namespace CVR.PlayerListMod.Patches {
  internal class CVR_MenuManager {
    public static void Patch() {
      var harmonyInstance = ModMain.Instance.HarmonyInstance;
      harmonyInstance.Patch(
                typeof(MenuManager).GetMethod(nameof(MenuManager.ToggleQuickMenu)),
                null,
                new HarmonyLib.HarmonyMethod(typeof(CVR_MenuManager).GetMethod(nameof(ToggleQuickMenu_Postfix), BindingFlags.Static | BindingFlags.NonPublic))
            );

      harmonyInstance.Patch(
                typeof(MenuManager).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic),
                null,
                new HarmonyLib.HarmonyMethod(typeof(CVR_MenuManager).GetMethod(nameof(Start_Postfix), BindingFlags.Static | BindingFlags.NonPublic))
            );
    }

    static void ToggleQuickMenu_Postfix(bool show) {
      Events.QuickMenu.OnToggleOpen(show);
    }

    static void Start_Postfix(ref MenuManager __instance) {
      __instance.quickMenu.gameObject.AddComponent<Components.PlayerListLoader>();
    }
  }
}
