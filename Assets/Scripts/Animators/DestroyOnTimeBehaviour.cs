﻿using UnityEngine;
using System.Collections;

public class DestroyOnTimeBehaviour : StateMachineBehaviour {

    [SerializeField] private bool DependsOnSpeed = true;
    [SerializeField] private string SpeedVariableName = "Speed";
    [SerializeField] private float Speed = 1.0f;
    [SerializeField] private float WhenDestroy;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.normalizedTime > WhenDestroy && !animator.IsInTransition(layerIndex)) {
            if (DependsOnSpeed) {
                if (animator.GetFloat(SpeedVariableName) == Speed) {
                    Destroy(animator.gameObject);
                }
            } else {
                Destroy(animator.gameObject);
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
