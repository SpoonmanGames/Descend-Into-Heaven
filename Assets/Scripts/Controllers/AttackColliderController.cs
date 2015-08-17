using UnityEngine;
using System.Collections;
using Player;

public class AttackColliderController : MonoBehaviour {

    public ProtaController protaController;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {            
            Boss enemy = other.GetComponentInParent<Boss>();
            enemy.Hurt(protaController.AttackDamage);
        }
    }
}
