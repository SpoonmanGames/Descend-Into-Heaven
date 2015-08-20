using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSelectorController : MonoBehaviour {

    [Header("General Setup")]
    public List<Vector2> OptionPositions;
    public string SceneToLoad;
    public BulletController TitleCreditScreen;
    public SplashScreenController MenuOptions;
    public GameObject TransitionToScene;

    [Header("Audio Setup")]
    public AudioClip moveSelectorSound;
    public AudioClip pressSelectorSound;
    public AudioSource selectorSource;

    private int _actualPosition = 0;
    private Vector2 _titleOriginalPosition;
    private Vector2 _titleOriginalTargetPosition;
    private Vector2 _creditTargetPosition;
    private bool _returningFromCredits = false;
    private bool _returningToTitle = false;
    private bool _loadingScreen = false;
    private float _timeToLoadCounter = 0.0f;
    private float _timeToLoad = 1.0f;
    private GameObject _transition;

    private bool _axisUp = false;
    private bool _axisDown = false;
    private bool _axisEnter = false;

    void Start() {
        selectorSource = GetComponent<AudioSource>();
        _creditTargetPosition = new Vector2(0.0f, -5.5f);
        _titleOriginalPosition = new Vector2(0.0f, 2.0f);
        _titleOriginalTargetPosition = new Vector2(0.0f, 0.354f);
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
        if (!_returningFromCredits && !_returningToTitle) {
            if (!_loadingScreen) {
                if (!_axisUp && Input.GetAxis("Up") == 1.0f) {
                    _actualPosition--;
                    _axisUp = true;
                    selectorSource.PlayOneShot(moveSelectorSound, 1F);
                } else if (!_axisDown && Input.GetAxis("Down") == 1.0f) {
                    _actualPosition++;
                    _axisDown = true;
                    selectorSource.PlayOneShot(moveSelectorSound, 1F);
                } else if (!_axisEnter && Input.GetAxis("Submit") == 1.0f) {
                    _axisEnter = true;
                    ActionInMenu();
                }

                if (_axisUp && Input.GetAxis("Up") < 1.0f) {
                    _axisUp = false;
                }
                
                if (_axisDown && Input.GetAxis("Down") < 1.0f) {
                    _axisDown = false;
                }
                
                if (_axisEnter && Input.GetAxis("Submit") < 1.0f) {
                    _axisEnter = false;
                }
            } else {
                _timeToLoadCounter += Time.deltaTime;

                if (_timeToLoadCounter >= _timeToLoad) {
                    Application.LoadLevel(SceneToLoad);
                }
            }            
        } else if (_returningFromCredits) {
            if (TitleCreditScreen.transform.position.y == _creditTargetPosition.y) {
                TitleCreditScreen.transform.position = _titleOriginalPosition;
                TitleCreditScreen.TargetPosition = _titleOriginalTargetPosition;
                _returningFromCredits = false;
            }
        } else if (_returningToTitle) {
            if (TitleCreditScreen.transform.position.y == _titleOriginalTargetPosition.y) {
                this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                MenuOptions.ShowNow();
                _returningToTitle = false;
            }
        }  
	}

    private void ActionInMenu() {

        selectorSource.PlayOneShot(pressSelectorSound,1F);

        if (_actualPosition == 0) {
            _transition = Instantiate(TransitionToScene, Vector3.zero, Quaternion.identity) as GameObject;
            _transition.GetComponent<Animator>().SetFloat("Speed", -1.0f);
            _transition.GetComponent<Animator>().Play("LoadingTransition", 0, 1.0f);
            _loadingScreen = true;            
        } else if (_actualPosition == 1) {
            TitleCreditScreen.TargetPosition = _creditTargetPosition;
            this.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            MenuOptions.FadeFromBlack = false;
            MenuOptions.WaitingTimeBeforeFadeIn = 30.0f;
            MenuOptions.FadeInTime = 0.1f;
            MenuOptions.WaitingTimeBeforeFadeOut = 0.0f;
            MenuOptions.FadeOutTime = 0.0f;
            MenuOptions.StartSplash();
            _returningFromCredits = true;
            _returningToTitle = true;
        } else if (_actualPosition == 2) {
            Application.Quit();
        }
    }
}
