using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplashScreenController : MonoBehaviour {

    public bool FadeFromBlack = true;
    [Space(10)]
    public float WaitingTimeBeforeFadeIn;
    public float FadeInTime;
    [Space(10)]
    public float WaitingTimeBeforeFadeOut;
    public float FadeOutTime;

    private float _waitingTimeFadeInCounter = 0.0f;
    private float _waitingTimeFadeOutCounter = 0.0f;

    private float _fadeInFactor;
    private float _fadeInColor;

    private float _fadeOutFactor;
    private float _fadeOutColor;

    private List<SpriteRenderer> _childRenders = new List<SpriteRenderer>();
	
	void Start () {
        if (FadeFromBlack) {
            this.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        } else {
            this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        _childRenders.Add(this.GetComponent<SpriteRenderer>());

        SpriteRenderer[] childs = this.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < childs.Length; i++) {
            if (FadeFromBlack) {
                childs[i].color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            } else {
                childs[i].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
            _childRenders.Add(childs[i]);
        }

        if (FadeInTime > 0.0f) {
            _fadeInFactor = 1.0f / FadeInTime;
            _fadeInColor = 0.0f;
        }

        if (FadeOutTime > 0.0f) {
            _fadeOutFactor = 1.0f / FadeOutTime;
            _fadeOutColor = 1.0f;
        }
	}
	
	void Update () {

        _waitingTimeFadeInCounter += Time.deltaTime;

        if (_waitingTimeFadeInCounter >= WaitingTimeBeforeFadeIn) {
            if (FadeInTime > 0.0f && _fadeInColor < 1.0f) {
                _fadeInColor += _fadeInFactor;
                _fadeInColor = Mathf.Clamp(_fadeInColor, 0.0f, 1.0f);

                for (int i = 0; i < _childRenders.Count; i++) {
                    if (FadeFromBlack) {
                        _childRenders[i].color = new Color(_fadeInColor, _fadeInColor, _fadeInColor, 1f);
                    } else {
                        _childRenders[i].color = new Color(1.0f, 1.0f, 1.0f, _fadeInColor);
                    }                    
                }
            } else {
                _waitingTimeFadeOutCounter += Time.deltaTime;

                if (_waitingTimeFadeOutCounter >= WaitingTimeBeforeFadeOut) {
                    if (FadeOutTime > 0.0f && _fadeOutColor > 0) {
                        _fadeOutColor -= _fadeOutFactor;
                        _fadeOutColor = Mathf.Clamp(_fadeOutColor, 0.0f, 1.0f);

                        for (int i = 0; i < _childRenders.Count; i++) {
                            if (FadeFromBlack) {
                                _childRenders[i].color = new Color(_fadeOutColor, _fadeOutColor, _fadeOutColor, 1f);
                            } else {
                                _childRenders[i].color = new Color(1.0f, 1.0f, 1.0f, _fadeOutColor);
                            }
                        }
                    } else {
                        if (FadeOutTime > 0.0f) {
                            Destroy(this.gameObject);
                        }
                    }
                }
            }
        }
	}
}
