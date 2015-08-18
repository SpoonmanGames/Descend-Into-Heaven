using UnityEngine;
using System.Collections;

public class DisableTillFullyShown : MonoBehaviour {

    SpriteRenderer _playerRenderer;
	
	void Start () {
        _playerRenderer = this.GetComponent<SpriteRenderer>();
        this.GetComponent<MenuSelectorController>().enabled = false;
	}
	
	void Update () {
        if (_playerRenderer.color.a == 1.0f && _playerRenderer.color.r == 1.0f) {
            this.GetComponent<MenuSelectorController>().enabled = true;
        }
	}
}
