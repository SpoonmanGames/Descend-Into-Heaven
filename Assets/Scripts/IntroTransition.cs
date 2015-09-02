using UnityEngine;
using System.Collections;

public class IntroTransition : MonoBehaviour {

    void Awake() {
        this.GetComponent<Animator>().SetFloat("Speed", 1.0f);
        this.GetComponent<Animator>().Play("LoadingTransition", 0, 0.0f);
    }
}
