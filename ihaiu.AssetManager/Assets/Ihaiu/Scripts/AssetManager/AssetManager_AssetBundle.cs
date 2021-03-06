﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Ihaiu.Assets
{
    public partial class AssetManager 
    {
        ManifestAssetBundleManager  manifestAssetBundleManager;

        public Dictionary<string, LoadedAssetBundle>   LoadedAssetBundles
        {
            get
            {
                if (manifestAssetBundleManager != null)
                {
                    return manifestAssetBundleManager.LoadedAssetBundles;
                }
                return null;
            }
        }


        public IEnumerator InitManifest()
        {
            manifestAssetBundleManager =  new ManifestAssetBundleManager(this, AssetManagerSetting.ManifestURL);
            #if UNITY_EDITOR
            if(!AssetManagerSetting.EditorSimulateAssetBundle)
            #endif
            {
                yield return StartCoroutine(manifestAssetBundleManager.LoadManifest());
            }
        }

        internal void OnLoadManifest(IAssetBundleManager manifest)
        {
            PrepareFinal();
        }


        void UpdateAssetBundle()
        {
            if(manifestAssetBundleManager != null)
                manifestAssetBundleManager.Update();
        }


        //=================
        public AssetBundleLoadAssetOperation LoadAssetAsync(string assetBundleName, string assetName, System.Type type)
        {
            return manifestAssetBundleManager.LoadAssetAsync(assetBundleName, assetName, type);
        }

        public AssetBundleLoadOperation LoadLevelAsync(string assetBundleName, string levelName, bool isAdditive)
        {
            return manifestAssetBundleManager.LoadLevelAsync(assetBundleName, levelName, isAdditive);
        }



        //=================
        public void LoadAssetAsync(string assetBundleName, string assetName, System.Type type, Action<string, string, object, object[]> callback, params object[] callbackArgs)
        {
            StartCoroutine(OnLoadAssetAsync(assetBundleName, assetName, type, callback, callbackArgs));
        }

        IEnumerator OnLoadAssetAsync(string assetBundleName, string assetName, System.Type type, Action<string, string, object, object[]> callback, params object[] callbackArgs)
        {
            AssetBundleLoadAssetOperation operation = manifestAssetBundleManager.LoadAssetAsync(assetBundleName, assetName, type);
            yield return operation;

            if (callback != null)
            {
                UnityEngine.Object obj = operation.GetAsset<UnityEngine.Object>();
                callback(assetBundleName, assetName, obj, callbackArgs);
            }
        }


        //=================
        public void LoadAssetAsync(string assetBundleName, string assetName, System.Type type, Action<string, object, object[]> callback, params object[] callbackArgs)
        {
            StartCoroutine(OnLoadAssetAsync(assetBundleName, assetName, type, callback, callbackArgs));
        }

        IEnumerator OnLoadAssetAsync(string assetBundleName, string assetName, System.Type type, Action<string, object, object[]> callback, params object[] callbackArgs)
        {
            AssetBundleLoadAssetOperation operation = manifestAssetBundleManager.LoadAssetAsync(assetBundleName, assetName, type);
            yield return operation;

            if (callback != null)
            {
                UnityEngine.Object obj = operation.GetAsset<UnityEngine.Object>();
                callback(assetBundleName, obj, callbackArgs);
            }
        }

        //----------------
        public void LoadLevelAsync(string assetBundleName, string levelName, bool isAdditive, Action<string, string, object[]> callback, params object[] callbackArgs)
        {
            StartCoroutine(OnLoadLevelAsync(assetBundleName, levelName, isAdditive, callback, callbackArgs));
        }

        IEnumerator OnLoadLevelAsync(string assetBundleName, string levelName, bool isAdditive, Action<string, string, object[]> callback, params object[] callbackArgs)
        {
            AssetBundleLoadOperation operation = manifestAssetBundleManager.LoadLevelAsync(assetBundleName, levelName, isAdditive);
            yield return operation;

            if (callback != null)
            {
                callback(assetBundleName, levelName, callbackArgs);
            }
        }



        //----------------
        /** 卸载资源包和他依赖的资源包 */
        public void UnloadAssetBundle(string assetBundleName)
        {
            manifestAssetBundleManager.UnloadAssetBundle(assetBundleName);
        }

    }
}