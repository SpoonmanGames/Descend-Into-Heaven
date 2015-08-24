using UnityEngine;
using System.Collections;

namespace Player {

    public enum PlayerState {
        Idle, // 0
        Walking, // 1
        Attacking, // 2
        Jumping, // 3
        Dead, // 4
        Victory, // 5
        Hurt, // 6
        IdleAir // 7
    }

    [ExecuteInEditMode]
    public abstract class Player : MonoBehaviour {

        [Header("Base Setup")]
        public int Life = 1;
        public int AttackDamage = 1;
        [Space(10)]
        public float WalkingSpeed = 1;
        public float JumpForce = 200;
        [SerializeField] private bool m_AirControl = true;
        [SerializeField] private LayerMask m_WhatIsGround;
        [Header("Ground and Ceiling Setup")]
        public float GroundedRadius = 0.2f;
        public float CeilingRadius = 0.01f;
        [Header("Animator Setup")]
        public string StateVariableName = "State";
        public string SpeedVariableName = "Speed";

        [Header("Audio Setup")]
        [SerializeField] protected AudioClip m_soundIdle;                
	    [SerializeField] protected AudioClip m_soundWalking;
	    [SerializeField] protected AudioClip m_soundAttacking;
	    [SerializeField] protected AudioClip m_soundJumping;
	    [SerializeField] protected AudioClip m_soundDead;
	    [SerializeField] protected AudioClip m_soundVictory;
	    [SerializeField] protected AudioClip m_soundHurt;
        [SerializeField] protected AudioClip m_soundIdleAir;

        [Header("Debbuger Setup")]
        public bool EditorDebugMode = true;

        [HideInInspector] public bool IsFreeToMove = true;
        [HideInInspector] public PlayerState PlayerState = PlayerState.Idle;

        protected Rigidbody2D _playerRigidBody2D;
        protected Animator _playerAnimator;
        protected string _currentDirection = "right";        
        protected AudioSource _playerAudioSource;

        private Transform _GroundCheck;        
        protected bool _Grounded;
        private Transform _CeilingCheck;        

        virtual protected void Awake() {
            _playerAnimator = this.GetComponent<Animator>();            
            _playerRigidBody2D = this.GetComponent<Rigidbody2D>();
            _playerAudioSource = GetComponent<AudioSource>();
            _GroundCheck = transform.Find("GroundCheck");
            _CeilingCheck = transform.Find("CeilingCheck");

            if (_playerAnimator == null) {
                Debug.LogWarning("The Player doesn't have an Animator Component.");
            }

            if (_playerRigidBody2D == null) {
                Debug.LogWarning("The Player doesn't have a RigidBody2D Component.");
            }

            if (_playerAudioSource == null) {
                Debug.LogWarning("The Player doesn't have a AudioSource Component.");
            }

            if (_GroundCheck == null) {
                Debug.LogWarning("The Player doesn't have a GroundCheck Child.");
            }

            if (_CeilingCheck == null) {
                Debug.LogWarning("The Player doesn't have a CeilingCheck Child.");
            }
        }

        virtual protected void FixedUpdate() {
            _Grounded = false;

            // Esto se podría hacer usando layers
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_GroundCheck.position, GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].gameObject != this.gameObject)
                    _Grounded = true;
            }
            _playerAnimator.SetBool("Ground", _Grounded);
            _playerAnimator.SetFloat("vSpeed", _playerRigidBody2D.velocity.y);
        }

        virtual protected void Update() {
            if (EditorDebugMode) {
                DebugExtension.DebugWireSphere(_CeilingCheck.position, Color.red, CeilingRadius);
                DebugExtension.DebugWireSphere(_GroundCheck.position, Color.red, GroundedRadius);
            }
        }

        /*
         * Methods
         */

        public void ChangePlayerState(PlayerState playerState) {
            if (PlayerState != playerState) {
		        switch (playerState){
			        case PlayerState.Idle:
			            _playerAudioSource.PlayOneShot(m_soundIdle, 1.0f);
  			            break;
  			        case PlayerState.Walking:
			            _playerAudioSource.PlayOneShot(m_soundWalking, 1.0f);
  			            break;
  			        case PlayerState.Attacking:
			            _playerAudioSource.PlayOneShot(m_soundAttacking, 1.0f);
  			            break;
  			        case PlayerState.Dead:
			            _playerAudioSource.PlayOneShot(m_soundDead, 1.0f);
  			            break;
  			        case PlayerState.Victory:
			            _playerAudioSource.PlayOneShot(m_soundVictory, 0.3f);
  			            break;
  			        case PlayerState.Hurt:
			            _playerAudioSource.PlayOneShot(m_soundHurt, 1.0f);
  			            break;
                    case PlayerState.IdleAir:
                        _playerAudioSource.PlayOneShot(m_soundIdleAir, 1.0f);
                        break;
                }                
	
                PlayerState = playerState;

                if (!IsJumping) {
                    _playerAnimator.SetFloat(SpeedVariableName, 1.0f);
                }

                _playerAnimator.SetInteger(StateVariableName, (int)PlayerState);
            }
        }

        protected void VerticalMovement() {
            if (_Grounded) {
                _Grounded = false;
                _playerAnimator.SetBool("Ground", _Grounded);
                ChangePlayerState(PlayerState.Jumping);
                _playerRigidBody2D.AddForce(Vector2.up * JumpForce);
            }
        }

        protected void HorizontalMovement(string directionOfMovement) {
            if (_Grounded || m_AirControl) {
                Vector2 direction;

                if (directionOfMovement == "right") {
                    direction = Vector2.right;
                } else {
                    direction = Vector2.left;
                }

                this.ChangePlayerDirection(directionOfMovement);

                if (!IsJumping) {
                    this.ChangePlayerState(PlayerState.Walking);
                }
                
                this.transform.Translate(direction * WalkingSpeed * Time.deltaTime);
            }
        }

        protected void ChangePlayerDirection(string newDirection) {
            if (_currentDirection != newDirection) {
                _currentDirection = newDirection;

                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }

        /*
         * Properties
         */

        public bool IsIdle { get { return PlayerState == PlayerState.Idle; } }
        public bool IsWalking { get { return PlayerState == PlayerState.Walking; } }
        public bool IsAttacking { get { return PlayerState == PlayerState.Attacking; } }
        public bool IsJumping { get { return PlayerState == PlayerState.Jumping; } }
        public bool IsDead { get { return PlayerState == PlayerState.Dead; } }
        public bool IsVictory { get { return PlayerState == PlayerState.Victory; } }
        public bool IsHurt { get { return PlayerState == PlayerState.Hurt; } }
        public bool IsIdleAir { get { return PlayerState == PlayerState.IdleAir; } }
    }
}