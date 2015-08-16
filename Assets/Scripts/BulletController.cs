using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    public bool Horizontal = true;
    public bool IdaYVuelta = true;
    [Space(10)]
    public float LimiteInferior = 0.0f;
    public float LimiteSuperior = 0.0f;
    [Space(10)]
    public GameObject Bullet;

    void LateUpdate(){
        float actualPosition;

        if (Horizontal){
            actualPosition = Bullet.transform.position.x;
        } else {
            actualPosition = Bullet.transform.position.y;
        }

        
    }
    
}
