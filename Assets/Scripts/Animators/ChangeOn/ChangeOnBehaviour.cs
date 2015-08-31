using UnityEngine;
using System.Collections;

namespace Animators.ChangeOn {
    public abstract class ChangeOnBehaviour : StateMachineBehaviour {

        [SerializeField] private bool IsPlayer = true;
        [SerializeField] private Player.PlayerState NextPlayerState;
        [SerializeField] private string StateVariableName = "State";
        [SerializeField] private int NextStateValue;
        [SerializeField] private string NextStateName;
        [SerializeField] private float StartingPosition = 0.0f;
        [SerializeField] protected string SpeedVariableName = "Speed";
        [SerializeField] private float StartingSpeedValue = 1.0f;

        protected Player.Player _playerController;

        private int _nextState;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (IsPlayer) {
                _nextState = (int)NextPlayerState;
                _playerController = animator.GetComponentInParent<Player.Player>();
            } else {
                _nextState = NextStateValue;
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (this.ConditionToChangeOn(animator, stateInfo, layerIndex)) {
                if (IsPlayer) {
                    _playerController.ChangePlayerState(NextPlayerState);
                } else {
                    animator.SetInteger(StateVariableName, _nextState);
                }

                animator.SetFloat(SpeedVariableName, StartingSpeedValue);
                animator.Play(NextStateName, layerIndex, StartingPosition);
            }
        }

        abstract protected bool ConditionToChangeOn(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);

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
}