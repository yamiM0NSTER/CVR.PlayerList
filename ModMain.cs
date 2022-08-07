using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVR.PlayerList
{
  public class ModMain : MelonMod {
    public override void OnApplicationStart() {
      LoggerInstance.Msg("Hello");
    }
  }
}
