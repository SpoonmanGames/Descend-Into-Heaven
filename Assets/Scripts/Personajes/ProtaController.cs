using UnityEngine;
using System.Collections;

namespace Player {

    public class ProtaController : Player {

        [HideInInspector]
        public bool IsInTransition = false;
        
        private float _victorySoundDelay = 0.8f;
        private float _victorySoundDelayCounter = 0.0f;

        void FixedUpdate() {
            if (IsFreeToMove && !IsInTransition && !IsDead) {                
                if (!IsJumping && !IsAttacking && Input.GetKey(KeyCode.UpArrow)) {
                    this.ChangePlayerState(PlayerState.Jumping);
                    PlayerRigidBody2D.AddForce(Vector2.up * JumpForce);
                } else if (!IsAttacking && Input.GetKeyDown(KeyCode.A)) {
                    this.ChangePlayerState(PlayerState.Attacking);
                } else if (!IsAttacking && Input.GetKey(KeyCode.RightArrow)) {
                    this.HorizontalMovement("right");
                } else if (!IsAttacking &&  Input.GetKey(KeyCode.LeftArrow)) {
                    this.HorizontalMovement("left");
                } else if(!IsJumping && !IsAttacking){
                    this.ChangePlayerState(PlayerState.Idle);
                }
            }

            if (IsFreeToMove) {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    IsFreeToMove = false;
                    Application.LoadLevel("Menu");
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
        }
    }
}