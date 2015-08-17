using UnityEngine;
using System.Collections;

public class KillByTouch : MonoBehaviour {

    private Player.Player PlayerController;

    void Start() {
        PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            PlayerController.Life--;
        }
    }
}
