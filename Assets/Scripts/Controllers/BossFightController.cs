using UnityEngine;
using System.Collections;

public class BossFightController : MonoBehaviour {

    public Player.Player PlayerController;
    public Player.Player BossController;

    void Start() {
        PlayerController.IsFreeToMove = false;
        BossController.IsFreeToMove = false;
    }

    void Update() {
        PlayerController.IsFreeToMove = BossController.IsFreeToMove;
    }
}
