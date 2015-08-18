using UnityEngine;
using System.Collections;

public enum TransitionDirection {
    Down,
    Left,
    Right
}

public class DoorController : MonoBehaviour {

    public float TransitionSpeed = 1f;
    public TransitionDirection TransitionDirection = TransitionDirection.Right;
    public bool IsTransitioningToScene = false;
    public string SceneName = string.Empty;
    public GameObject TransitionEffect;
    [Space(10)]
    public bool UsePathBlocker = true;
    public GameObject PathBlocker;
    public Color ColorOfPathBlocker;
    public float TimeOffSet = 0.0f;
    public float TargetXPosition = 0.0f;
    public float TargetYPosition = 0.0f;

    private bool _transition = false;
    private GameObject _spawedPathBloquer;
    private bool _isSpawned = false;
    private GameObject _transitionToLevel;
    private bool _isTransitioningOut = false;
    private float _deadTimeCounter = 0.0f;
    private float _deadTime = 1.0f;

    void Update() {
        if (_isTransitioningOut) {
            _deadTimeCounter += Time.deltaTime;

            if (_deadTimeCounter >= _deadTime) {
                Application.LoadLevel(SceneName);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player" && !_transition) {
            _transition = true;

            SuperMetroidCameraController cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SuperMetroidCameraController>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player == null) {
                Debug.LogError("Player not found: Your main Player must have the Tag Player");
                return;
            }

            if (cameraScript == null) {
                Debug.LogError("Camera Script not found, please check if it is active");
                return;
            }

            if (!IsTransitioningToScene) {
                float CameraTargetPosition;
                float PlayerTargetPositon;
                Vector3 CameraTargetVector;
                Vector3 PlayerTargetVector;

                if (this.TransitionDirection == TransitionDirection.Right) {
                    CameraTargetPosition = transform.parent.position.x + 2.22f;
                    PlayerTargetPositon = player.transform.position.x + 0.45f;

                    CameraTargetVector = new Vector3(CameraTargetPosition, transform.parent.position.y, transform.parent.position.z);
                    PlayerTargetVector = new Vector3(PlayerTargetPositon, player.transform.position.y, player.transform.position.z);

                    cameraScript.MoveCamera(CameraTargetVector, PlayerTargetVector, TransitionSpeed);
                    cameraScript.ActivateLimits(CameraTargetPosition, CameraTargetPosition, transform.parent.position.y, transform.parent.position.y);
                } else if (this.TransitionDirection == TransitionDirection.Down) {
                    CameraTargetPosition = transform.parent.position.y - 2.0f;
                    PlayerTargetPositon = player.transform.position.y - 0.522f;

                    CameraTargetVector = new Vector3(transform.parent.position.x, CameraTargetPosition, transform.parent.position.z);
                    PlayerTargetVector = new Vector3(player.transform.position.x, PlayerTargetPositon, player.transform.position.z);

                    cameraScript.MoveCamera(CameraTargetVector, PlayerTargetVector, TransitionSpeed);
                    cameraScript.ActivateLimits(transform.parent.position.x, transform.parent.position.x, CameraTargetPosition, CameraTargetPosition);
                } else if (this.TransitionDirection == TransitionDirection.Left) {
                    CameraTargetPosition = transform.parent.position.x - 2.22f;
                    PlayerTargetPositon = player.transform.position.x - 0.45f;

                    CameraTargetVector = new Vector3(CameraTargetPosition, transform.parent.position.y, transform.parent.position.z);
                    PlayerTargetVector = new Vector3(PlayerTargetPositon, player.transform.position.y, player.transform.position.z);

                    cameraScript.MoveCamera(CameraTargetVector, PlayerTargetVector, TransitionSpeed);
                    cameraScript.ActivateLimits(CameraTargetPosition, CameraTargetPosition, transform.parent.position.y, transform.parent.position.y);
                }
            } else {
                if (!_isTransitioningOut) {
                    _isTransitioningOut = true;
                    _transitionToLevel = Instantiate(TransitionEffect, this.transform.parent.position, this.transform.parent.rotation) as GameObject;
                    _transitionToLevel.GetComponent<Animator>().SetFloat("Speed", -1.0f);
                    _transitionToLevel.GetComponent<Animator>().Play("LoadingTransition", 0, 1.0f);
                }
            }
        }

        if (UsePathBlocker && collider.tag == "Player" && _transition && !_isSpawned) {
            _isSpawned = true;
            Vector3 spawPositon = Vector3.zero;
            Quaternion spawnQuaternion = Quaternion.identity;
            BulletController bulletController = PathBlocker.GetComponent<BulletController>();
            bulletController.Tiempo = TransitionSpeed - TimeOffSet;

            switch (TransitionDirection) {
                case TransitionDirection.Down:
                    spawPositon = this.transform.position + Vector3.up * 2.5f;
                    spawnQuaternion = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
                    bulletController.Direccion = -1;
                    bulletController.TargetPosition = new Vector2(this.transform.position.x, this.transform.position.y + TargetYPosition);
                    break;
                case TransitionDirection.Left:
                    spawPositon = this.transform.position + Vector3.right * 2.5f;
                    spawnQuaternion = new Quaternion(0.0f, 0.0f, -1.0f, 1.0f);
                    bulletController.Direccion = -1;                    
                    bulletController.TargetPosition = new Vector2(this.transform.position.x + TargetXPosition, this.transform.position.y);
                    break;
                case TransitionDirection.Right:
                    spawPositon = this.transform.position - Vector3.right * 2.5f;
                    spawnQuaternion = new Quaternion(0.0f, 0.0f, 1.0f, 1.0f);
                    bulletController.Direccion = 1;
                    bulletController.TargetPosition = new Vector2(this.transform.position.x + TargetXPosition, this.transform.position.y);
                    break;
            }

            _spawedPathBloquer = Instantiate(PathBlocker, spawPositon, spawnQuaternion) as GameObject;
            _spawedPathBloquer.transform.localScale = new Vector3(3.0f, 3.0f, 1.0f);
            _spawedPathBloquer.GetComponent<SpriteRenderer>().color = ColorOfPathBlocker;
        }

    }
}
