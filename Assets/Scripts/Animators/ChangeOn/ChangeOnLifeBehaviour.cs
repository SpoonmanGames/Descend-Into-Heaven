using UnityEngine;
using System.Collections;

namespace Animators.ChangeOn {
    public class ChangeOnLifeBehaviour : ChangeOnBehaviour {

        [SerializeField] private int Life = 0;

        protected override bool ConditionToChangeOn(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            return _playerController.Life <= Life && !animator.IsInTransition(layerIndex);
        }
    }
}