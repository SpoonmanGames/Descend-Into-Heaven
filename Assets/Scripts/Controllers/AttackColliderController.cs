using UnityEngine;
using System.Collections;
using Player;

public class AttackColliderController : MonoBehaviour {

    public ProtaController protaController;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {            
            Enemy enemy = other.GetComponentInParent<Enemy>();
            enemy.Hurt(protaController.AttackDamage);
        }
    }
}
