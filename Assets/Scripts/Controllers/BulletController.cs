using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    public int Daño = 1;
    public float Tiempo = 1;
    public bool DestruirAlTerminar = false;
    
    [HideInInspector]
    public float Direccion = -1;
    [HideInInspector]
    public Vector2 TargetPosition;

    private float _velocidadX;
    private float _velocidadY;

    public void SetVelocity() {
        _velocidadX = (this.transform.position.x - TargetPosition.x) / Tiempo;
        _velocidadY = (this.transform.position.y - TargetPosition.y) / Tiempo;

        if (Direccion == 1) {
            _velocidadX *= -1;
            _velocidadY *= -1;
        }
    }

    void Start() {
        SetVelocity();
    }

    void LateUpdate(){
        Vector2 actualPosition = new Vector2(this.transform.position.x, this.transform.position.y);

        if (actualPosition != TargetPosition) {
            Vector3 newPosition;

            newPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            newPosition += new Vector3(_velocidadX * Time.deltaTime * Direccion, _velocidadY * Time.deltaTime * Direccion, 0.0f);

            this.transform.position = newPosition;
        } else {
            if (DestruirAlTerminar) {
                Destroy(this.gameObject);
            }
        }
    }    
}
