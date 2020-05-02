using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        #region Get or Create Settings
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
        #endregion

        #region Getter
        public bool TryGetCheatType(string key, out CheatType cheatType)
        {
            Cheat cheat = _cheats.Where(x => x.id == key).FirstOrDefault();

            if (cheat == null)
            {
                // abort
                cheatType = CheatType.Boolean; // default cheat type
                return false;
            }

            cheatType = cheat.type;
            return true;
        }

        public bool DoCheatKeyExist(string key)
        {
            return _cheats.Select(x => x.id).Contains(key);
        }
        #endregion
    }
}
