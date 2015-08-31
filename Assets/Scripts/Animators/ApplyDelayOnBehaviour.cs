using UnityEngine;
using System.Collections;

public class ApplyDelayOnBehaviour : StateMachineBehaviour {

    [Space(10)]
    [SerializeField] private string SpeedVariableName = "Speed";
    [Space(10)]
    [SerializeField] private float DelaySpeedValue = 0.0f;
    [SerializeField] private float SpeedDecreaseFactor = 1.0f;
    [SerializeField] private float SpeedAugmentedFactor = 1.0f;
    [Space(10)]
    [Range(0.0f,1.0f)] [SerializeField] private float WhenApplyDelay = 0.0f;
    [SerializeField] private float TimeTillResume = 1.0f;
    [SerializeField] private float ResumeSpeedValue = 1.0f;

    private float _counterTimeTillResume = 0.0f;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.normalizedTime > WhenApplyDelay && !animator.IsInTransition(layerIndex)) {
            _counterTimeTillResume += Time.deltaTime;

            //animator.SetFloat(SpeedVariableName, )

            if (_counterTimeTillResume >= TimeTillResume) {

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
