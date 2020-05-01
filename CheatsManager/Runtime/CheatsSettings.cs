using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TF.Cheats
{
    public class CheatsSettings : ScriptableObject
    {
        public static readonly string cheatsSettingsPath = "Assets/Editor/CheatsSettings.asset";

        [SerializeField] private Cheat[] _cheats = new Cheat[0];

        public Cheat[] Cheats { get => _cheats; }

        public static CheatsSettings GetOrCreateSettings()
        {
            CheatsSettings settings = GetSettings();

            if (settings == null)
                settings = CreateCheatsSettings(out settings, cheatsSettingsPath);

            return settings;
        }

        static CheatsSettings GetSettings()
        {
            return AssetDatabase.LoadAssetAtPath<CheatsSettings>(cheatsSettingsPath);
        }

        static CheatsSettings CreateCheatsSettings(out CheatsSettings settings, string createAssetPath)
        {
            settings = ScriptableObject.CreateInstance<CheatsSettings>();
            AssetDatabase.CreateAsset(settings, createAssetPath);
            AssetDatabase.SaveAssets();

            return settings;
        }
    }
}
