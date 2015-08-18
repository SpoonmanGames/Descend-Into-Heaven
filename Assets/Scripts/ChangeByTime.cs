using UnityEngine;
using System.Collections;

public class ChangeByTime : MonoBehaviour {

    public float LifeTime;
    public string StateVariableName;
    public int NextStateVariableValue;

    private float _lifeTimeCounter;
    private bool _changed = false;
		
	void Update () {
        _lifeTimeCounter += Time.deltaTime;

        if (!_changed && _lifeTimeCounter >= LifeTime) {
            this.GetComponent<Animator>().SetInteger(StateVariableName, NextStateVariableValue);
            _changed = true;
        }
	}
}
