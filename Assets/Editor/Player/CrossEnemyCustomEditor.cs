using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Player.CrossEnemy))]
public class CrossEnemyCustomEditor : PlayerCustomEditor {

    private const string _playerControllerError = "The Script won't work correctly if no Target Player is set.";
    private const string _bulletToSpawnError = "The Script won't spawn anything if the Bullet to Spawn is not set.";
    private const string _rangeDetectionSizeError = "The width and height MUST be bigger than 0. Otherwise it won't detect anything.";
    private const string _choseShootDirectionsInfo = "The three normal direction of spawning the bullet are: left, up and right.";
    private const string _shootAtTargetInfo = "The gameObject will shoot directly to the last detected position of the Target Player when in range.";
    private const string _attackDelayError = "The Attack Delay must be bigger than 0.";
    private const string _shootAlwaysInfo = "The Cross will shoot always with the attack delay given at the directions said.";

    public override void ExtraGUI() {

        SerializedObject myScript = new SerializedObject(target);
        SerializedProperty PlayerController = myScript.FindProperty("PlayerController");
        SerializedProperty BulletToSpawn = myScript.FindProperty("BulletToSpawn");
        SerializedProperty AttackDelay = myScript.FindProperty("AttackDelay");
        SerializedProperty ShootAlways = myScript.FindProperty("ShootAlways");
        SerializedProperty DetectionBlock = myScript.FindProperty("DetectionBlock");
        SerializedProperty ShootRangeBlock = myScript.FindProperty("ShootRangeBlock");
        SerializedProperty ShootAtTarget = myScript.FindProperty("ShootAtTarget");
        SerializedProperty ChooseShootDirections = myScript.FindProperty("ChooseShootDirections");
        SerializedProperty ShootLeft = myScript.FindProperty("ShootLeft");
        SerializedProperty ShootUp = myScript.FindProperty("ShootUp");
        SerializedProperty ShootRight = myScript.FindProperty("ShootRight");

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Cross Base Setup", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(PlayerController);

        if (PlayerController.objectReferenceValue == null) {
            EditorGUILayout.HelpBox(_playerControllerError, MessageType.Error);
        }

        EditorGUILayout.PropertyField(BulletToSpawn);

        if (BulletToSpawn.objectReferenceValue == null) {
            EditorGUILayout.HelpBox(_bulletToSpawnError, MessageType.Error);
        }

        EditorGUILayout.PropertyField(AttackDelay);

        if (AttackDelay.floatValue <= 0) {
            EditorGUILayout.HelpBox(_attackDelayError, MessageType.Error);
        }

        EditorGUILayout.PropertyField(ShootAlways);

        if (ShootAlways.boolValue) {
            EditorGUILayout.HelpBox(_shootAlwaysInfo, MessageType.Info);
        } else {
            EditorGUILayout.PropertyField(DetectionBlock);

            if (DetectionBlock.boundsValue.extents.x <= 0 || DetectionBlock.boundsValue.extents.y <= 0) {
                EditorGUILayout.HelpBox(_rangeDetectionSizeError, MessageType.Error);
            }
        }

        EditorGUILayout.PropertyField(ShootRangeBlock);

        if (ShootRangeBlock.boundsValue.extents.x <= 0 || ShootRangeBlock.boundsValue.extents.y <= 0) {
            EditorGUILayout.HelpBox(_rangeDetectionSizeError, MessageType.Error);
        }
        

        EditorGUILayout.PropertyField(ShootAtTarget);

        if (!ShootAtTarget.boolValue) {
            EditorGUILayout.PropertyField(ChooseShootDirections);

            if (ChooseShootDirections.boolValue) {
                EditorGUILayout.PropertyField(ShootLeft);
                EditorGUILayout.PropertyField(ShootUp);
                EditorGUILayout.PropertyField(ShootRight);
            } else {
                EditorGUILayout.HelpBox(_choseShootDirectionsInfo, MessageType.Info);
            }
        } else {
            EditorGUILayout.HelpBox(_shootAtTargetInfo, MessageType.Info);
        }

        myScript.ApplyModifiedProperties();
    }
}
