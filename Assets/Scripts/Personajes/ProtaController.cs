using UnityEngine;
using System.Collections;

namespace Player {

    public class ProtaController : Player {

        [Header("Prota Transition")]
        public GameObject TransitionOut;

        [HideInInspector]
        public bool IsInTransition = false;
        
        private float _victorySoundDelay = 0.8f;
        private float _victorySoundDelayCounter = 0.0f;

        private GameObject _transitionOut;
        private bool _exit = false;

        private float _delayExit = 1.0f;
        private float _delayExitCounter = 0.0f;

        private bool _axisAttack = false;

        void FixedUpdate() {
            if (IsFreeToMove && !IsInTransition && !IsDead) {
                if (!IsJumping && !IsAttacking && Input.GetAxis("Jump") == 1.0f) {
                    this.ChangePlayerState(PlayerState.Jumping);
                    PlayerRigidBody2D.AddForce(Vector2.up * JumpForce);
                } else if (!_axisAttack && !IsAttacking && Input.GetAxis("Attack") == 1.0f) {
                    _axisAttack = true;
                    this.ChangePlayerState(PlayerState.Attacking);
                } else if (!IsAttacking && Input.GetAxis("Right") == 1.0f) {
                    this.HorizontalMovement("right");
                } else if (!IsAttacking && Input.GetAxis("Left") == 1.0f) {
                    this.HorizontalMovement("left");
                } else if(!IsJumping && !IsAttacking){
                    this.ChangePlayerState(PlayerState.Idle);
                }

                if (_axisAttack && Input.GetAxis("Attack") < 1.0f) {
                    _axisAttack = false;
                }
            }

            if (IsFreeToMove) {
                if (Input.GetAxis("Cancel") == 1.0f) {
                    IsFreeToMove = false;
                    _exit = true;
                    this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                    this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    TransitioningOut();
                }
            } else {
                if (_exit) {
                    _delayExitCounter += Time.deltaTime;

                    if (_delayExitCounter >= _delayExit) {
                        Application.LoadLevel("Menu");
                    }
                }
            }
        }

        void Update() {
            if (IsVictory) {
                _victorySoundDelayCounter += Time.deltaTime;

                if (_victorySoundDelayCounter >= _victorySoundDelay) {
                    _victorySoundDelayCounter = 0.0f;
                    audioSource.volume = 0.3f;
                    audioSource.PlayOneShot(soundVictory, 1F);
                }
            }

            if (Life <= 0 && !IsDead) {
                ChangePlayerState(PlayerState.Dead);

                TransitioningOut();
            }
        }

        void TransitioningOut() {
            Vector3 posicion = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
            posicion.z = this.transform.position.z;

            _transitionOut = Instantiate(TransitionOut, posicion, this.transform.rotation) as GameObject;
            _transitionOut.GetComponent<SpriteRenderer>().sortingOrder = 10;
            _transitionOut.GetComponent<Animator>().SetFloat("Speed", -1.0f);
            _transitionOut.GetComponent<Animator>().Play("LoadingTransition", 0, 1.0f);
        }
    }

}