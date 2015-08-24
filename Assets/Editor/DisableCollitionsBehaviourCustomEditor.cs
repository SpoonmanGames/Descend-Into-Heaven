using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DisableCollitionsBehaviour))]
public class DisableCollitionsBehaviourCustomEditor : Editor {

    private const string _playerRigidBodyTooltip = "Rigid Body of the player used to calculate the Jump Movement";

    public override void OnInspectorGUI() {
        DisableCollitionsBehaviour myScript = (DisableCollitionsBehaviour)target;

        EditorGUILayout.Space();
        myScript.DisableGravity =
            EditorGUILayout.Toggle("Disable Gravity?", myScript.DisableGravity);
    }	
}