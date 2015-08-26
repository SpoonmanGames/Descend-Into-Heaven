using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using CustomPlugins;

[CustomEditor(typeof(Player.Player))]
public abstract class PlayerCustomEditor : Editor {

    private const string _playerInfo = "Never use the Player Script alone, you must inheritage from another class.";
    private const string _animatorInfo = "If any of this State Name is empty it will not be access by the script.";
    private const string _groundAndCeilingWarning = "This GameObject must have two childs called GroundCheck and CeilingChek with only a transform component for this to work propertly.";
    private const string _groundAndCeilingInfo = "If the debugger setup is on, you will se those detection areas as red circles.";
    private const string _groundAndCeilingError = "The Ground and Ceiling values must be bigger than 0 if you are using this kind of detection.";
    private const string _audioInfo = "If any audio is empty it will simply won't make any sound for that clip.";
    private const string _audioWarning = "This GameObject must have an AudioSource for the AudioClips to work.";
    private const string _debugInfo = "It will show information on the Scene but not in the actual Game.";

    public override void OnInspectorGUI() {

        SerializedObject myScript = new SerializedObject(target);
        SerializedProperty Life = myScript.FindProperty("Life");
        SerializedProperty AttackDamage = myScript.FindProperty("AttackDamage");
        SerializedProperty WalkingSpeed = myScript.FindProperty("WalkingSpeed");
        SerializedProperty JumpForce = myScript.FindProperty("JumpForce");
        SerializedProperty AirControl = myScript.FindProperty("AirControl");
        SerializedProperty UseGroundAndCeilingDetection = myScript.FindProperty("UseGroundAndCeilingDetection");
        SerializedProperty WhatIsGround = myScript.FindProperty("WhatIsGround");
        SerializedProperty GroundedRadius = myScript.FindProperty("GroundedRadius");
        SerializedProperty CeilingRadius = myScript.FindProperty("CeilingRadius");
        SerializedProperty StateVariableName = myScript.FindProperty("StateVariableName");
        SerializedProperty SpeedVariableName = myScript.FindProperty("SpeedVariableName");
        SerializedProperty vSpeedVariableName = myScript.FindProperty("vSpeedVariableName");
        SerializedProperty GroundVariableName = myScript.FindProperty("GroundVariableName");
        SerializedProperty SoundIdle = myScript.FindProperty("SoundIdle");
        SerializedProperty SoundWalking = myScript.FindProperty("SoundWalking");
        SerializedProperty SoundAttacking = myScript.FindProperty("SoundAttacking");
        SerializedProperty SoundJumping = myScript.FindProperty("SoundJumping");
        SerializedProperty SoundDead = myScript.FindProperty("SoundDead");
        SerializedProperty SoundVictory = myScript.FindProperty("SoundVictory");
        SerializedProperty SoundHurt = myScript.FindProperty("SoundHurt");
        SerializedProperty SoundIdleAir = myScript.FindProperty("SoundIdleAir");
        SerializedProperty EditorDebugMode = myScript.FindProperty("EditorDebugMode");

        EditorGUILayout.HelpBox(_playerInfo, MessageType.None);

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Base Setup", EditorStyles.boldLabel);

        Life.intValue =
            EditorGUILayout.IntField("Life", Life.intValue);

        AttackDamage.intValue =
            EditorGUILayout.IntField("Attack Damage", AttackDamage.intValue);

        EditorGUILayout.Space(); EditorGUILayout.Space();
        WalkingSpeed.floatValue =
            EditorGUILayout.FloatField("Walking Speed", WalkingSpeed.floatValue);

        JumpForce.floatValue =
            EditorGUILayout.FloatField("Jump Force", JumpForce.floatValue);

        AirControl.boolValue =
            EditorGUILayout.Toggle("Air Control?", AirControl.boolValue);

        UseGroundAndCeilingDetection.boolValue =
            EditorGUILayout.Toggle("Use Ground and Ceiling Detection?", UseGroundAndCeilingDetection.boolValue);

        if (UseGroundAndCeilingDetection.boolValue) {

            EditorGUILayout.Space(); EditorGUILayout.Space();
            EditorGUILayout.LabelField("Player Ground and Ceiling Setup", EditorStyles.boldLabel);

            EditorGUILayout.HelpBox(_groundAndCeilingWarning, MessageType.Warning);
            EditorGUILayout.HelpBox(_groundAndCeilingInfo, MessageType.Info);

            List<string> layersName = new List<string>();
            GetLayerNames(layersName);

            WhatIsGround.intValue =
                EditorGUILayout.MaskField("What is Ground?", WhatIsGround.intValue, layersName.ToArray());

            GroundedRadius.floatValue =
                EditorGUILayout.FloatField("Grounded Radius", GroundedRadius.floatValue);

            CeilingRadius.floatValue =
                EditorGUILayout.FloatField("Ceiling Radius", CeilingRadius.floatValue);

            if (GroundedRadius.floatValue <= 0 || CeilingRadius.floatValue <= 0) {
                EditorGUILayout.HelpBox(_groundAndCeilingError, MessageType.Info);
            }
        }

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Animator Setup", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox(_animatorInfo, MessageType.Info);

        StateVariableName.stringValue =
                EditorGUILayout.TextField("State Variable Name", StateVariableName.stringValue);

        SpeedVariableName.stringValue =
                EditorGUILayout.TextField("Speed Variable Name", SpeedVariableName.stringValue);

        vSpeedVariableName.stringValue =
                EditorGUILayout.TextField("Vertical Speed Variable Name", vSpeedVariableName.stringValue);

        GroundVariableName.stringValue =
                EditorGUILayout.TextField("Ground Variable Name", GroundVariableName.stringValue);

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Audio Setup", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox(_audioWarning, MessageType.Warning);
        EditorGUILayout.HelpBox(_audioInfo, MessageType.Info);

        SoundIdle.objectReferenceValue =
            EditorGUILayout.ObjectField("Sound Idle", SoundIdle.objectReferenceValue, typeof(AudioClip), false) as AudioClip;
        SoundWalking.objectReferenceValue =
            EditorGUILayout.ObjectField("Sound Walking", SoundWalking.objectReferenceValue, typeof(AudioClip), false) as AudioClip;
        SoundAttacking.objectReferenceValue =
            EditorGUILayout.ObjectField("Sound Attacking", SoundAttacking.objectReferenceValue, typeof(AudioClip), false) as AudioClip;
        SoundJumping.objectReferenceValue =
            EditorGUILayout.ObjectField("Sound Jumping", SoundJumping.objectReferenceValue, typeof(AudioClip), false) as AudioClip;
        SoundDead.objectReferenceValue =
            EditorGUILayout.ObjectField("Sound Dead", SoundDead.objectReferenceValue, typeof(AudioClip), false) as AudioClip;
        SoundVictory.objectReferenceValue =
            EditorGUILayout.ObjectField("Sound Victory", SoundVictory.objectReferenceValue, typeof(AudioClip), false) as AudioClip;
        SoundHurt.objectReferenceValue =
            EditorGUILayout.ObjectField("Sound Hurt", SoundHurt.objectReferenceValue, typeof(AudioClip), false) as AudioClip;
        SoundIdleAir.objectReferenceValue =
            EditorGUILayout.ObjectField("Sound Idle Air", SoundIdleAir.objectReferenceValue, typeof(AudioClip), false) as AudioClip;

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Debug Setup", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox(_debugInfo, MessageType.Info);

        EditorDebugMode.boolValue =
            EditorGUILayout.Toggle("Editor Debug Mode?", EditorDebugMode.boolValue);

        ExtraGUI();
    }

    public abstract void ExtraGUI();

    private void GetLayerNames(List<string> layersName) {
        string actualName;

        for (int i = 0; i < 31; i++) {
            actualName = LayerMask.LayerToName(i);

            if (actualName.Length > 0) {
                layersName.Add(actualName);
            }
        }

    }
}
