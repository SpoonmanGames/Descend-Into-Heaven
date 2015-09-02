using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    public int Daño = 1;
    public float Tiempo = 1;
    public bool DestruirAlTerminar = false;
    
    public Vector2 TargetPosition;

    private float _velocidadX;
    private float _velocidadY;

    public void SetVelocity() {
        _velocidadX = (TargetPosition.x - this.transform.position.x) / Tiempo;
        _velocidadY = (TargetPosition.y - this.transform.position.y) / Tiempo;
    }

    void Start() {
        SetVelocity();
    }

    void LateUpdate(){
        if (!IsOnDestiny()) {
            Vector3 newPosition;

            newPosition = this.transform.position;
            newPosition += new Vector3(_velocidadX * Time.deltaTime, _velocidadY * Time.deltaTime, 0.0f);

            this.transform.position = newPosition;
        } else {
            this.transform.position = TargetPosition;
            if (DestruirAlTerminar) {
                Destroy(this.gameObject);
            }
        }
    }

    bool IsOnDestiny() {
        Vector2 actualPosition = this.transform.position;

        if (_velocidadX <= 0 ) {

            if (actualPosition.x <= TargetPosition.x) {
                if (_velocidadY <= 0) {
                    if (actualPosition.y <= TargetPosition.y) {
                        return true;
                    }
                } else {
                    if (actualPosition.y >= TargetPosition.y) {
                        return true;
                    }
                }
            }           

            
        } else {

            if (actualPosition.x >= TargetPosition.x) {
                if (_velocidadY <= 0) {
                    if (actualPosition.y <= TargetPosition.y) {
                        return true;
                    }
                } else {
                    if (actualPosition.y >= TargetPosition.y) {
                        return true;
                    }
                }
            } 
        }

        return false;
    }
}
