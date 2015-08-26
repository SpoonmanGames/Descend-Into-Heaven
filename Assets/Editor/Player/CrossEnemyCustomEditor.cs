using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Player.CrossEnemy))]
public class CrossEnemyCustomEditor : PlayerCustomEditor {

    private const string _playerControllerError = "The Script won't work correctly if no Target Player is set.";
    private const string _bulletToSpawnError = "The Script won't spawn anything if the Bullet to Spawn is not set.";
    private const string _rangeDetectionSizeError = "This is the range from where the Cross will detect the target, the widht and height MUST be bigger than 0.";
    private const string _choseShootDirectionsInfo = "The three normal direction of spawning the bullet are: left, up and right.";
    private const string _shootAtTargetInfo = "The gameObject will shoot directly to the last detected position of the Target Player when in range.";
    private const string _attackDelayError = "The Attack Delay must be bigger than 0.";

    public override void ExtraGUI() {

        SerializedObject myScript = new SerializedObject(target);
        SerializedProperty PlayerController = myScript.FindProperty("PlayerController");
        SerializedProperty BulletToSpawn = myScript.FindProperty("BulletToSpawn");
        SerializedProperty DetectionBlock = myScript.FindProperty("DetectionBlock");
        SerializedProperty AttackDelay = myScript.FindProperty("AttackDelay");
        SerializedProperty ShootAtTarget = myScript.FindProperty("ShootAtTarget");
        SerializedProperty ChooseShootDirections = myScript.FindProperty("ChooseShootDirections");
        SerializedProperty ShootLeft = myScript.FindProperty("ShootLeft");
        SerializedProperty ShootUp = myScript.FindProperty("ShootUp");
        SerializedProperty ShootRight = myScript.FindProperty("ShootRight");

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Cross Base Setup", EditorStyles.boldLabel);

        PlayerController.objectReferenceValue =
            EditorGUILayout.ObjectField("Target Player", PlayerController.objectReferenceValue, typeof(Player.Player), true) as Player.Player;

        if (PlayerController.objectReferenceValue == null) {
            EditorGUILayout.HelpBox(_playerControllerError, MessageType.Error);
        }

        BulletToSpawn.objectReferenceValue =
            EditorGUILayout.ObjectField("Bullet to Spawn", BulletToSpawn.objectReferenceValue, typeof(GameObject), false) as GameObject;

        if (BulletToSpawn.objectReferenceValue == null) {
            EditorGUILayout.HelpBox(_bulletToSpawnError, MessageType.Error);
        }

        DetectionBlock.boundsValue =
            EditorGUILayout.BoundsField("Detection Block Area", DetectionBlock.boundsValue);

        if (DetectionBlock.boundsValue.extents.x <= 0 || DetectionBlock.boundsValue.extents.y <= 0) {
            EditorGUILayout.HelpBox(_rangeDetectionSizeError, MessageType.Error);
        }

        AttackDelay.floatValue =
            EditorGUILayout.FloatField("Attack Delay (s)", AttackDelay.floatValue);

        if (AttackDelay.floatValue <= 0) {
            EditorGUILayout.HelpBox(_attackDelayError, MessageType.Error);
        }

        ShootAtTarget.boolValue =
            EditorGUILayout.Toggle("Shoot At Target?", ShootAtTarget.boolValue);

        if (!ShootAtTarget.boolValue) {
            ChooseShootDirections.boolValue =
                EditorGUILayout.Toggle("Choose Shoot Directions", ChooseShootDirections.boolValue);

            if (ChooseShootDirections.boolValue) {
                ShootLeft.boolValue =
                    EditorGUILayout.Toggle("Shoot Left", ShootLeft.boolValue);

                ShootUp.boolValue =
                    EditorGUILayout.Toggle("Shoot Up", ShootUp.boolValue);

                ShootRight.boolValue =
                    EditorGUILayout.Toggle("Shoot Right", ShootRight.boolValue);
            } else {
                EditorGUILayout.HelpBox(_choseShootDirectionsInfo, MessageType.Info);
            }
        } else {
            EditorGUILayout.HelpBox(_shootAtTargetInfo, MessageType.Info);
        }
    }
}
