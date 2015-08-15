using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    bool _transition = false;
    public float TransitionSpeed = 1f;

    void OnTriggerEnter2D(Collider2D collider) {
        if (!_transition) {
            _transition = true;

            SuperMetroidCameraController cameraScript = Camera.main.GetComponent<SuperMetroidCameraController>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player == null) {
                Debug.LogError("Player not found: Your main Player must have the Tag Player");
                return;
            }

            if (cameraScript == null) {
                Debug.LogError("Camera Script not found, please check if it is active");
                return;
            }

            float CameraTargetPosition = transform.parent.position.x + 2.22f;
            float PlayerTargetPositon = player.transform.position.x + 0.45f;

            Vector3 CameraTargetVector = new Vector3(CameraTargetPosition, transform.parent.position.y, 0);
            Vector3 PlayerTargetVector = new Vector3(PlayerTargetPositon, player.transform.position.y, 0);

            cameraScript.MoveCamera(CameraTargetVector, PlayerTargetVector, TransitionSpeed);
            cameraScript.ActivateLimits(CameraTargetPosition, CameraTargetPosition, 0, 0);
        }
    }
}
