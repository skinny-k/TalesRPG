using UnityEditor;
using UnityEngine;

namespace SkinnyUtils
{
    [CustomPropertyDrawer(typeof(RandomRange))]
    public class RandomRangeDrawer : PropertyDrawer
    {
        bool isShown = true;
        bool intOnly = true;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            float svs = EditorGUIUtility.standardVerticalSpacing;
            float line = EditorGUIUtility.singleLineHeight + svs;
            isShown = EditorGUI.BeginFoldoutHeaderGroup(new Rect(position.x, position.y + svs, position.width, line), isShown, label);

            if (isShown)
            {
                var lw = EditorGUIUtility.labelWidth;

                // calculate positions
                Rect minPos = new Rect(position.x, position.y + line, (position.width / 2) - 5, line);
                Rect maxPos = new Rect(position.x + (position.width / 2), position.y + line, position.width / 2, line);
                Rect intPos = new Rect(position.x, position.y + (line * 2), position.width, line);

                // draw properties with modified label size
                EditorGUIUtility.labelWidth = 35;
                EditorGUI.PropertyField(minPos, property.FindPropertyRelative("Min"), new GUIContent("Min:"));
                EditorGUI.PropertyField(maxPos, property.FindPropertyRelative("Max"), new GUIContent("Max:"));
                
                EditorGUIUtility.labelWidth = lw;

                // draw int toggle
                EditorGUI.BeginChangeCheck();
                SerializedProperty intProp = property.FindPropertyRelative("IntOnly");
                intOnly = EditorGUI.ToggleLeft(intPos, "Return Int", intProp.boolValue);
                if (EditorGUI.EndChangeCheck())
                {
                    intProp.boolValue = intOnly;
                }
            }

            EditorGUI.EndFoldoutHeaderGroup();
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * (isShown ? 3 : 1);
        }
    }
}
