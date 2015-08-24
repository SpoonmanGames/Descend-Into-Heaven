using UnityEngine;
using System.Collections;
using Player;

public class SpawnBehaviour : StateMachineBehaviour {

    public GameObject SpawnGameObject;
    public float SpawnTime = 0.0f;
    public bool DependOnThisState = true;
    public float DestroyTime = 0.0f;
    public float OffsetDueDirection = 0.0f;

    private bool _spawning;
    private GameObject _spawnedGameObject;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _spawning = false;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.normalizedTime > SpawnTime && !_spawning) {
            Vector3 animatorPosition;

            _spawning = true;

            if (animator.transform.localScale.x > 0) {
                animatorPosition = animator.transform.position;
            } else {
                animatorPosition = animator.transform.position + Vector3.left * OffsetDueDirection;                                                         
            }
            
            _spawnedGameObject = Instantiate(SpawnGameObject, animatorPosition, animator.transform.rotation) as GameObject;
            _spawnedGameObject.transform.parent = animator.transform;
        }

        if (DependOnThisState && stateInfo.normalizedTime > DestroyTime) {
            if (_spawnedGameObject != null) {
                Destroy(_spawnedGameObject);
            }
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // In case it is not destroyed for some reason when it depends on the animation
        if (DependOnThisState && _spawnedGameObject != null) {
            Destroy(_spawnedGameObject);
        }
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
