using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CVR.PlayerListMod.Resources;

namespace CVR.PlayerListMod.Utilities {
  internal class AssetBundleLoader {
    public static AssetBundle LoadBundle(byte[] data, bool dontUnload = true) {
      AssetBundle assetBundle = AssetBundle.LoadFromMemory(data);
      if(dontUnload)
        assetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;
      return assetBundle;
    }

    public static IEnumerator Co_LoadBundle(byte[] data, Action<AssetBundle> callback, bool dontUnload = true) {
      var bundleCreateRequest = AssetBundle.LoadFromMemoryAsync(data);
      yield return bundleCreateRequest;

      var assetBundle = bundleCreateRequest.assetBundle;
      if(dontUnload)
        assetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;

      callback(assetBundle);
      yield break;
    }
  }
}
