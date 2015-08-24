using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SpawnBehaviour))]
public class SpawnBehaviourCustomEditor : Editor {

    private const string _spawnGameObjectTooltip = "GameObject to spawn when the time comes";
    private const string _spawnTimeTooltip = "Percentage (%) of the animation in progress in wich the GameObject will appear.";
    private const string _destroyTimeTooltip = "Percentage (%) of the animation in progress in wich the GameObject will be destroyed.";
    private const string _offsetDueDirectionTooltip = "We assume it will spawn at the right, so if you need it to spawn at the left you must add the correction in Unity units.";

    public override void OnInspectorGUI() {
        SpawnBehaviour myScript = (SpawnBehaviour)target;

        EditorGUILayout.Space();
        myScript.SpawnGameObject = 
            (GameObject)EditorGUILayout.ObjectField(new GUIContent("Spawn Game Object", _spawnGameObjectTooltip), myScript.SpawnGameObject, typeof(GameObject), true);
        myScript.SpawnTime =
            EditorGUILayout.Slider(new GUIContent("Spawn Time", _spawnTimeTooltip), myScript.SpawnTime, 0.0f, 1.0f);

        EditorGUILayout.Space();
        myScript.DependOnThisState = 
            EditorGUILayout.Toggle("Depends on this State?", myScript.DependOnThisState);

        if (myScript.DependOnThisState) {
            myScript.DestroyTime =
                EditorGUILayout.Slider(new GUIContent("Destroy Time", _destroyTimeTooltip), myScript.DestroyTime, 0.0f, 1.0f);
        }

        EditorGUILayout.Space();
        myScript.OffsetDueDirection =
            EditorGUILayout.FloatField(new GUIContent("Offset Due Direction", _offsetDueDirectionTooltip), myScript.OffsetDueDirection);
    }	
}
