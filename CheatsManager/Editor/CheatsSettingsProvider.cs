using System.Collections;
using System.Collections.Generic;
using TF.Cheats;
using UnityEditor;
using UnityEngine;

namespace TF.CheatsEditor
{
    public static class CheatsSettingsProvider
    {
        public static readonly string cheatsSettingsPath = "Project/Cheats";
        public static readonly string cheatsSettingsLabel = "Cheats";

        private static Editor _cheatsSettingsEditor = null;

        private static Editor CheatsSettingsEditor
        {
            get
            {
                if (_cheatsSettingsEditor == null)
                    CreateCheatsSettingsEditor(out _cheatsSettingsEditor);

                return _cheatsSettingsEditor;
            }
        }


        [SettingsProvider]
        public static SettingsProvider CreateCheatsSettingsProvider()
        {
            SettingsProvider provider = new SettingsProvider(cheatsSettingsPath, SettingsScope.Project)
            {
                label = cheatsSettingsLabel,
                guiHandler = DrawCheatsSettingsProvider
            };

            return provider;
        }

        private static void DrawCheatsSettingsProvider(string searchContext)
        {
            CheatsSettingsEditor.OnInspectorGUI();
        }

        private static void CreateCheatsSettingsEditor(out Editor editor)
        {
            // get cheatsSettings
            CheatsSettings cheatsSettings = CheatsSettings.GetOrCreateSettings();
            var serializedObjectCheatsSettings = new SerializedObject(cheatsSettings);

            editor = Editor.CreateEditor(cheatsSettings);
        }
    }
}
