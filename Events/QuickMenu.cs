using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVR.PlayerListMod.Events {
  internal class QuickMenu {
    public static event Action OnQuickMenuOpen;
    public static event Action OnQuickMenuClose;

    public static void OnToggleOpen(bool open) {
      if(open) {
        OnQuickMenuOpen?.Invoke();
      } else {
        OnQuickMenuClose?.Invoke();
      }
    }
    public static void OnOpen() => OnQuickMenuOpen?.Invoke();
    public static void OnClose() => OnQuickMenuClose?.Invoke();
  }
}
