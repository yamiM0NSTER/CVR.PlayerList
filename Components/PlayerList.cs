using ABI_RC.Core.Player;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using NetworkManager = ABI_RC.Core.Networking.NetworkManager;
using PlayerManager = ABI_RC.Core.Player.CVRPlayerManager;

namespace CVR.PlayerListMod.Components {
  internal class PlayerList : MonoBehaviour {
    MelonLogger.Instance _logger;
    Dictionary<string, PlayerListEntry> _players = new Dictionary<string, PlayerListEntry>();

    GameObject _gameObject;
    RectTransform _rectTransform;
    RectTransform _contentTransform;
    GameObject _playerEntryPrefab;
    TextMeshProUGUI _playerCountText;

    void Awake() {
      _logger = ModMain.Instance.Logger;
      _gameObject = gameObject;
      _rectTransform = GetComponent<RectTransform>();
      _contentTransform = (RectTransform)_rectTransform.Find("PlayerList Scroll View/Viewport/Content");
      _playerEntryPrefab = _contentTransform.Find("Template").gameObject;
      _playerCountText = _contentTransform.Find("Header").GetComponent<TextMeshProUGUI>();
      SubscribeToEvents();
    }
    void Start() {
      _rectTransform.localScale = new Vector3(0.0004f, 0.0004f, 0.0004f);
      _rectTransform.anchoredPosition = new Vector2(0.69f, 0);
      _gameObject.layer = 5; // UI Layer
      _gameObject.SetActive(false);
      SetPlayerCountText();
    }

    void SubscribeToEvents() {
      Events.QuickMenu.OnQuickMenuClose += OnQuickMenuClose;
      Events.QuickMenu.OnQuickMenuOpen += OnQuickMenuOpen;
      Events.Players.OnRemotePlayerJoin += OnPlayerJoin;
      Events.Players.OnRemotePlayerLeave += OnPlayerLeave;
      NetworkManager.Instance.GameNetwork.Disconnected += OnDisconnected;
    }

    void UnsubscribeFromEvents() {
      Events.QuickMenu.OnQuickMenuClose -= OnQuickMenuClose;
      Events.QuickMenu.OnQuickMenuOpen -= OnQuickMenuOpen;
      Events.Players.OnRemotePlayerJoin -= OnPlayerJoin;
      Events.Players.OnRemotePlayerLeave -= OnPlayerLeave;
      NetworkManager.Instance.GameNetwork.Disconnected -= OnDisconnected;
    }

    void OnDestroy() {
      UnsubscribeFromEvents();
    }

    void OnDisconnected(object sender, DarkRift.Client.DisconnectedEventArgs args) {
      foreach(var entry in _players.Values) {
        RemoveEntry(entry);
      }
      _players.Clear();
      SetPlayerCountText();
    }

    void OnQuickMenuOpen() {
      _gameObject.SetActive(true);
    }

    void OnQuickMenuClose() {
      _gameObject.SetActive(false);
    }
    
    void OnPlayerJoin(CVRPlayerEntity player) {
      var playerData = new Data.PlayerData(player.Uuid, player.Username);
      _logger.Msg($"[Join] Player: {playerData.Name}[{playerData.Id}]");

      // TODO: Grab from pool
      GameObject playerEntry = UnityEngine.Object.Instantiate(_playerEntryPrefab, _contentTransform);
      var entryComp = playerEntry.AddComponent<PlayerListEntry>();
      entryComp.Bind(playerData);
      _players.Add(playerData.Id, entryComp);
      playerEntry.SetActive(true);
      SetPlayerCountText();
    }

    void RemoveEntry(PlayerListEntry entry) {
      // TODO: Return to pool
      Destroy(entry.gameObject);
    }

    void SetPlayerCountText() {
      _playerCountText.text = $"<color=yellow>Total:</color> {PlayerManager.Instance.NetworkPlayers.Count}";
    }

    void OnPlayerLeave(string playerId) {
      if(!_players.TryGetValue(playerId, out var entry))
        return;

      RemoveEntry(entry);
      _players.Remove(playerId);
      _logger.Msg($"[Leave] Player: {entry.PlayerData.Name}[{playerId}]");
      SetPlayerCountText();
    }
  }
}
