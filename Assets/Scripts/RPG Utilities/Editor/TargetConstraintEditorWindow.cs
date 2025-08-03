using UnityEditor;
using UnityEngine;

public class TargetConstraintEditorWindow : EditorWindow
{
    public static TargetConstraintEditorWindow Instance = null;

    SerializedProperty targeterProperty;

    public static void OpenTargetConstraints(SerializedProperty property)
    {
        if (property.type != "Targeter")
        {
            Debug.LogError("TargetConstraintEditorWindow.OpenTargetConstraints() must be called with a SerializedProperty that points to a type of Targeter!");
            return;
        }
        else if (Instance != null)
        {
            Debug.LogWarning("Target Constraint Editor is already open! Previous Targeter data will be saved and closed.");
            return;
        }

        TargetConstraintEditorWindow window = GetWindow<TargetConstraintEditorWindow>();
        window.titleContent = new GUIContent("Edit Target Selector Constraints");
        Instance = window;
        Instance.targeterProperty = property;
    }

    void OnDestroy()
    {
        Instance.targeterProperty = null;
        Instance = null;
    }
}
