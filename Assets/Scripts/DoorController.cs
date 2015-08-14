using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    bool _transition = false;
    public float TransitionSpeed = 1f;

    void OnTriggerEnter2D(Collider2D collider) {
        if (!_transition) {
            _transition = true;
            SuperMetroidCameraController cameraScript = Camera.main.GetComponent<SuperMetroidCameraController>();
            float targetPosition = transform.parent.position.x + 2.22f;
            Vector3 targetVector = new Vector3(targetPosition, 0f, 0);
            cameraScript.MoveCamera(targetVector, TransitionSpeed);
            cameraScript.ActivateLimits(targetPosition, targetPosition, 0, 0);
        }
    }
}
