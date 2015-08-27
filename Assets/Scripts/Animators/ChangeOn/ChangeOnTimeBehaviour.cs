using UnityEngine;
using System.Collections;

namespace Animators.ChangeOn {
    public class ChangeOnTimeBehaviour : ChangeOnBehaviour {

        [SerializeField] private float WhenChange = 0.0f;
        [SerializeField] private float WhenSpeedIs = 1.0f;

        protected override bool ConditionToChangeOn(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            return animator.GetFloat(SpeedVariableName) == WhenSpeedIs && stateInfo.normalizedTime > WhenChange && !animator.IsInTransition(layerIndex);
        }
    }
}