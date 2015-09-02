using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DestroyOnTimeBehaviour))]
public class DestroyOnTimeBehaviourCustomEditor : Editor {    

    public override void OnInspectorGUI() {

        SerializedObject myScript = new SerializedObject(target);
        SerializedProperty DependsOnSpeed = myScript.FindProperty("DependsOnSpeed");
        SerializedProperty SpeedVariableName = myScript.FindProperty("SpeedVariableName");
        SerializedProperty Speed = myScript.FindProperty("Speed");
        SerializedProperty WhenDestroy = myScript.FindProperty("WhenDestroy");

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.PropertyField(DependsOnSpeed);

        if (DependsOnSpeed.boolValue) {
            EditorGUILayout.PropertyField(SpeedVariableName);
            EditorGUILayout.Slider(Speed, -1.0f, 1.0f);
            EditorGUILayout.Slider(WhenDestroy, 0.0f, 1.0f);
        }

        myScript.ApplyModifiedProperties();
    }	
}
