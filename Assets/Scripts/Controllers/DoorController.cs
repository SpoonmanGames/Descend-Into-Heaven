using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    public float TransitionSpeed = 1f;
    public bool IsTransitioningRight = true;

    private bool _transition = false;
    

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player" && !_transition) {
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

            float CameraTargetPosition;
            float PlayerTargetPositon;
            Vector3 CameraTargetVector;
            Vector3 PlayerTargetVector;

            if (IsTransitioningRight) {
                CameraTargetPosition = transform.parent.position.x + 2.22f;
                PlayerTargetPositon = player.transform.position.x + 0.45f;

                CameraTargetVector = new Vector3(CameraTargetPosition, transform.parent.position.y, transform.parent.position.z);
                PlayerTargetVector = new Vector3(PlayerTargetPositon, player.transform.position.y, player.transform.position.z);

                cameraScript.MoveCamera(CameraTargetVector, PlayerTargetVector, TransitionSpeed);
                cameraScript.ActivateLimits(CameraTargetPosition, CameraTargetPosition,transform.parent.position.y,transform.parent.position.y);
            } else {
                CameraTargetPosition = transform.parent.position.y - 2.0f;
                PlayerTargetPositon = player.transform.position.y - 0.522f;

                CameraTargetVector = new Vector3(transform.parent.position.x, CameraTargetPosition, transform.parent.position.z);
                PlayerTargetVector = new Vector3(player.transform.position.x, PlayerTargetPositon, player.transform.position.z);

                cameraScript.MoveCamera(CameraTargetVector, PlayerTargetVector, TransitionSpeed);
                cameraScript.ActivateLimits(transform.parent.position.x, transform.parent.position.x, CameraTargetPosition, CameraTargetPosition);
            }
        }
    }
}
