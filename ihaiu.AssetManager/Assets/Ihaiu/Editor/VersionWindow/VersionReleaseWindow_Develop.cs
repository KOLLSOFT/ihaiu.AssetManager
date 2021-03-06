﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using Games;


namespace Ihaiu.Assets
{
    public partial class VersionReleaseWindow
    {

        /** 开发 */
        void OnGUI_Develop()
        {
            HGUILayout.BeginCenterHorizontal();
            if (GUILayout.Button("生成版本信息", GUILayout.MinHeight(50), GUILayout.MaxWidth(200)))
            {
                if (currentDvancedSettingData.GetValue(DvancedSettingType.GameConstConfig))
                {
                    GameConstConfig config = GameConstConfig.Load();
                    config.DevelopMode      = true;
                    config.TestVersionMode  = false;
                    config.Save();
                }


                if (currentDvancedSettingData.GetValue(DvancedSettingType.Clear_AssetBundleName))
                {
                    AssetBundleEditor.ClearAssetBundleNames();
                    AssetDatabase.RemoveUnusedAssetBundleNames();
                }


                if (currentDvancedSettingData.GetValue(DvancedSettingType.Set_AssetBundleName))
                {
                    AssetBundleEditor.SetNames();
                }

                if (currentDvancedSettingData.GetValue(DvancedSettingType.GeneratorStreamingAssetsFilesCSV))
                {
                    FilesCsvForStreamingAssets.Generator(true);
                }

                if (currentDvancedSettingData.GetValue(DvancedSettingType.GeneratorResourcesFilesCSV))
                {
                    FilesCsvForResources.Generator();
                }
            }
            HGUILayout.EndCenterHorizontal();

        }


    }
}
