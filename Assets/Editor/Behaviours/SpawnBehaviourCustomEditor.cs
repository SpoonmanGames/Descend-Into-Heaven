using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SpawnBehaviour))]
public class SpawnBehaviourCustomEditor : Editor {

    private const string _offsetDueDirectionInfo = "The next value correct the position for mirrored effect of Spawing.";
    private const string _spawnError = "An object must be assigned for this script to work.";

    public override void OnInspectorGUI() {

        SerializedObject myScript = new SerializedObject(target);
        SerializedProperty SpawnGameObject = myScript.FindProperty("SpawnGameObject");
        SerializedProperty SpawnTime = myScript.FindProperty("SpawnTime");
        SerializedProperty DependOnThisState = myScript.FindProperty("DependOnThisState");
        SerializedProperty DestroyTime = myScript.FindProperty("DestroyTime");
        SerializedProperty OffsetDueDirection = myScript.FindProperty("OffsetDueDirection");

        EditorGUILayout.Space(); EditorGUILayout.Space();
        SpawnGameObject.objectReferenceValue = 
            EditorGUILayout.ObjectField(new GUIContent("Spawn Game Object"), SpawnGameObject.objectReferenceValue, typeof(GameObject), true) as GameObject;

        if (SpawnGameObject.objectReferenceValue == null) {
            EditorGUILayout.HelpBox(_spawnError, MessageType.Error);
        }

        EditorGUILayout.Slider(SpawnTime, 0.0f, 1.0f, new GUIContent("Spawn Time (%)"));

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.PropertyField(DependOnThisState);

        if (DependOnThisState.boolValue) {
            EditorGUILayout.Slider(DestroyTime, 0.0f, 1.0f, new GUIContent("Destroy Time (%)"));
        }

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Base Setup", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox(_offsetDueDirectionInfo, MessageType.Info);

        EditorGUILayout.PropertyField(OffsetDueDirection);

        myScript.ApplyModifiedProperties();
    }	
}
