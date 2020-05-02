using System.Collections;
using System.Collections.Generic;
using TF.Cheats;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public bool invincibility;

    private void OnEnable()
    {
        CheatsManager.OnCheatChanged += CheatsManager_OnCheatUpdated;
    }

    void Start()
    {
        CheatsManager.SetBool((Cheat)null, false);
        CheatsManager.SetBool((string)null, false);
        CheatsManager.SetBool("hfqsdfk", false);
        CheatsManager.GetBool("hfqsdfk");
        CheatsManager.GetBool("");
        CheatsManager.GetBool((Cheat)null);
        CheatsManager.GetBool((string)null);
    }

    private void CheatsManager_OnCheatUpdated(string key, CheatType cheatType)
    {
        if (key == "invincibility")
        {
            invincibility = CheatsManager.GetBool(key);
        }
    }
}
