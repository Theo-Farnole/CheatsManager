using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace TF.Cheats
{
    public class DoubleJump : MonoBehaviour
    {
        private const string cheatKey = "double_jump";
        public bool doubleJump;

        void OnEnable()
        {
            CheatsManager.OnCheatChanged += CheatsManager_OnCheatUpdated;
            doubleJump = CheatsManager.GetBool(cheatKey);
        }

        private void CheatsManager_OnCheatUpdated(string key, CheatType cheatType)
        {
            if (key != cheatKey)
                return;
            
            doubleJump = CheatsManager.GetBool(key);
        }
    }
}
