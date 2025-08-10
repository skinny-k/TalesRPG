using UnityEditor;
using UnityEngine;
using SkinnyUtils;
using System;
using System.Text.RegularExpressions;

[CustomPropertyDrawer(typeof(Targeter))]
public class TargeterDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        GUI.Label(new Rect(position.x, position.y, position.width, SkinnyGUI.lineHeight), label, EditorStyles.boldLabel);
        var lw = EditorGUIUtility.labelWidth;

        // calculate positions
        Rect targetPos = new Rect(position.x, position.y + SkinnyGUI.lineHeight, (position.width / 2) - 5, SkinnyGUI.lineHeight);
        Rect selectPos = new Rect(position.x, position.y + (SkinnyGUI.lineHeight * 2), position.width, SkinnyGUI.lineHeight);
        Rect detailPos = new Rect(position.x + (position.width / 2), position.y + SkinnyGUI.lineHeight, position.width / 2, SkinnyGUI.lineHeight);
        Rect conListPos = new Rect(position.x + 62, position.y + (SkinnyGUI.lineHeight * 3), position.width - 62, SkinnyGUI.lineHeight);

        // draw enums with modified label size
        EditorGUIUtility.labelWidth = 60;
        EditorGUI.PropertyField(targetPos, property.FindPropertyRelative("Target"), new GUIContent("Target:"));
        EditorGUI.PropertyField(selectPos, property.FindPropertyRelative("Selector"), new GUIContent("Selector:"));

        // draw details with modified label size based on target type selection
        switch ((Targeter.TargetType)(property.FindPropertyRelative("Target").enumValueIndex))
        {
            case Targeter.TargetType.Self:
                break;
            case Targeter.TargetType.Other:
            case Targeter.TargetType.Entity:
                EditorGUIUtility.labelWidth = 85;
                EditorGUI.PropertyField(detailPos, property.FindPropertyRelative("NumTargets"), new GUIContent("Num Targets:"));
                break;
            case Targeter.TargetType.Zone:
                EditorGUIUtility.labelWidth = 50;
                EditorGUI.PropertyField(detailPos, property.FindPropertyRelative("Zones"), new GUIContent("Zones:"));
                break;
        }

        // draw constraint list with modified label size
        if (GUI.Button(conListPos, "Modify Selector Constraints"))
        {
            TargetConstraintEditorWindow.OpenTargetConstraints(property);
        }
        
        EditorGUIUtility.labelWidth = lw;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return SkinnyGUI.lineHeight * 4f;
    }
    
    [CustomPropertyDrawer(typeof(Targeter.TargetConstraint))]
    public class TargetConstraintDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect labelPos = new Rect(position.x, position.y, position.width, SkinnyGUI.lineHeight);
            
            if ((Targeter.TargetConstraint.TC_Type)(property.FindPropertyRelative("t").enumValueIndex) != Targeter.TargetConstraint.TC_Type.None)
                EditorGUI.LabelField(labelPos, Enum.GetName(typeof(Targeter.TargetConstraint.TC_Type), (Targeter.TargetConstraint.TC_Type)(property.FindPropertyRelative("t").enumValueIndex)) + " Constraint");
            
            UnityEngine.Object targetObject = property.serializedObject.targetObject;
            Type t = targetObject.GetType();
            var field = t.GetField("_targetInfo");
            if (field != null)
            {
                string iMatch = Regex.Match(property.propertyPath, "\\[\\d+\\]", RegexOptions.RightToLeft).Value;
                Targeter.TargetConstraint tc = ((Targeter)field.GetValue(targetObject)).Constraints[Int32.Parse(iMatch.Substring(1, iMatch.Length - 2))];
                Rect propPos = new Rect(position.x, position.y + SkinnyGUI.lineHeight, position.width, SkinnyGUI.lineHeight);
                switch (tc.t)
                {
                    case Targeter.TargetConstraint.TC_Type.Affiliation:
                        // Targeter.TC_Affiliation tc_aff = (Targeter.TC_Affiliation)tc;
                        // tc_aff._affiliations = (TeamAffiliation)EditorGUI.EnumFlagsField(propPos, tc_aff._affiliations);
                        break;
                    case Targeter.TargetConstraint.TC_Type.Effect:
                        // Targeter.TC_Effect tc_eff = (Targeter.TC_Effect)tc;
                        // propPos = new Rect(position.x, position.y + SkinnyGUI.lineHeight, position.width / 2, SkinnyGUI.lineHeight);
                        // Rect countPos = new Rect(position.x + (position.width / 2) + 5f, position.y + SkinnyGUI.lineHeight, (position.width / 2) - 5f, SkinnyGUI.lineHeight);
                        // tc_eff._effect = (EffectType)EditorGUI.EnumPopup(propPos, tc_eff._effect);
                        // tc_eff._count = EditorGUI.IntField(countPos, tc_eff._count);
                        break;
                    case Targeter.TargetConstraint.TC_Type.Zone:
                        // Targeter.TC_Zone tc_zon = (Targeter.TC_Zone)tc;
                        // tc_zon._zones = (CombatZone)EditorGUI.EnumFlagsField(propPos, tc_zon._zones);
                        break;
                    default:
                        propPos = new Rect(position.x, position.y, position.width, SkinnyGUI.lineHeight);
                        EditorGUI.LabelField(propPos, "Bad type! Delete this element.");
                        break;
                }
            }
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            switch ((Targeter.TargetConstraint.TC_Type)(property.FindPropertyRelative("t").enumValueIndex))
            {
                case Targeter.TargetConstraint.TC_Type.None:
                    return SkinnyGUI.lineHeight;
                default:
                    return SkinnyGUI.lineHeight * 2f;
            }
        }
    }
}
