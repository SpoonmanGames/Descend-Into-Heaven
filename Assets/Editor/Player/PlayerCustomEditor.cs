using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

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

        EditorGUILayout.PropertyField(Life);
        EditorGUILayout.PropertyField(AttackDamage);

        EditorGUILayout.Space(); EditorGUILayout.Space();

        EditorGUILayout.PropertyField(WalkingSpeed);
        EditorGUILayout.PropertyField(JumpForce);
        EditorGUILayout.PropertyField(AirControl);
        EditorGUILayout.PropertyField(UseGroundAndCeilingDetection);

        if (UseGroundAndCeilingDetection.boolValue) {

            EditorGUILayout.Space(); EditorGUILayout.Space();
            EditorGUILayout.LabelField("Player Ground and Ceiling Setup", EditorStyles.boldLabel);

            EditorGUILayout.HelpBox(_groundAndCeilingWarning, MessageType.Warning);
            EditorGUILayout.HelpBox(_groundAndCeilingInfo, MessageType.Info);
            
            EditorGUILayout.PropertyField(WhatIsGround);
            EditorGUILayout.PropertyField(GroundedRadius);
            EditorGUILayout.PropertyField(CeilingRadius);

            if (GroundedRadius.floatValue <= 0 || CeilingRadius.floatValue <= 0) {
                EditorGUILayout.HelpBox(_groundAndCeilingError, MessageType.Info);
            }
        }

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Animator Setup", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox(_animatorInfo, MessageType.Info);

        EditorGUILayout.PropertyField(StateVariableName);
        EditorGUILayout.PropertyField(SpeedVariableName);
        EditorGUILayout.PropertyField(vSpeedVariableName);
        EditorGUILayout.PropertyField(GroundVariableName);

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Audio Setup", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox(_audioWarning, MessageType.Warning);
        EditorGUILayout.HelpBox(_audioInfo, MessageType.Info);


        EditorGUILayout.PropertyField(SoundIdle);
        EditorGUILayout.PropertyField(SoundWalking);
        EditorGUILayout.PropertyField(SoundAttacking);
        EditorGUILayout.PropertyField(SoundJumping);
        EditorGUILayout.PropertyField(SoundDead);
        EditorGUILayout.PropertyField(SoundVictory);
        EditorGUILayout.PropertyField(SoundHurt);
        EditorGUILayout.PropertyField(SoundIdleAir);

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Debug Setup", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox(_debugInfo, MessageType.Info);

        EditorGUILayout.PropertyField(EditorDebugMode);

        myScript.ApplyModifiedProperties();

        ExtraGUI();
    }

    public abstract void ExtraGUI();

}
