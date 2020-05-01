using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheatsEditorWindow : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Cheats Editor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        CheatsEditorWindow window = (CheatsEditorWindow)EditorWindow.GetWindow(typeof(CheatsEditorWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Cheats Editor", EditorStyles.boldLabel);

        var cheatsSettings = CheatsSettings.GetOrCreateSettings();

        for (int i = 0; i < cheatsSettings.Cheats.Length; i++)
        {
            var cheat = cheatsSettings.Cheats[i];
            DrawCheat(cheat);
        }
    }

    void DrawCheat(Cheat cheat)
    {

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel(cheat.id);

        switch (cheat.type)
        {   
            case CheatType.Boolean:
                bool defaultValue = PlayerPrefs.GetFloat(cheat.id) == 1;

                bool input = EditorGUILayout.Toggle(defaultValue);

                PlayerPrefs.SetFloat(cheat.id, input ? 1 : 0);
                break;

            default:
                Debug.LogError("Missing a value in DrawCheat(). Call your coder.");
                break;
        }
        EditorGUILayout.EndHorizontal();
    }
}
