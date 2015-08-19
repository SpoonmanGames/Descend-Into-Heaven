using UnityEngine;
using System.Collections;

namespace Player {
    public class BadGuy : Player {        

        //delay antes o despues de atacar
        //te sigue cuando estas a su misma altura
        //según su rango
        //ataca en su rango

        [Header("Behaviour")]
        public ProtaController _protaController;
        public float LeftRangeOfSight = 0.5f;
        public float RightRangeOfSight = 0.5f;
        [Space(10)]
        public float RangeOfAttack = 0.5f;
        public float DelayToAttack = 0.0f;
        public bool DelayBeforeAttack = true;

        private float _delayToAttackCounter;
        private bool _following = false;

        public void Hurt(int damage) {
            Life -= damage;
            this.ChangePlayerState(PlayerState.Hurt);
        }

        protected override void Start() {
            base.Start();

            this._currentDirection = "left";
            if (DelayBeforeAttack) {
                _delayToAttackCounter = 0.0f;
            } else {
                _delayToAttackCounter = DelayToAttack;
            }
        }

        void FixedUpdate() {
            if (IsFreeToMove && !IsDead && !IsHurt) {
                float hightPositionDifference = this.transform.position.y - _protaController.transform.position.y;

                if (!IsAttacking && !IsHurt && (hightPositionDifference >= -0.03 && hightPositionDifference <= 0.03 || _following)) {

                    _following = true;

                    if (!IsAttacking && !IsHurt 
                        && this.transform.position.x - RangeOfAttack <= _protaController.transform.position.x
                        && this.transform.position.x + RangeOfAttack >= _protaController.transform.position.x) {

                        _delayToAttackCounter += Time.deltaTime;

                        if (_delayToAttackCounter >= DelayToAttack) {
                            if (!IsAttacking && !IsHurt && this.transform.position.x > _protaController.transform.position.x) {
                                this.ChangePlayerDirection("left");
                                this.ChangePlayerState(PlayerState.Attacking);
                            } else if (!IsAttacking && !IsHurt) {
                                this.ChangePlayerDirection("right");
                                this.ChangePlayerState(PlayerState.Attacking);
                            }
                            
                            _delayToAttackCounter = 0.0f;
                        }
                    } else  if (!IsAttacking && !IsHurt 
                        && this.transform.position.x - LeftRangeOfSight <= _protaController.transform.position.x
                        && this.transform.position.x + RightRangeOfSight >= _protaController.transform.position.x) {
                        // At Left
                        if (!IsAttacking && !IsHurt && this.transform.position.x > _protaController.transform.position.x) {
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
    }
}
