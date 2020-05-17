using System.Collections;
using System.Collections.Generic;
using TF.Cheats;
using UnityEditor;
using UnityEngine;

namespace TF.CheatsEditor
{
    public class CheatsEditorWindow : EditorWindow
    {
        #region Methods
        [MenuItem("Window/Cheats Editor")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            CheatsEditorWindow window = (CheatsEditorWindow)GetWindow(typeof(CheatsEditorWindow));
            window.Show();
        }

        #region EditorWindow Callbacks
        void OnEnable()
        {
            CheatsManager.OnCheatChanged += CheatsManager_OnCheatChanged;   
        }

        void OnDisable()
        {
            CheatsManager.OnCheatChanged -= CheatsManager_OnCheatChanged;
        }

        void OnGUI()
        {
            GUILayout.Label("Cheats Editor", EditorStyles.boldLabel);

            DrawCheats();
        }
        #endregion

        #region Events Handlers
        private void CheatsManager_OnCheatChanged(string key, CheatType cheatType)
        {
            Repaint();
        }
        #endregion

        #region Draw Methods
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
                    bool input = EditorGUILayout.Toggle(CheatsManager.GetBool(cheat));
                    CheatsManager.SetBool(cheat, input);
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
        #endregion
        #endregion
    }
}
