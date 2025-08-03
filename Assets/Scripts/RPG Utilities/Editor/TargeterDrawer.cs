using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Targeter))]
public class TargeterDrawer : PropertyDrawer
{
    bool isShown = true;

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
            Rect targetPos = new Rect(position.x, position.y + line, (position.width / 2) - 5, line);
            Rect selectPos = new Rect(position.x, position.y + (line * 2), position.width, line);
            Rect detailPos = new Rect(position.x + (position.width / 2), position.y + line, position.width / 2, line);
            Rect conListPos = new Rect(position.x + 62, position.y + (line * 3), position.width - 62, line);

            // draw enums with modified label size
            EditorGUIUtility.labelWidth = 60;
            EditorGUI.PropertyField(targetPos, property.FindPropertyRelative("Target"), new GUIContent("Target:"));
            EditorGUI.PropertyField(selectPos, property.FindPropertyRelative("Selector"), new GUIContent("Selector:"));

            // draw details with modified label size based on target type selection
            switch (property.FindPropertyRelative("Target").enumValueIndex)
            {
                case (int)Targeter.TargetType.Self:
                    break;
                case (int)Targeter.TargetType.Other:
                case (int)Targeter.TargetType.Entity:
                    EditorGUIUtility.labelWidth = 85;
                    EditorGUI.PropertyField(detailPos, property.FindPropertyRelative("NumTargets"), new GUIContent("Num Targets:"));
                    break;
                case (int)Targeter.TargetType.Zone:
                    EditorGUIUtility.labelWidth = 50;
                    EditorGUI.PropertyField(detailPos, property.FindPropertyRelative("Zones"), new GUIContent("Zones:"));
                    break;
            }

            // draw constraint list with modified label size
            // EditorGUI.PropertyField(conListPos, property.FindPropertyRelative("Constraints"), new GUIContent("Constraints"));
            if (GUI.Button(conListPos, "Modify Selector Constraints"))
            {
                TargetConstraintEditorWindow.OpenTargetConstraints(property);
            }

            EditorGUIUtility.labelWidth = lw;
        }

        EditorGUI.EndFoldoutHeaderGroup();
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * (isShown ? 4.5f : 1);
    }
}
