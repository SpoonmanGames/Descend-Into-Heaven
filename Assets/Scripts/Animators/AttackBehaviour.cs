using UnityEngine;
using System.Collections;
using Player;

public class AttackBehaviour : StateMachineBehaviour {

    public GameObject AttackTriggerCollider;
    [Range(0,1)]
    public float SpawTime;
    [Range(0, 1)]
    public float DestroyTime;
    [Space(10)]
    public float OffsetDueDirection = 0.0f;

    private bool _attacking;
    private GameObject _spawnedCollider;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _attacking = false;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.normalizedTime > SpawTime && !_attacking) {
            Vector3 animatorPosition;

            if (animator.transform.localScale.x > 0) {
                animatorPosition = animator.transform.position;
            } else {
                animatorPosition = new Vector3(animator.transform.position.x - OffsetDueDirection, animator.transform.position.y, animator.transform.position.z);
            }

            _attacking = true;
            _spawnedCollider = Instantiate(AttackTriggerCollider, animatorPosition, animator.transform.rotation) as GameObject;
            _spawnedCollider.transform.parent = animator.transform;            
        }

        if (stateInfo.normalizedTime > DestroyTime) {
            if (_spawnedCollider != null) {
                Destroy(_spawnedCollider);
            }
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
