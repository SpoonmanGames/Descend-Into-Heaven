using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
    public float lifeTime;

    void Start() {
        Destroy(this.gameObject, lifeTime);
    }
}
