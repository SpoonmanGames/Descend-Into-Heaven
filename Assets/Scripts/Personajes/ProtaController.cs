using UnityEngine;
using System.Collections;

namespace Player {    

    public class ProtaController : Player {

        [HideInInspector]
        public bool IsInTransition = false;
		public AudioClip swordMiss;		


        void FixedUpdate() {
            if (!IsInTransition) {
                if (!IsAttacking && !IsJumping) {
                    if (Input.GetKeyDown(KeyCode.UpArrow)) {
                        this.ChangePlayerState(PlayerState.Jumping);
                        PlayerRigidBody2D.AddForce(Vector2.up * JumpForce);                        
                    } else if (Input.GetKeyDown(KeyCode.A)) {
                        this.ChangePlayerState(PlayerState.Attacking);
                        audioSource.PlayOneShot(swordMiss, 1F);
                    } else if (Input.GetKey(KeyCode.RightArrow)) {
                        this.HorizontalMovement("right");
                    } else if (Input.GetKey(KeyCode.LeftArrow)) {
                        this.HorizontalMovement("left");
                    } else {
                        this.ChangePlayerState(PlayerState.Idle);
                    }
                } else {
                    if (IsJumping && Input.GetKeyDown(KeyCode.A)) {
                        this.ChangePlayerState(PlayerState.Attacking);
                        audioSource.PlayOneShot(swordMiss, 1F);
                    }
                }
            }
        }
    }
}