using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DisableCollitionsBehaviour))]
public class DisableCollitionsBehaviourCustomEditor : Editor {

    public override void OnInspectorGUI() {
        SerializedObject myScript = new SerializedObject(target);
        SerializedProperty DisableGravity = myScript.FindProperty("DisableGravity");

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.PropertyField(DisableGravity);

        myScript.ApplyModifiedProperties();
    }	
}