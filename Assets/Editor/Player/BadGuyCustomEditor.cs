using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Player.BadGuy))]
public class BadGuyCustomEditor : PlayerCustomEditor {

    private const string _protaControllerError = "The Script won't work correctly if no ProtaController Player is set.";
    private const string _rangeDetectionSizeError = "The width and height MUST be bigger than 0. Otherwise it won't detect anything.";

    public override void ExtraGUI() {

        SerializedObject myScript = new SerializedObject(target);
        SerializedProperty ProtaController = myScript.FindProperty("ProtaController");
        SerializedProperty DetectionBlock = myScript.FindProperty("DetectionBlock");
        SerializedProperty UseHightDetection = myScript.FindProperty("UseHightDetection");
        SerializedProperty AlwaysFollow = myScript.FindProperty("AlwaysFollow");
        SerializedProperty RangeOfAttack = myScript.FindProperty("RangeOfAttack");
        SerializedProperty DelayToAttack = myScript.FindProperty("DelayToAttack");
        SerializedProperty DelayBeforeAttack = myScript.FindProperty("DelayBeforeAttack");

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("BadGuy Base Setup", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(ProtaController);

        if (ProtaController.objectReferenceValue == null) {
            EditorGUILayout.HelpBox(_protaControllerError, MessageType.Error);
        }

        EditorGUILayout.PropertyField(DetectionBlock);

        if (DetectionBlock.boundsValue.extents.x <= 0 || DetectionBlock.boundsValue.extents.y <= 0) {
            EditorGUILayout.HelpBox(_rangeDetectionSizeError, MessageType.Error);
        }

        EditorGUILayout.PropertyField(UseHightDetection);
        EditorGUILayout.PropertyField(AlwaysFollow);
        EditorGUILayout.PropertyField(RangeOfAttack);

        if (RangeOfAttack.floatValue < 0.0f) {
            RangeOfAttack.floatValue = 0.0f;
        }

        EditorGUILayout.PropertyField(DelayToAttack);

        if (DelayToAttack.floatValue < 0.0f) {
            DelayToAttack.floatValue = 0.0f;
        }

        EditorGUILayout.PropertyField(DelayBeforeAttack);

        myScript.ApplyModifiedProperties();
    }
}
