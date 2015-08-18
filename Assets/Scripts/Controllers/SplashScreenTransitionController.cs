﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplashScreenTransitionController : MonoBehaviour {
    
    public float WaitingTimeBeforeFadeIn;
    public GameObject TransitionUsedToFadeIn;
    [Space(10)]
    public float WaitingTimeBeforeFadeOut;
    public GameObject TransitionUsedToFadeOut;
    public float DeadOffSet = 0.0f;

    private float _waitingTimeFadeInCounter = 0.0f;
    private bool _alreadyIn = false;
    private bool _speedSet = false;
    private float _waitingTimeFadeOutCounter = 0.0f;
    private bool _alreadyOut = false;

    private GameObject _transitionIn;
    private GameObject _transitionOut;

    private List<SpriteRenderer> _childRenders = new List<SpriteRenderer>();
    private bool _isOut = false;

    private float _deadTimeCounter;
    private float _deadTime;

    void Start() {
        _childRenders.Add(this.GetComponent<SpriteRenderer>());

        SpriteRenderer[] childs = this.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < childs.Length; i++) {
            childs[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            _childRenders.Add(childs[i]);
        }

        _deadTime = _waitingTimeFadeInCounter + _waitingTimeFadeOutCounter + DeadOffSet;
    }

    void Update() {
        _waitingTimeFadeInCounter += Time.deltaTime;

        if(!_alreadyIn){
            _transitionIn = Instantiate(TransitionUsedToFadeIn, this.transform.position, this.transform.rotation) as GameObject;
            _alreadyIn = true;
        }

        if (_waitingTimeFadeInCounter <= WaitingTimeBeforeFadeIn) {
            _transitionIn.GetComponent<Animator>().SetFloat("Speed", 0.0f);
        } else {
            if (!_speedSet) {
                _transitionIn.GetComponent<Animator>().SetFloat("Speed", 1.0f);
                _speedSet = true;
            }
            _waitingTimeFadeOutCounter += Time.deltaTime;

            if (!_alreadyOut && _waitingTimeFadeOutCounter >= WaitingTimeBeforeFadeOut) {
                _transitionOut = Instantiate(TransitionUsedToFadeOut, this.transform.position, this.transform.rotation) as GameObject;
                _transitionOut.GetComponent<Animator>().SetFloat("Speed", -1.0f);
                _transitionOut.GetComponent<Animator>().Play("LoadingTransition", 0, 1.0f);
                _alreadyOut = true;
            }

            if (!_isOut && _alreadyOut) {
                _deadTimeCounter += Time.deltaTime;

                if (_deadTimeCounter >= _deadTime) {
                    _isOut = true;

                    for (int i = 0; i < _childRenders.Count; i++) {
                        _childRenders[i].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                    }

                    Destroy(_transitionOut.gameObject);
                }
            }
        }
    }
}
