using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace TF.Cheats
{
    public delegate void OnCheatUpdated(string key, CheatType cheatType);

    public static class CheatsManager
    {
        public static readonly string playerPrefsPrefix = "CHEATS_";

        public static event OnCheatUpdated OnCheatChanged;

        #region Setter methods
        public static void SetBool(Cheat cheat, bool value)
        {
            if (cheat == null)
            {
                Debug.LogErrorFormat("Cheat is null. Aborting...");
                return;
            }

            if (cheat.type != CheatType.Boolean)
            {
                Debug.LogErrorFormat("Aborting SetBool() because {0} isn't a cheat of type boolean", cheat.id);
                return;
            }

            SetBool(cheat.id, value);
        }

        public static void SetBool(string key, bool value)
        {
            // do cheat key exist
            if (!CheatsSettings.GetOrCreateSettings().DoCheatKeyExist(key))
            {
                Debug.LogErrorFormat("Key {0} doesn't exist in CheatsSettings. Aborting method.", key);
                return;
            }

            // prevent useless calculations
            if (value == GetBool(key))
                return;

            // turn bool into float
            float floatValue = value ? 1 : 0;

            // PlayerPrefs doesn't have "SetBool" value
            PlayerPrefs.SetFloat(playerPrefsPrefix + key, floatValue);

            // find cheat type of key
            bool cheatTypeFounded = CheatsSettings.GetOrCreateSettings().TryGetCheatType(key, out CheatType cheatType);

            if (!cheatTypeFounded)
            {
                Debug.LogFormat("cheatTypeFounded should always be true. There is something weird in the code. Call your coder please.");
                return;
            }

            OnCheatChanged?.Invoke(key, cheatType);
        }
        #endregion

        #region Getter methods
        public static bool GetBool(Cheat cheat)
        {
            if (cheat == null)
            {
                Debug.LogErrorFormat("Cheat is null. Aborting.");
                return false;
            }

            if (cheat.type != CheatType.Boolean)
            {
                Debug.LogErrorFormat("Can't use GetBool() because {0} isn't a cheat of type boolean. Returning false", cheat.id);
                return false;
            }

            return GetBool(cheat.id);
        }

        public static bool GetBool(string key)
        {
            if (!CheatsSettings.GetOrCreateSettings().DoCheatKeyExist(key))
            {
                Debug.LogErrorFormat("Key {0} doesn't exist in CheatsSettings. Aborting.", key);
                return false;
            }

            float value = PlayerPrefs.GetFloat(playerPrefsPrefix + key);

            // float to boolean
            bool output = (value == 1);

            return output;
        }
        #endregion
    }
}
