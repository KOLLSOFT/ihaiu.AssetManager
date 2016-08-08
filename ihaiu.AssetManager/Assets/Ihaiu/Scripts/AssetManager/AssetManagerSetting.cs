﻿using UnityEngine;
using System.Collections;

namespace Ihaiu.Assets
{
    public partial class AssetManagerSetting 
    {
        public static bool TestVersionMode = false;
        public static string BytesExt = ".txt";
        public static string AssetbundleExt = "-assetbundle";

        public static string PersistentAssetListName    = "PersistentAssetList.csv";
        public static string FilesName                  = "files.csv";
        public static string AssetBundleListName        = "AssetBundleList.csv";
        public static string AssetListName              = "AssetList.csv";
        public static string UpdateAssetListName        = "UpdateAssetListName.csv";
        public static string GameConstName              = "game_const.csv";



        public static string RootPathStreaming
        {
            get
            {
                return Application.streamingAssetsPath + "/";
            }
        }

        public static string RootPathPersistent
        {
            get
            {
                #if UNITY_EDITOR
                if(TestVersionMode)
                {
                    return Application.dataPath + "/../res/";
                }
                else
                {
                    return RootPathStreaming;
                }
                #endif

                #if UNITY_STANDALONE
                switch(Application.platform)
                {
                    case RuntimePlatform.WindowsPlayer:
                        return Application.dataPath + "/../res/" ;
                    case RuntimePlatform.OSXPlayer:
                        return Application.dataPath + "/res/" ;
                }
                #endif

                return Application.persistentDataPath + "/";
            }
        }

        public static string RootUrlStreaming
        {
            get
            {
                if (Application.platform == RuntimePlatform.Android) 
                {
                    return RootPathStreaming;
                }
                else
                {
                    return "file:///" + RootPathStreaming;
                }
            }
        }


        public static string RootUrlPersistent
        {
            get
            {
                return "file:///" + RootPathPersistent;
            }
        }

        public static string GameConstUrl_Streaming     = RootUrlStreaming      + GameConstName;
        public static string GameConstUrl_Persistent    = RootUrlPersistent     + GameConstName;

        public static string AssetFileListPath = RootPathPersistent + AssetListName;
        public static string PersistentAssetFileListPath = RootPathPersistent + PersistentAssetListName;


        public static AssetFileList persistentAssetFileList = new AssetFileList();


        /** 是否严格 */
        public static bool IsStrict = true;

        /** 强制异步加载,等待一帧(Resource.AsyLoad) */
        public static bool ForcedResourceAsynLoadWaitFrame = true;


        /** 获取绝对URL
         * path = "Platform/IOS/config.assetbundle"
         * return "file:///xxxx/Platform/IOS/config.assetbundle";
         */
        public static string GetAbsoluteURL(string path)
        {
            if (persistentAssetFileList.Has(path))
            {
                return RootUrlPersistent + path;
            }
            else
            {
                return RootUrlStreaming + path;
            }
        }




        /** 获取平台文件路径
         * path = "{0}/config.assetbundle"
         * return "Platform/IOS/config.assetbundle"
         */
        public static string GetPlatformPath(string path)
        {
            return string.Format(path, Platform.PlatformDirectory);
        }

        /** 获取平台文件URL
         * path="{0}/config.assetbundle"
         * return "file:///xxxx/Platform/IOS/config.assetbundle";
        */
        public static string GetPlatformURL(string path)
        {
            return GetAbsoluteURL(GetPlatformPath(path));
        }

        /** AssetBundleManifest文件路径 */
        public static string ManifestURL
        {
            get
            {
                return GetAbsoluteURL(Platform.ManifestPath);
            }
        }

        #region files.csv

        /** 资源列表文件路径
         * return "files.csv" in "Resources" directory
         */
        public static string AssetlistForResource = "files";

        /** 资源列表文件路径
         * return "StreamingAssets/Platform/IOS/files.csv"
         * return "Res/Platform/IOS/files.csv"
         */
        public static string AssetlistForStreaming
        {
            get
            {
                return GetPlatformURL("{0}/files.csv");
            }
        }

        /** 服务器资源更新列表
         * root =  "http://112.126.75.68:8080/StreamingAssets/"
         * return  "http://112.126.75.68:8080/StreamingAssets/"
         */
        public static string GetServerUpdateAssetlistURL(string root)
        {
            return root + GetPlatformPath("{0}/files.csv");
        }
        #endregion files.csv

        public static string GetServerVersionInfoURL(string root)
        {
            return root + "version_" + Platform.PlatformDirectoryName.ToLower();
        }




        /** 获取配置文件的AssetName
         * filename = "config/skill";
         * return "Assets/Game/ConfigBytes/skill.txt"
         */
        public static string GetConfigAssetName(string filename)
        {
            return filename.ToLower().Replace("config/", ConfigBytesRoot) + BytesExt;
        }


        /** 是否是Config文件
         * filename = "config/skill"; return true;
         * filename = "panel/heropanel" return false;
        */
        public static bool IsConfigFile(string filename)
        {
            return filename.ToLower().IndexOf("config/") == 0;
        }


        public const string ObjType_Sprite         = "Sprite";
        public const string ObjType_GameObject     = "GameObject";

        /** 获取资源Type */
        public static System.Type GetObjType(string objType)
        {
            switch(objType)
            {
                case ObjType_Sprite:
                    return typeof(Sprite);

                case ObjType_GameObject:
                    return typeof(GameObject);

                default:
                    return typeof(System.Object);
            }
        }


    }
}
