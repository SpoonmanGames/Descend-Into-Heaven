using UnityEngine;
using System.Collections;

namespace Animators.ChangeOn {
    public class ChangeOnTimeBehaviour : ChangeOnBehaviour {

        public float WhenChange = 0.0f;
        public float WhenSpeedIs = 1.0f;

        protected override bool ConditionToChangeOn(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            return animator.GetFloat(SpeedVariableName) == WhenSpeedIs && stateInfo.normalizedTime > WhenChange && !animator.IsInTransition(layerIndex);
        }
    }
}