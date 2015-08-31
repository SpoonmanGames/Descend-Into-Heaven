using UnityEngine;
using System.Collections;

namespace Player {
    public class BadGuy : Player {        

        [SerializeField] private ProtaController ProtaController;
        [SerializeField] private Bounds DetectionBlock;
        public bool UseHightDetection = true;
        public bool AlwaysFollow = false;
        public float RangeOfAttack = 0.5f;
        public float DelayToAttack = 0.0f;
        public bool DelayBeforeAttack = true;

        private float _delayToAttackCounter;
        private bool _following = false;
        private Bounds _realDetectionBlock;

        public override void Hurt(int damage) {
            Life -= damage;
            this.ChangePlayerState(PlayerState.Hurt);
        }

        void Start() {
            this._currentDirection = "left";
            if (DelayBeforeAttack) {
                _delayToAttackCounter = 0.0f;
            } else {
                _delayToAttackCounter = DelayToAttack;
            }
        }

        override protected void FixedUpdate() {
            base.FixedUpdate();

            _realDetectionBlock = DetectionBlock;
            _realDetectionBlock.center = DetectionBlock.center + this.transform.position;

            if (IsFreeToMove && !IsDead && !IsHurt) {
                float hightPositionDifference = this.transform.position.y - ProtaController.transform.position.y;

                if (!IsAttacking && !IsHurt && (hightPositionDifference >= -0.03 && hightPositionDifference <= 0.03 || _following)) {

                    if (AlwaysFollow) {
                        _following = true;
                    }

                    if (!IsAttacking && !IsHurt 
                        && this.transform.position.x - RangeOfAttack <= ProtaController.transform.position.x
                        && this.transform.position.x + RangeOfAttack >= ProtaController.transform.position.x) {

                        _delayToAttackCounter += Time.deltaTime;

                        if (_delayToAttackCounter >= DelayToAttack) {
                            if (!IsAttacking && !IsHurt && this.transform.position.x > ProtaController.transform.position.x) {
                                this.ChangePlayerDirection("left");
                                this.ChangePlayerState(PlayerState.Attacking);
                            } else if (!IsAttacking && !IsHurt) {
                                this.ChangePlayerDirection("right");
                                this.ChangePlayerState(PlayerState.Attacking);
                            }
                            
                            _delayToAttackCounter = 0.0f;
                        }
                    } else if (!IsAttacking && !IsHurt && _realDetectionBlock.Contains(ProtaController.transform.position)) {
                        // At Left
                        if (!IsAttacking && !IsHurt && this.transform.position.x > ProtaController.transform.position.x) {
                            this.HorizontalMovement("left");
                        } else if (!IsAttacking && !IsHurt) {
                            this.HorizontalMovement("right");
                        }
                    } else if (!IsAttacking && !IsHurt) {
                        this.ChangePlayerState(PlayerState.Idle);
                    }
                } else if (!IsAttacking && !IsHurt) {
                    this.ChangePlayerState(PlayerState.Idle);
                }
            }
        }

        protected override void Update() {
            base.Update();

            _realDetectionBlock = DetectionBlock;
            _realDetectionBlock.center = DetectionBlock.center + this.transform.position;

            if (EditorDebugMode) {
                DebugExtension.DebugBounds(_realDetectionBlock, Color.red);
            }
        }
    }
}
