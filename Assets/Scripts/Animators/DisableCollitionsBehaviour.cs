using UnityEngine;
using System.Collections;

public class DisableCollitionsBehaviour : StateMachineBehaviour {

    [SerializeField] private bool DisableGravity = true;

    private Collider2D[] _playerCollider2D;
    private Rigidbody2D _playerRigidBody2D;
    private float _gravitySaved;
    private Vector2 _velocitySaved;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _playerCollider2D = animator.GetComponentsInParent<Collider2D>();
        for (int i = 0; i < _playerCollider2D.Length; i++) {
            _playerCollider2D[i].enabled = false;
        }

        if (DisableGravity) {
            _playerRigidBody2D = animator.GetComponentInParent<Rigidbody2D>();
            _gravitySaved = _playerRigidBody2D.gravityScale;
            _playerRigidBody2D.gravityScale = 0.0f;

            _velocitySaved = _playerRigidBody2D.velocity;
            _playerRigidBody2D.velocity = Vector2.zero;
        }

        Debug.Log("Starting dead");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        for (int i = 0; i < _playerCollider2D.Length; i++) {
            _playerCollider2D[i].enabled = true;
        }

        if (DisableGravity) {
            _playerRigidBody2D.gravityScale = _gravitySaved;
            _playerRigidBody2D.velocity = _velocitySaved;
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
