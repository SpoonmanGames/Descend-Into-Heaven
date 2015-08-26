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

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Player.CrossEnemy myScript = (Player.CrossEnemy)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Cross Base Setup", EditorStyles.boldLabel);

        myScript.PlayerController =
            EditorGUILayout.ObjectField("Target Player", myScript.PlayerController, typeof(Player.Player), true) as Player.Player;

        if (myScript.PlayerController == null) {
            EditorGUILayout.HelpBox(_playerControllerError, MessageType.Error);
        }

        myScript.BulletToSpawn =
            EditorGUILayout.ObjectField("Bullet to Spawn", myScript.BulletToSpawn, typeof(GameObject), false) as GameObject;

        if (myScript.BulletToSpawn == null) {
            EditorGUILayout.HelpBox(_bulletToSpawnError, MessageType.Error);
        }

        myScript.DetectionBlock =
            EditorGUILayout.BoundsField("Detection Block Area", myScript.DetectionBlock);

        if (myScript.DetectionBlock.extents.x <= 0 || myScript.DetectionBlock.extents.y <= 0) {
            EditorGUILayout.HelpBox(_rangeDetectionSizeError, MessageType.Error);
        }

        myScript.AttackDelay =
            EditorGUILayout.FloatField("Attack Delay (s)", myScript.AttackDelay);

        myScript.ShootAtTarget =
            EditorGUILayout.Toggle("Shoot At Target?", myScript.ShootAtTarget);

        if (!myScript.ShootAtTarget) {
            myScript.ChoseShootDirections =
                EditorGUILayout.Toggle("ChoseShootDirections", myScript.ChoseShootDirections);

            if (myScript.ChoseShootDirections) {
                myScript.ShootLeft =
                    EditorGUILayout.Toggle("ShootLeft", myScript.ShootLeft);

                myScript.ShootUp =
                    EditorGUILayout.Toggle("ShootUp", myScript.ShootUp);

                myScript.ShootRight =
                    EditorGUILayout.Toggle("ShootRight", myScript.ShootRight);
            } else {
                EditorGUILayout.HelpBox(_choseShootDirectionsInfo, MessageType.Info);
            }
        } else {
            EditorGUILayout.HelpBox(_shootAtTargetInfo, MessageType.Info);
        }
    }
}
