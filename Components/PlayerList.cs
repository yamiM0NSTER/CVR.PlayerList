using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CVR.PlayerListMod.Components {
  internal class PlayerList : MonoBehaviour {
    MelonLogger.Instance _logger;
    GameObject _gameObject;
    RectTransform _rectTransform;

    void Awake() {
      _logger = ModMain.Instance.Logger;
      _gameObject = gameObject;
      _rectTransform = GetComponent<RectTransform>();
      SubscribeToEvents();
    }
    void Start() {
      _rectTransform.localScale = new Vector3(0.0004f, 0.0004f, 1f);
      _rectTransform.anchoredPosition = new Vector2(0.69f, 0);
      _gameObject.layer = 5; // UI Layer
      _gameObject.SetActive(false);
    }

    void SubscribeToEvents() {
      Events.QuickMenu.OnQuickMenuClose += OnQuickMenuClose;
      Events.QuickMenu.OnQuickMenuOpen += OnQuickMenuOpen;
    }

    void UnsubscribeFromEvents() {
      Events.QuickMenu.OnQuickMenuClose -= OnQuickMenuClose;
      Events.QuickMenu.OnQuickMenuOpen -= OnQuickMenuOpen;
    }

    void OnDestroy() {
      UnsubscribeFromEvents();
    }

    void OnQuickMenuOpen() {
      _gameObject.SetActive(true);
    }

    void OnQuickMenuClose() {
      _gameObject.SetActive(false);
    }
  }
}
