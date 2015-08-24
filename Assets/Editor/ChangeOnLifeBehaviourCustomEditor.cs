using UnityEngine;
using System.Collections;
using UnityEditor;
using Animators.ChangeOn;

[CustomEditor(typeof(ChangeOnLifeBehaviour))]
public class ChangeOnLifeBehaviourCustomEditor : ChangeOnBehaviourCustomEditor {
    
    private const string _lifeTooltip = "Life value when you want this state to change to another";

    protected override void ExtraGUI() {
        ChangeOnLifeBehaviour myScript = (ChangeOnLifeBehaviour)target;

        EditorGUILayout.Space();
        myScript.Life =
            EditorGUILayout.IntField(
                new GUIContent("Life", _lifeTooltip)
                , myScript.Life
            );
    }
}
