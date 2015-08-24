using UnityEngine;
using System.Collections;
using UnityEditor;
using Animators.ChangeOn;

[CustomEditor(typeof(ChangeOnAirGroundBehaviour))]
public class ChangeOnAirGroundBehaviourCustomEditor : ChangeOnBehaviourCustomEditor {

    protected override void ExtraGUI() {
        ChangeOnAirGroundBehaviour myScript = (ChangeOnAirGroundBehaviour)target;

        myScript.GroundVariableName =
            EditorGUILayout.TextField("Ground Variable Name", myScript.GroundVariableName);

        myScript.ChangeOnAir =
            EditorGUILayout.Toggle("Change On Air?", myScript.ChangeOnAir);

        myScript.ChangeOnGround =
            EditorGUILayout.Toggle("Change On Ground?", myScript.ChangeOnGround);
    }
}