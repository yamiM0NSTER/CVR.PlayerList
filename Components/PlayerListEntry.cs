using CVR.PlayerListMod.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using ABI_RC.Core.Player;

namespace CVR.PlayerListMod.Components {
  internal class PlayerListEntry : MonoBehaviour {
    PlayerData _playerData;
    TextMeshProUGUI _textLeft;
    TextMeshProUGUI _textRight;
    Transform _transform;

    public PlayerData PlayerData => _playerData;

    void Awake() {
      _transform = transform;
      _textLeft = _transform.Find("LeftPart").GetComponent<TextMeshProUGUI>();
      _textRight = _transform.Find("RightPart").GetComponent<TextMeshProUGUI>();
      SubscribeToEvents();
    }

    void Start() {
      _textRight.text = _playerData.Name;
      UpdateLeftText();
    }

    void OnDestroy() {
      UnsubscribeFromEvents();
    }

    void SubscribeToEvents() {
      Events.Players.OnRemotePlayerJoin += OnPlayerJoin;
      Events.Players.OnRemotePlayerLeave += OnPlayerLeave;
    }

    void UnsubscribeFromEvents() {
      Events.Players.OnRemotePlayerJoin -= OnPlayerJoin;
      Events.Players.OnRemotePlayerLeave -= OnPlayerLeave;
    }

    void OnPlayerJoin(CVRPlayerEntity player) {
      UpdateLeftText();
    }

    void OnPlayerLeave(string playerId) {
      UpdateLeftText();
    }

    void UpdateLeftText() {
      _textLeft.text = $"<mspace=0.5em>{_transform.GetSiblingIndex() - 1,3:D}|";
    }

    internal void Bind(PlayerData playerData) {
      _playerData = playerData;
    }
  }
}
