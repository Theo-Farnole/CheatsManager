using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace TF.Cheats.Editor
{
    [CustomPropertyDrawer(typeof(Cheat))]
    public class CheatDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var id = property.FindPropertyRelative("id");
            var type = property.FindPropertyRelative("type");

            float updatePositionX = position.x;
            float labelWidth = 70f;
            float fieldWidth = (position.width / 2f) - labelWidth;

            EditorGUI.LabelField(new Rect(updatePositionX, position.y, labelWidth, position.height), "Cheat ID");
            updatePositionX += labelWidth;
            id.stringValue = EditorGUI.TextField(new Rect(updatePositionX, position.y, fieldWidth, position.height), id.stringValue);
            updatePositionX += fieldWidth;

            EditorGUI.LabelField(new Rect(updatePositionX, position.y, labelWidth, position.height), "Type");
            updatePositionX += labelWidth;
            type.enumValueIndex = (int)(CheatType)EditorGUI.EnumPopup(new Rect(updatePositionX, position.y, fieldWidth, position.height), (CheatType)type.enumValueIndex);
            updatePositionX += fieldWidth;
        }
    }
}
