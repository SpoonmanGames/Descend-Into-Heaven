using UnityEngine;
using System.Collections;
using Player;

public class AttackColliderController : MonoBehaviour {

    public Player.Player playerAttackingController;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Boss") {            
            Boss enemy = other.GetComponentInParent<Boss>();
            enemy.Hurt(playerAttackingController.AttackDamage);
        } else if (other.tag == "Golem") {
            BadGuy badGuy = other.GetComponentInParent<BadGuy>();
            badGuy.Hurt(playerAttackingController.AttackDamage);
        } else if (other.tag == "Player") {
            ProtaController prota = other.GetComponentInParent<ProtaController>();
            prota.Life -= playerAttackingController.AttackDamage;
        }
    }
}
