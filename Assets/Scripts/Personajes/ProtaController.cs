using UnityEngine;
using System.Collections;

namespace Player {

    public class ProtaController : Player {

        [Header("Audio Setup")]
        public AudioClip swordMiss;                
	public AudioClip jump;

        [HideInInspector]
        public bool IsInTransition = false;


        void FixedUpdate() {
            if (IsFreeToMove && !IsInTransition && !IsDead) {                
                if (!IsJumping && !IsAttacking && Input.GetKey(KeyCode.UpArrow)) {
                    this.ChangePlayerState(PlayerState.Jumping);
			audioSource.PlayOneShot(jump, 1F);
                    PlayerRigidBody2D.AddForce(Vector2.up * JumpForce);
                } else if (!IsAttacking && Input.GetKeyDown(KeyCode.A)) {
                    this.ChangePlayerState(PlayerState.Attacking);
                    audioSource.PlayOneShot(swordMiss, 1F);
                } else if (!IsAttacking && Input.GetKey(KeyCode.RightArrow)) {
                    this.HorizontalMovement("right");
                } else if (!IsAttacking &&  Input.GetKey(KeyCode.LeftArrow)) {
                    this.HorizontalMovement("left");
                } else if(!IsJumping && !IsAttacking){
                    this.ChangePlayerState(PlayerState.Idle);
                }
            }
        }
    }
}