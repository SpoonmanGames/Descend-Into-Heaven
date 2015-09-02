using UnityEngine;
using System.Collections;

namespace Player {

    public class ProtaController : Player {

        [SerializeField] private GameObject TransitionOut;

        [HideInInspector] public bool IsInTransition = false;

        private GameObject _transitionOut;
        private bool _exit = false;
        private float _delayExit = 1.0f;
        private float _delayExitCounter = 0.0f;
        private bool jumping = false;
        private bool attacking = false;

        protected override void FixedUpdate() {
            base.FixedUpdate();

            bool movingRight = Input.GetButton("Right");
            bool movingLeft = Input.GetButton("Left");

            if (IsFreeToMove && !IsInTransition && !IsDead && !IsAttacking) {
                if (!IsJumping && !IsIdleAir && _Grounded && jumping) {
                    _playerAudioSource.PlayOneShot(SoundJumping, 1.0f);
                    this.VerticalMovement();
                } else if (attacking) {
                    this.ChangePlayerState(PlayerState.Attacking);
                } else if (movingRight) {
                    this.HorizontalMovement("right");
                } else if (movingLeft) {
                    this.HorizontalMovement("left");
                } else if(!IsJumping && !IsIdleAir){
                    this.ChangePlayerState(PlayerState.Idle);
                }
            }

            jumping = false;
            attacking = false;
        }

        override protected void Update() {
            base.Update();

            if (IsFreeToMove && !IsInTransition && !IsDead) {
                if (!IsJumping && !IsIdleAir && _Grounded && !jumping) {
                    jumping = Input.GetButton("Jump");
                }

                if (!IsAttacking && !attacking) {
                    attacking = Input.GetButtonDown("Attack");
                }
            
                // TODO: Falta script que haga la pega de un menú y pause
                if (Input.GetAxis("Cancel") == 1.0f) {
                    IsFreeToMove = false;
                    _exit = true;
                    this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                    this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    TransitioningOut();
                } else {
                    if (_exit) {
                        _delayExitCounter += Time.deltaTime;

                        if (_delayExitCounter >= _delayExit) {
                            Application.LoadLevel("Menu");
                        }
                    }
                }
            }

            if (Life <= 0 && !IsDead) {
                TransitioningOut();
            }
        }

        void TransitioningOut() {
            Vector3 posicion = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
            posicion.z = this.transform.position.z;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SuperMetroidCameraController>().PlayerIsDead = true;

            _transitionOut = Instantiate(TransitionOut, posicion, this.transform.rotation) as GameObject;
            _transitionOut.GetComponent<SpriteRenderer>().sortingOrder = 10;
            _transitionOut.GetComponent<Animator>().SetFloat("Speed", -1.0f);
            _transitionOut.GetComponent<Animator>().Play("LoadingTransition", 0, 1.0f);
        }

        public override void Hurt(int damage) {
            throw new System.NotImplementedException();
        }
    }

}