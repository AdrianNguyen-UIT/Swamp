using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;


[CustomEditor(typeof(InteractableObjectConfig))]
public class InteractableObjectEditor : Editor
{
    private SerializedProperty interactType;

    private void OnEnable()
    {
        interactType = serializedObject.FindProperty("interactType");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(interactType);
        serializedObject.ApplyModifiedProperties();

        if (interactType.intValue == (int)InteractType.Grab)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("idleFriction"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("grabbedFriction"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
