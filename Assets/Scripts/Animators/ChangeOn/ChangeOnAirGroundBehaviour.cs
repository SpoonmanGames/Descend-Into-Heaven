using UnityEngine;
using System.Collections;

namespace Animators.ChangeOn {
    public class ChangeOnAirGroundBehaviour : ChangeOnBehaviour {

        [SerializeField] private string GroundVariableName = "Ground";
        [SerializeField] private bool ChangeOnAir = false;
        [SerializeField] private bool ChangeOnGround = false;

        protected override bool ConditionToChangeOn(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (animator.GetBool(GroundVariableName) && !animator.IsInTransition(layerIndex)) {
                return ChangeOnGround;
            } else {
                return ChangeOnAir;
            }
        }
    }
}