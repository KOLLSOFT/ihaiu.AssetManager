﻿using UnityEngine;
using System.Collections;
using System.IO;

namespace Ihaiu.Assets
{
    public class VersionInfo 
    {
        public string version = "0.0.0";

        public string downLoadUrl = "http://www.ihaiu.com/";
        public string updateLoadUrl = "http://www.ihaiu.com/StreamingAssets/";

        #if UNITY_EDITOR
        public static VersionInfo Load(string serverRoot)
        {
            string path = serverRoot + "/" + AssetManagerSetting.VersionInfoName;

            var f = new FileInfo(path);
            if (f.Exists)
            {
                var sr = f.OpenText();
                var str = sr.ReadToEnd();
                sr.Close();

                VersionInfo config = JsonUtility.FromJson<VersionInfo>(str);
                return config;
            }
            else
            {
                return new VersionInfo();
            }
        }


        public void Save(string serverRoot)
        {
            string str = JsonUtility.ToJson(this, true);
            string filesPath = serverRoot + "/" + AssetManagerSetting.VersionInfoName;

            PathUtil.CheckPath(filesPath, true);
            if (File.Exists(filesPath)) File.Delete(filesPath);

            FileStream fs = new FileStream(filesPath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(str);
            sw.Close(); fs.Close();
            UnityEditor.AssetDatabase.Refresh();
            Debug.Log("[VersionInfoJsonGenerator]" + filesPath);
        }
        #endif

        public override string ToString()
        {
            return string.Format("[VersionInfo] version={0}, downLoadUrl={1}, updateLoadUrl={2}", version, downLoadUrl, updateLoadUrl);
        }
    }
}