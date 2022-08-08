using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CVR.PlayerListMod.Patches {
  internal class CVR_MenuManager {
    public static void Patch() {
      var harmonyInstance = ModMain.Instance.HarmonyInstance;
      harmonyInstance.Patch(
                typeof(ABI_RC.Core.InteractionSystem.CVR_MenuManager).GetMethod(nameof(ABI_RC.Core.InteractionSystem.CVR_MenuManager.ToggleQuickMenu)),
                null,
                new HarmonyLib.HarmonyMethod(typeof(CVR_MenuManager).GetMethod(nameof(ToggleQuickMenu_Postfix), BindingFlags.Static | BindingFlags.NonPublic))
            );

      harmonyInstance.Patch(
                typeof(ABI_RC.Core.InteractionSystem.CVR_MenuManager).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic),
                null,
                new HarmonyLib.HarmonyMethod(typeof(CVR_MenuManager).GetMethod(nameof(Start_Postfix), BindingFlags.Static | BindingFlags.NonPublic))
            );
    }

    static void ToggleQuickMenu_Postfix(bool __0) {
      Events.QuickMenu.OnToggleOpen(__0);
    }

    static void Start_Postfix(ref ABI_RC.Core.InteractionSystem.CVR_MenuManager __instance) {
      __instance.quickMenu.gameObject.AddComponent<Components.PlayerListLoader>();
    }
  }
}
