using UnityEngine;
using System.Collections;
using UnityEditor;
using Animators.ChangeOn;

[CustomEditor(typeof(ChangeOnAirGroundBehaviour))]
public class ChangeOnAirGroundBehaviourCustomEditor : ChangeOnBehaviourCustomEditor {

    protected override void ExtraGUI() {

        SerializedObject myScript = new SerializedObject(target);
        SerializedProperty GroundVariableName = myScript.FindProperty("GroundVariableName");
        SerializedProperty ChangeOnAir = myScript.FindProperty("ChangeOnAir");
        SerializedProperty ChangeOnGround = myScript.FindProperty("ChangeOnGround");

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.PropertyField(GroundVariableName);
        EditorGUILayout.PropertyField(ChangeOnAir);
        EditorGUILayout.PropertyField(ChangeOnGround);

        myScript.ApplyModifiedProperties();
    }
}