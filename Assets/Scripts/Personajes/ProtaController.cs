using UnityEngine;
using System.Collections;

namespace Player {

    public class ProtaController : Player {

        [HideInInspector]
        public bool IsInTransition = false;

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
        }
    }
}