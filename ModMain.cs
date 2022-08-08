using MelonLoader;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVR.PlayerListMod {
  public class ModMain : MelonMod {

    public static ModMain Instance { get; private set; }

    public MelonLogger.Instance Logger => Instance.LoggerInstance;

    public override void OnApplicationStart() {
      Instance = this;
      LoggerInstance.Msg("Hello");
      Patches.CVR_MenuManager.Patch();
      Patches.CVRPlayerManager.Patch();
    }
  }
}
