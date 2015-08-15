using UnityEngine;
using System.Collections;

namespace Player {    

    public class ProtaController : Player {

        [HideInInspector]
        public bool IsInTransition = false;
		public AudioClip swordMiss;

		private AudioSource source;

		void Awake () {

			source = GetComponent<AudioSource>();

		}

        void FixedUpdate() {
            if (!IsInTransition) {
                if (!IsAttacking) {
                    if (Input.GetKeyDown(KeyCode.UpArrow)) {
                        _rigidBody2D.AddForce(Vector2.up * JumpForce);
                    } else if (Input.GetKeyDown(KeyCode.A)) {
                        Debug.Log("Attacking");
                        this.ChangePlayerState(PlayerState.Attacking);
						source.PlayOneShot(swordMiss,1F);

                    } else if (Input.GetKey(KeyCode.RightArrow)) {
                        this.HorizontalMovement("right");
                    } else if (Input.GetKey(KeyCode.LeftArrow)) {
                        this.HorizontalMovement("left");


                    } else {
                        this.ChangePlayerState(PlayerState.Idle);
                    }
                } else {
                    AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
                    if (this.HasAnimatorStateEnded_ByName(AttackingAnimatorName)) {
                        this.ChangePlayerState(PlayerState.Idle);
                    }
                }
            }
        }
    }
}