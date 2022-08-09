using CVR.PlayerListMod.Utilities;
using MelonLoader;
using System;
using UnityEngine;

namespace CVR.PlayerListMod.Components {
  public class PlayerListLoader : MonoBehaviour {
    MelonLogger.Instance _logger;

    private void Awake() {
      _logger = ModMain.Instance.Logger;
    }

    private void Start() {
      StartCoroutine(AssetBundleLoader.Co_LoadBundle(Resources.Resources.playerlistmod, new Action<AssetBundle>(OnAssetBundleLoaded)));
    }

    const string assetPath = "Assets/Prefabs/PlayerListMod.prefab";

    void OnAssetBundleLoaded(AssetBundle assetBundle) {
      var prefab = assetBundle.LoadAsset<GameObject>(assetPath);
      var playerList = UnityEngine.Object.Instantiate(prefab, gameObject.transform);
      playerList.AddComponent<PlayerList>();
      Destroy(this);
    }
  }
}
