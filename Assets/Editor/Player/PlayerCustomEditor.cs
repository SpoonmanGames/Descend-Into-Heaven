using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Player.Player))]
public class PlayerCustomEditor : Editor {

    private const string _playerInfo = "Never use the Player Script alone, you must inheritage from another class.";
    private const string _animatorInfo = "If any of this State Name is empty it will not be access by the script.";
    private const string _groundAndCeilingWarning = "This GameObject must have two childs called GroundCheck and CeilingChek with only a transform component for this to work propertly.";
    private const string _groundAndCeilingInfo = "If the debugger setup is on you will se those detection areas as red circles.";
    private const string _audioInfo = "If any audio is empty it will simply won't make any sound for that clip.";
    private const string _audioWarning = "This GameObject must have an AudioSource for the AudioClips to work.";
    private const string _debugInfo = "It will show information on the Scene but not in the actual Game.";

    public override void OnInspectorGUI() {

        Player.Player myScript = (Player.Player)target;

        EditorGUILayout.HelpBox(_playerInfo, MessageType.None);

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Base Setup", EditorStyles.boldLabel);

        myScript.Life =
            EditorGUILayout.IntField("Life", myScript.Life);

        myScript.AttackDamage =
            EditorGUILayout.IntField("Attack Damage", myScript.AttackDamage);

        EditorGUILayout.Space(); EditorGUILayout.Space();
        myScript.WalkingSpeed =
            EditorGUILayout.FloatField("Walking Speed", myScript.WalkingSpeed);

        myScript.JumpForce =
            EditorGUILayout.FloatField("Jump Force", myScript.JumpForce);

        myScript.AirControl =
            EditorGUILayout.Toggle("Air Control?", myScript.AirControl);

        myScript.UseGroundAndCeilingDetection =
            EditorGUILayout.Toggle("Use Ground and Ceiling Detection?", myScript.UseGroundAndCeilingDetection);

        if (myScript.UseGroundAndCeilingDetection) {

            EditorGUILayout.Space(); EditorGUILayout.Space();
            EditorGUILayout.LabelField("Player Ground and Ceiling Setup", EditorStyles.boldLabel);

            EditorGUILayout.HelpBox(_groundAndCeilingWarning, MessageType.Warning);
            EditorGUILayout.HelpBox(_groundAndCeilingInfo, MessageType.Info);

            List<string> layersName = new List<string>();
            GetLayerNames(layersName);

            myScript.WhatIsGround =
                EditorGUILayout.MaskField("What is Ground?", myScript.WhatIsGround, layersName.ToArray());
            
            myScript.GroundedRadius =
                EditorGUILayout.FloatField("Grounded Radius", myScript.GroundedRadius);

            myScript.CeilingRadius =
                EditorGUILayout.FloatField("Ceiling Radius", myScript.CeilingRadius);
        }

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Animator Setup", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox(_animatorInfo, MessageType.Info);

        myScript.StateVariableName =
                EditorGUILayout.TextField("State Variable Name", myScript.StateVariableName);

        myScript.SpeedVariableName =
                EditorGUILayout.TextField("Speed Variable Name", myScript.SpeedVariableName);

        myScript.vSpeedVariableName =
                EditorGUILayout.TextField("Vertical Speed Variable Name", myScript.vSpeedVariableName);

        myScript.GroundVariableName =
                EditorGUILayout.TextField("Ground Variable Name", myScript.GroundVariableName);

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Audio Setup", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox(_audioWarning, MessageType.Warning);
        EditorGUILayout.HelpBox(_audioInfo, MessageType.Info);

        myScript.SoundIdle =
            EditorGUILayout.ObjectField("Sound Idle", myScript.SoundIdle, typeof(AudioClip), false) as AudioClip;
        myScript.SoundWalking =
            EditorGUILayout.ObjectField("Sound Walking", myScript.SoundWalking, typeof(AudioClip), false) as AudioClip;
        myScript.SoundAttacking =
            EditorGUILayout.ObjectField("Sound Attacking", myScript.SoundAttacking, typeof(AudioClip), false) as AudioClip;
        myScript.SoundJumping =
            EditorGUILayout.ObjectField("Sound Jumping", myScript.SoundJumping, typeof(AudioClip), false) as AudioClip;
        myScript.SoundDead =
            EditorGUILayout.ObjectField("Sound Dead", myScript.SoundDead, typeof(AudioClip), false) as AudioClip;
        myScript.SoundVictory =
            EditorGUILayout.ObjectField("Sound Victory", myScript.SoundVictory, typeof(AudioClip), false) as AudioClip;
        myScript.SoundHurt =
            EditorGUILayout.ObjectField("Sound Hurt", myScript.SoundHurt, typeof(AudioClip), false) as AudioClip;
        myScript.SoundIdleAir =
            EditorGUILayout.ObjectField("Sound Idle Air", myScript.SoundIdleAir, typeof(AudioClip), false) as AudioClip;

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Debug Setup", EditorStyles.boldLabel);        

        EditorGUILayout.HelpBox(_debugInfo, MessageType.Info);

        myScript.EditorDebugMode =
            EditorGUILayout.Toggle("Editor Debug Mode?", myScript.EditorDebugMode);
    }

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
