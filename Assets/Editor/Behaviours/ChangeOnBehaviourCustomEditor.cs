using UnityEngine;
using System.Collections;
using UnityEditor;
using Animators.ChangeOn;

[CustomEditor(typeof(ChangeOnBehaviour))]
public abstract class ChangeOnBehaviourCustomEditor : Editor {

    private const string _changeOnInfo = "Never use ChangeOnBehaviour (Script) alone, you must inheritage from another class.";

    public override void OnInspectorGUI() {

        SerializedObject myScript = new SerializedObject(target);
        SerializedProperty IsPlayer = myScript.FindProperty("IsPlayer");
        SerializedProperty NextPlayerState = myScript.FindProperty("NextPlayerState");
        SerializedProperty StateVariableName = myScript.FindProperty("StateVariableName");
        SerializedProperty NextStateValue = myScript.FindProperty("NextStateValue");
        SerializedProperty NextStateName = myScript.FindProperty("NextStateName");
        SerializedProperty StartingPosition = myScript.FindProperty("StartingPosition");
        SerializedProperty SpeedVariableName = myScript.FindProperty("SpeedVariableName");
        SerializedProperty StartingSpeedValue = myScript.FindProperty("StartingSpeedValue");

        EditorGUILayout.HelpBox(_changeOnInfo, MessageType.None);

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.PropertyField(IsPlayer);

        if (IsPlayer.boolValue) {
            EditorGUILayout.Space(); EditorGUILayout.Space();
            EditorGUILayout.PropertyField(NextPlayerState);
        } else {
            EditorGUILayout.Space(); EditorGUILayout.Space();
            EditorGUILayout.PropertyField(StateVariableName);
            EditorGUILayout.PropertyField(NextStateValue);
        }

        EditorGUILayout.PropertyField(NextStateName);
        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.Slider(StartingPosition, 0.0f, 1.0f, new GUIContent("Starting Position (%)"));
        EditorGUILayout.PropertyField(SpeedVariableName);
        EditorGUILayout.PropertyField(StartingSpeedValue);

        myScript.ApplyModifiedProperties();

        ExtraGUI();
    }

    abstract protected void ExtraGUI();
}
