using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVR.PlayerListMod.Events {
  internal class QuickMenu {
    public static event Action QuickMenuOpened;
    public static event Action QuickMenuClosed;

    public static void OnToggleOpen(bool open) {
      if(open) {
        QuickMenuOpened?.Invoke();
      } else {
        QuickMenuClosed?.Invoke();
      }
    }
    public static void OnOpen() => QuickMenuOpened?.Invoke();
    public static void OnClose() => QuickMenuClosed?.Invoke();
  }
}
