using UnityEngine;
using System.Collections;
using Player;

public class JumpAttackAnimationBehaviour : StateMachineBehaviour {

    Rigidbody2D _playerRigidBody;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _playerRigidBody = animator.GetComponentInParent<ProtaController>().PlayerRigidBody2D;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (_playerRigidBody.velocity.y == 0f) {
            animator.SetInteger("State", 0);
            animator.SetFloat("SpeedModificator", 1);
            animator.GetComponentInParent<ProtaController>().PlayerState = PlayerState.Idle;
        } else if (stateInfo.normalizedTime > 1 && !animator.IsInTransition(layerIndex)) {
            animator.SetInteger("State", 3);
            animator.Play("Prota-Jump", layerIndex, 1.0f);
            animator.GetComponentInParent<ProtaController>().PlayerState = PlayerState.Jumping;
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
