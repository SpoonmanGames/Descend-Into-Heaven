using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player" || other.tag == "Golem") {
            other.GetComponent<Player.Player>().Life = 0;
        }
    }
}
