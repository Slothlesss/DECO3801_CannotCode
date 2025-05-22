// Editor/GameManagerEditor.cs
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager manager = (GameManager)target;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Testing Emotional States", EditorStyles.boldLabel);

        // Frustration Dropdown
        Frustration newFrustration = (Frustration)EditorGUILayout.EnumPopup("Frustration", manager.frustration);
        if (newFrustration != manager.frustration)
        {
            manager.SetFrustration(newFrustration);
        }

        // Fatigue Dropdown
        Fatigue newFatigue = (Fatigue)EditorGUILayout.EnumPopup("Fatigue", manager.fatigue);
        if (newFatigue != manager.fatigue)
        {
            manager.SetFatigue(newFatigue);
        }

        // Focus Dropdown
        Focus newFocus = (Focus)EditorGUILayout.EnumPopup("Focus", manager.focus);
        if (newFocus != manager.focus)
        {
            manager.SetFocus(newFocus);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(manager);
        }
    }
}
