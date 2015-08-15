using UnityEngine;
using System.Collections;

namespace Player {    

    public class ProtaController : Player {

        [HideInInspector]
        public bool IsInTransition = false;

        void FixedUpdate() {



            if (!IsInTransition && !IsAttacking && !IsJumping) {
                if (Input.GetKeyDown(KeyCode.UpArrow)) {
                    PlayerRigidBody2D.AddForce(Vector2.up * JumpForce);
                    this.ChangePlayerState(PlayerState.Jumping);
                } else if (Input.GetKeyDown(KeyCode.A)) {
                    this.ChangePlayerState(PlayerState.Attacking);
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
                }
            }
        }
    }
}