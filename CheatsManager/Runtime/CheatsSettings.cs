﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TF.Cheats
{
    public class CheatsSettings : ScriptableObject
    {
        public static readonly string cheatsSettingsFilename = "CheatsSettings";
        public static readonly string cheatsSettingsPath = "Assets/Resources/";

        [SerializeField] private Cheat[] _cheats = new Cheat[0];

        public Cheat[] Cheats { get => _cheats; }

        public static CheatsSettings GetOrCreateSettings()
        {
            CheatsSettings settings = GetSettings();

            if (settings == null)
#if UNITY_EDITOR
                CreateCheatsSettings(out settings, cheatsSettingsPath + cheatsSettingsFilename);
#else            
                Debug.LogErrorFormat("There is no cheats settings. Watch your Resources folder please.");
#endif
            return settings;
        }

        static CheatsSettings GetSettings()
        {
            return Resources.Load<CheatsSettings>(cheatsSettingsFilename);
        }

#if UNITY_EDITOR
        static void CreateCheatsSettings(out CheatsSettings settings, string createAssetPath)
        {
            Debug.Log("<color=magenta>CheatsSettings # </color> Create CheatsSettings file at " + cheatsSettingsPath);

            settings = ScriptableObject.CreateInstance<CheatsSettings>();

            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");

            AssetDatabase.CreateAsset(settings, createAssetPath + ".asset");
            AssetDatabase.SaveAssets();

            return;
        }
#endif
    }
}
