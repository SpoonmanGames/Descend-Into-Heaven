using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Player.ProtaController))]
public class ProtaControllerCustomEditor : PlayerCustomEditor {

    public override void ExtraGUI() {

        SerializedObject myScript = new SerializedObject(target);
        SerializedProperty TransitionOut = myScript.FindProperty("TransitionOut");

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Prota Base Setup", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(TransitionOut);

        myScript.ApplyModifiedProperties();
    }
}
