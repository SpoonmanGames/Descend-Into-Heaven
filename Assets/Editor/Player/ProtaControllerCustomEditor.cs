using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Player.ProtaController))]
public class ProtaControllerCustomEditor : PlayerCustomEditor {

    private const string _protaDefaultInfo = "The Prota Controller doesn't have especial fields or properties.";

    public override void ExtraGUI() {

        SerializedObject myScript = new SerializedObject(target);

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Prota Base Setup", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox(_protaDefaultInfo, MessageType.Info);

        myScript.ApplyModifiedProperties();
    }
}
