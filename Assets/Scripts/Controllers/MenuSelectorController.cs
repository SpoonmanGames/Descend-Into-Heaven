using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSelectorController : MonoBehaviour {

    public List<Vector2> OptionPositions;
    public string SceneToLoad;

    [Header("Audio Setup")]
    public AudioClip moveSelectorSound;
    public AudioClip pressSelectorSound;
    public AudioSource selectorSource;

    private int _actualPosition = 0;

    void Start() {

        selectorSource = GetComponent<AudioSource>();

    }


    void FixedUpdate() {
        if (_actualPosition >= OptionPositions.Count) {
            _actualPosition = 0;
        } else if (_actualPosition < 0) {
            _actualPosition = OptionPositions.Count - 1;
        }

        this.transform.position = OptionPositions[_actualPosition];
    }	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            _actualPosition--;
            selectorSource.PlayOneShot(moveSelectorSound,1F);
        } else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            _actualPosition++;
            selectorSource.PlayOneShot(moveSelectorSound,1F);
        } else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) {
            ActionInMenu();
        }
	}

    private void ActionInMenu() {

        selectorSource.PlayOneShot(pressSelectorSound,1F);

        if (_actualPosition == 0) {
            Application.LoadLevel(SceneToLoad);
        } else if (_actualPosition == 1) {

        } else if (_actualPosition == 2) {
            Application.Quit();
        }
    }
}
