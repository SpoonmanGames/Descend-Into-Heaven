using UnityEngine;
using System.Collections;

namespace Animators.ChangeOn {
    public class ChangeOnAirGroundBehaviour : ChangeOnBehaviour {

        public string GroundVariableName = "Ground";
        public bool ChangeOnAir = false;
        public bool ChangeOnGround = false;

        protected override bool ConditionToChangeOn(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (animator.GetBool(GroundVariableName)) {
                return ChangeOnGround;
            } else {
                return ChangeOnAir;
            }
        }
    }
}