using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using SkinnyUtils;
using System;
using System.Collections;
using System.Collections.Generic;

public class TargetConstraintEditorWindow : EditorWindow
{
    public static TargetConstraintEditorWindow Instance = null;

    SerializedProperty editing;
    List<Targeter.TargetConstraint> editingList;
    Targeter.TargetConstraint.TC_Type addType = Targeter.TargetConstraint.TC_Type.None;
    
    string[] constraintNames = Enum.GetNames(typeof(Targeter.TargetConstraint.TC_Type));
    GenericMenu.MenuFunction2 selectCallback;

    public static void OpenTargetConstraints(SerializedProperty property)
    {
        if (!(property.type == "Targeter"))
        {
            Debug.LogError("Target Constraint Editor must be passed in a Serialized Property that points to a Targeter object.");
            return;
        }
        if (Instance != null)
        {
            Debug.LogWarning("Target Constraint Editor is already open! Previous Targeter data has been unloaded.");
        }

        TargetConstraintEditorWindow window = GetWindow<TargetConstraintEditorWindow>();
        window.titleContent = new GUIContent("Edit Target Selector Constraints");
        Instance = window;
        
        UnityEngine.Object targetObject = property.serializedObject.targetObject;
        Type t = targetObject.GetType();
        var field = t.GetField(property.propertyPath);
        if (field != null)
        {
            Instance.editingList = ((Targeter)field.GetValue(targetObject)).Constraints;
        }
        
        Instance.editing = property.FindPropertyRelative("Constraints");
        Instance.editingList.Clear();
        Instance.selectCallback = Instance.OnSelectType;
    }
    
    public static void ApplyModifiedProperties()
    {
        if (Instance != null)
        {
            Instance.editing.serializedObject.ApplyModifiedProperties();
        }
    }
    
    void OnGUI()
    {
        if (editing != null)
        {
            float padding = 5f;
            Rect basePos = new Rect(padding, padding, position.width - (padding * 2), SkinnyGUI.lineHeight);
            
            string label = addType == Targeter.TargetConstraint.TC_Type.None ? "Select a Constraint Type" : "Add " + constraintNames[(int)addType] + " Constraint";
            if (EditorGUI.LargeSplitButtonWithDropdownList(new GUIContent(label), constraintNames, selectCallback, addType == Targeter.TargetConstraint.TC_Type.None))
            {
                Targeter.TargetConstraint tc = null;
                switch (addType)
                {
                    case Targeter.TargetConstraint.TC_Type.None:
                        Debug.LogWarning("No Target Constraint type set!");
                        break;
                    case Targeter.TargetConstraint.TC_Type.Affiliation:
                        tc = new Targeter.TC_Affiliation();
                        break;
                    case Targeter.TargetConstraint.TC_Type.Effect:
                        tc = new Targeter.TC_Effect();
                        break;
                    case Targeter.TargetConstraint.TC_Type.Zone:    
                        tc = new Targeter.TC_Zone();
                        break;
                }
                if (tc != null)
                {
                    editingList.Add(tc);
                }
            }
            EditorGUI.PropertyField(new Rect(basePos.x, basePos.y + SkinnyGUI.lineHeight + padding, basePos.width, SkinnyGUI.lineHeight), editing);
            
            ApplyModifiedProperties();
        }
        else
        {
            EditorGUILayout.LabelField("There is no Target Selector Constraint list currently open!");
        }
    }
    
    void OnSelectType(System.Object type)
    {
        addType = (Targeter.TargetConstraint.TC_Type)type;
    }

    void OnDestroy()
    {
        if (Instance != null)
        {
            Instance.editing = null;
            Instance.editingList = null;
            Instance.addType = Targeter.TargetConstraint.TC_Type.None;
            Instance.selectCallback = null;
            Instance = null;
        }
    }
}
