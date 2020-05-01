using System.Collections;
using System.Collections.Generic;
using TF.Cheats;
using UnityEditor;
using UnityEngine;

namespace TF.CheatsEditor
{
    public class CheatsEditorWindow : EditorWindow
    {
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

            DrawCheats();
        }

        void DrawCheats()
        {
            var cheatsSettings = CheatsSettings.GetOrCreateSettings();

            if (cheatsSettings.Cheats.Length == 0)
            {
                // warn the user that there is no cheat
                DrawCheatsEmptyLabel();
            }
            else
            {
                EditorGUI.indentLevel++;

                foreach (var cheat in cheatsSettings.Cheats)
                    DrawCheat(cheat);

                EditorGUI.indentLevel--;
            }
        }

        void DrawCheat(Cheat cheat)
        {
            EditorGUILayout.BeginHorizontal();

            // draw prefix
            string prefix = string.IsNullOrWhiteSpace(cheat.id) ? "ERROR: No cheat ID." : cheat.id;
            EditorGUILayout.PrefixLabel(prefix);

            // draw value
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

        void DrawCheatsEmptyLabel()
        {
            GUILayout.Label("There is no cheats in ProjectSettings. Please call your project's administrator.");
        }
    }
}
