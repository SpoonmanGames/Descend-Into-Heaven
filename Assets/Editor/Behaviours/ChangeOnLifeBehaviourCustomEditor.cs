using UnityEngine;
using System.Collections;
using UnityEditor;
using Animators.ChangeOn;

[CustomEditor(typeof(ChangeOnLifeBehaviour))]
public class ChangeOnLifeBehaviourCustomEditor : ChangeOnBehaviourCustomEditor {

    protected override void ExtraGUI() {

        SerializedObject myScript = new SerializedObject(target);
        SerializedProperty Life = myScript.FindProperty("Life");

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.PropertyField(Life);

        myScript.ApplyModifiedProperties();
    }
}
