using UnityEngine;
using System.Collections;
using UnityEditor;
using Animators.ChangeOn;

[CustomEditor(typeof(ChangeOnTimeBehaviour))]
public class ChangeOnTimeBehaviourCustomEditor : ChangeOnBehaviourCustomEditor {

    protected override void ExtraGUI() {
        ChangeOnTimeBehaviour myScript = (ChangeOnTimeBehaviour)target;

        myScript.WhenChange =
            EditorGUILayout.Slider("When Change", myScript.WhenChange, 0.0f, 1.0f);
        myScript.WhenSpeedIs =
            EditorGUILayout.Slider("When Speed Is", myScript.WhenSpeedIs, -1.0f, 1.0f);
    }
}
