using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    public int Daño = 1;
    public float Tiempo = 1;
    public bool DestruirAlTerminar = false;
    
    [HideInInspector]
    public Player.Player PlayerController;
    [HideInInspector]
    public float Direccion = -1;
    [HideInInspector]
    public Vector2 TargetPosition;

    private float _velocidadX;
    private float _velocidadY;

    void Start() {
        _velocidadX = (this.transform.position.x - TargetPosition.x) / Tiempo;
        _velocidadY = (this.transform.position.y - TargetPosition.y) / Tiempo;

        if (Direccion == 1) {
            _velocidadX *= -1;
            _velocidadY *= -1;
        }
    }

    void LateUpdate(){
        Vector3 newPosition;

        newPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        newPosition += new Vector3(_velocidadX * Time.deltaTime * Direccion, _velocidadY * Time.deltaTime * Direccion, 0.0f);

        this.transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            PlayerController.Life--;
        }
    }
    
}
