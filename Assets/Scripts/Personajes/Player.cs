using UnityEngine;
using System.Collections;

namespace Player {

    public enum PlayerState {
        Idle,
        Walking,
        Attacking,
        Jumping,
        Dead,
        Victory,
        Hurt
    }


    public abstract class Player : MonoBehaviour {

        [Header("Base Setup")]
        public int Life = 1;
        public int AttackDamage = 1;
        [Space(10)]
        public float WalkingSpeed = 1;
        public float JumpForce = 200;

        [Header("Animator Setup")]
        [Tooltip("This is the name of the Variable you use to change from one state to another.")]
        public string StateVariableName = string.Empty;
        [Space(10)]
        public string IdleAnimatorName = string.Empty;
        public string WalkingAnimatorName = string.Empty;
        public string AttackingAnimatorName = string.Empty;
        public string JumpingAnimatorName = string.Empty;
        public string DeadAnimatorName = string.Empty;
        public string VictoryAnimatorName = string.Empty;
        public string HurtAnimatorName = string.Empty;

        [HideInInspector]
        public bool IsFreeToMove = true;
        [HideInInspector]
        public PlayerState PlayerState = PlayerState.Idle;
        [HideInInspector]
        public Rigidbody2D PlayerRigidBody2D;

        protected bool _isBeingHurt = false;
        protected string _currentDirection = "right";
        protected Animator _animator;
        protected bool _hasAnimatorComponent;
        protected AudioSource audioSource;


        virtual protected void Start() {
            _animator = this.GetComponent<Animator>();
            _hasAnimatorComponent = _animator != null;
            
            PlayerRigidBody2D = this.GetComponent<Rigidbody2D>();

            audioSource = GetComponent<AudioSource>();

            if (!_hasAnimatorComponent) {
                Debug.LogError("The Player doesn't have an Animator Component.");
            }

            if (PlayerRigidBody2D == null) {
                Debug.LogError("The Player doesn't have a RigidBody2D Component.");
            }
        }

        void Update() {            
            if (Life <= 0 && !IsDead) {
                ChangePlayerState(PlayerState.Dead);
            }
        }

        /*
         * Methods
         */

        public void ChangePlayerState(PlayerState playerState) {
            if (PlayerState != playerState) {
                PlayerState = playerState;
                _animator.SetInteger(StateVariableName, (int)PlayerState);
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

        protected void HorizontalMovement(string directionOfMovement) {
            Vector2 direction;

            this.ChangePlayerDirection(directionOfMovement);

            if (!IsJumping) {
                this.ChangePlayerState(PlayerState.Walking);
            }

            if (directionOfMovement == "right") {
                direction = Vector2.right;
            } else {
                direction = Vector2.left;
            }

            this.transform.Translate(direction * WalkingSpeed * Time.deltaTime);
        }

        /*
         * Properties, most of them are Helpers for validations 
         */

        public bool IsIdle { get { return PlayerState == PlayerState.Idle; } }
        public bool IsWalking { get { return PlayerState == PlayerState.Walking; } }
        public bool IsAttacking { get { return PlayerState == PlayerState.Attacking; } }
        public bool IsJumping { get { return PlayerState == PlayerState.Jumping; } }
        public bool IsDead { get { return PlayerState == PlayerState.Dead; } }
        public bool IsHurt { get { return PlayerState == PlayerState.Hurt; } }
    }
}