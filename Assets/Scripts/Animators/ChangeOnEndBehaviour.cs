using UnityEngine;
using System.Collections;

public class ChangeOnEndBehaviour : StateMachineBehaviour {
    
    public string StateVariableName;
    [Space(10)]
    public Player.PlayerState NextPlayerState;
    public string NextStateName;
    [Space(10)]
    [Range(0,1)]
    public float StartingPosition;
    [Range(0, 1)]
    public float WhenChange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {        
        if (stateInfo.normalizedTime > WhenChange && !animator.IsInTransition(layerIndex)) {
            animator.SetInteger(StateVariableName, (int)NextPlayerState);
            animator.GetComponentInParent<Player.Player>().PlayerState = NextPlayerState;
            animator.Play(NextStateName, layerIndex, StartingPosition);
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
