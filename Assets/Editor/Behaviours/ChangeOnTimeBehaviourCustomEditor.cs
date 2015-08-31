using UnityEngine;
using System.Collections;
using UnityEditor;
using Animators.ChangeOn;

[CustomEditor(typeof(ChangeOnTimeBehaviour))]
public class ChangeOnTimeBehaviourCustomEditor : ChangeOnBehaviourCustomEditor {

    protected override void ExtraGUI() {

        SerializedObject myScript = new SerializedObject(target);
        SerializedProperty WhenChange = myScript.FindProperty("WhenChange");
        SerializedProperty WhenSpeedIs = myScript.FindProperty("WhenSpeedIs");

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.Slider(WhenChange, 0.0f, 1.0f);
        EditorGUILayout.Slider(WhenSpeedIs, -1.0f, 1.0f);

        myScript.ApplyModifiedProperties();

    }
}
