
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class AssetBundleInformation : IDisposable
{
    public AssetBundle AssetBundle;

    public IEnumerator Setup(string url, int version)
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;

        // Start the download
        using (WWW www = WWW.LoadFromCacheOrDownload(url, version))
        {
            while (www.isDone == false && www.progress != 1)
            {
                yield return null;
            }
            if (www.error != null)
            {
                yield break;
            }
            yield return www;
            AssetBundle = www.assetBundle;
        }
    }


    public T DownloadAssetBundle<T>(string asset) where T : UnityEngine.Object
    {
#if UNITY_EDITOR
        return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(asset);
#else
        
        return AssetBundle.LoadAsset<T>(asset);

#endif
    }

    public void Dispose()
    {
        if(CommonFunction.IsNull(AssetBundle) == false)
        {
            AssetBundle.Unload(false);
        }
    }
}