using UnityEngine;
using System.Collections;
using UnityEditor;
using Animators.ChangeOn;

[CustomEditor(typeof(ChangeOnBehaviour))]
public abstract class ChangeOnBehaviourCustomEditor : Editor {

    private const string _startingPosition = "Percentage (%) at witch you want the Next State to start.";

    public override void OnInspectorGUI() {
        ChangeOnBehaviour myScript = (ChangeOnBehaviour)target;

        EditorGUILayout.Space();
        myScript.IsPlayer =
            EditorGUILayout.Toggle("Is a Player", myScript.IsPlayer);

        if (myScript.IsPlayer) {
            EditorGUILayout.Space();
            myScript.NextPlayerState =
                (Player.PlayerState)EditorGUILayout.EnumPopup("Next Player State", myScript.NextPlayerState);
        } else {
            EditorGUILayout.Space();
            myScript.StateVariableName =
                EditorGUILayout.TextField("State Variable Name", myScript.StateVariableName);
            myScript.NextStateValue =
                EditorGUILayout.IntField("Next State Value", myScript.NextStateValue);
        }

        myScript.NextStateName =
            EditorGUILayout.TextField("Next State Name", myScript.NextStateName);
        EditorGUILayout.Space();
        myScript.StartingPosition =
            EditorGUILayout.Slider(new GUIContent("Starting Position", _startingPosition), myScript.StartingPosition, 0.0f, 1.0f);
        myScript.SpeedVariableName =
                EditorGUILayout.TextField("Speed Variable Name", myScript.SpeedVariableName);
        myScript.StartingSpeedValue =
            EditorGUILayout.FloatField("Starting Speed Value", myScript.StartingSpeedValue);

        ExtraGUI();
    }

    abstract protected void ExtraGUI();
}
