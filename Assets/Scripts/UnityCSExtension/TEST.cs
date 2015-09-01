using UnityEngine;
using System.Collections;

public class TEST : MonoBehaviour {
    
    public float TranslationTime = 10;
    public Vector3 TargetPosition;  

    void Update() {
        if (this.transform.position != TargetPosition) {
            this.transform.Translate(
                ((TargetPosition - this.transform.position) / TranslationTime) * Time.deltaTime
            );
        }
    }
}