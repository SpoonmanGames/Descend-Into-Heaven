using UnityEngine;
using System.Collections;

public enum PlayerState {
    Idle,
    Walking,
    Attacking
}

public class ProtaController : MonoBehaviour {

    public float BaseSpeed = 1;
    public float JumpForce = 200;

    PlayerState _playerState;
    Animator _animator;
    Rigidbody2D _rb2d;
    string _currentDirection;

    void Start() {
        _animator = this.GetComponent<Animator>();
        _rb2d = this.GetComponent<Rigidbody2D>();
        _currentDirection = "right";
        _playerState = PlayerState.Idle;
    }

    void FixedUpdate() {
        if (!IsAttacking) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                _rb2d.AddForce(Vector2.up * JumpForce);
            } else if (Input.GetKeyDown(KeyCode.A)) {
                Debug.Log("Attacking");
                this.ChangePlayerState(PlayerState.Attacking);
            } else if (Input.GetKey(KeyCode.RightArrow)) {
                this.ChangePlayerDirection("right");
                this.ChangePlayerState(PlayerState.Walking);
                this.transform.Translate(Vector2.right * BaseSpeed * Time.deltaTime);
            } else if (Input.GetKey(KeyCode.LeftArrow)) {
                this.ChangePlayerDirection("left");
                this.ChangePlayerState(PlayerState.Walking);
                this.transform.Translate(Vector2.left * BaseSpeed * Time.deltaTime);
            } else {
                this.ChangePlayerState(PlayerState.Idle);
            }
        } else {
            Debug.Log("Waiting");
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Prota-Attack") && stateInfo.normalizedTime > 1 && !_animator.IsInTransition(0)) {
                Debug.Log("StoppedWaiting");
                this.ChangePlayerState(PlayerState.Idle);
            }
        }
    }

    void ChangePlayerState(PlayerState playerState) {
        if (_playerState != playerState) {
            _playerState = playerState;
            _animator.SetInteger("State", (int)_playerState);
        }
    }

    void ChangePlayerDirection(string newDirection) {
        if (_currentDirection != newDirection) {
            _currentDirection = newDirection;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public bool IsIdle { get { return _playerState == PlayerState.Idle; } }
    public bool IsWalking { get { return _playerState == PlayerState.Walking; } }
    public bool IsAttacking { get { return _playerState == PlayerState.Attacking; } }
}
