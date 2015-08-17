using UnityEngine;
using System.Collections;

namespace Player {    

    public class ProtaController : Player {

        [HideInInspector]
        public bool IsInTransition = false;
		public AudioClip swordMiss;
		public AudioClip jump;


        void FixedUpdate() {
            if (!IsInTransition) {                
                if (!IsJumping && !IsAttacking && Input.GetKeyDown(KeyCode.UpArrow)) {
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