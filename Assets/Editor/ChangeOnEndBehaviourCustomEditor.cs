using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ChangeOnEndBehaviour))]
public class ChangeOnEndBehaviourCustomEditor : Editor {
    public Player.PlayerState playerStateOption;


    public override void OnInspectorGUI() {
        ChangeOnEndBehaviour myScript = (ChangeOnEndBehaviour)target;

        EditorGUILayout.Space();
        myScript.StateVariableName = EditorGUILayout.TextField("State Variable Name", myScript.StateVariableName);

        EditorGUILayout.Space();
        myScript.IsPlayer = EditorGUILayout.Toggle("Is a Player", myScript.IsPlayer);

        if (myScript.IsPlayer) {
            myScript.NextPlayerState = (Player.PlayerState)EditorGUILayout.EnumPopup("Next Player State", playerStateOption);
        } else {
            myScript.NextStateValue = EditorGUILayout.IntField("Next State Value", myScript.NextStateValue);
        }

        myScript.NextStateName = EditorGUILayout.TextField("State Variable Name", myScript.NextStateName);
        EditorGUILayout.Space();

        myScript.StartingPosition = EditorGUILayout.Slider("Starting Position", myScript.StartingPosition, 0.0f, 1.0f);
        myScript.WhenChange = EditorGUILayout.Slider("When Change", myScript.WhenChange, 0.0f, 1.0f);
    }
}
