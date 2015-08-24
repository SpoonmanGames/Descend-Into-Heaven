using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DestroyOnTimeBehaviour))]
public class DestroyOnTimeBehaviourCustomEditor : Editor {

    private const string _speedTooltip = "You must have a variable called 'Speed' in your machine state to this to work";

    public override void OnInspectorGUI() {
        DestroyOnTimeBehaviour myScript = (DestroyOnTimeBehaviour)target;

        EditorGUILayout.Space();
        myScript.DependsOnSpeed =
            EditorGUILayout.Toggle(new GUIContent("Depends on Speed?", _speedTooltip), myScript.DependsOnSpeed);

        if (myScript.DependsOnSpeed) {
            myScript.SpeedVariableName =
                EditorGUILayout.TextField("Speed Variable Name", myScript.SpeedVariableName);
            myScript.Speed =
                EditorGUILayout.Slider("Speed", myScript.Speed, -1.0f, 1.0f);
            myScript.WhenChange =
                EditorGUILayout.Slider("When Change", myScript.WhenChange, 0.0f, 1.0f);
        }
    }	
}
