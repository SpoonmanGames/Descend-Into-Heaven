using UnityEngine;
using System.Collections;

public class MusicSceneController : MonoBehaviour {

    public AudioClip MusicToPlay;
	
	void Awake(){
        AudioSource audio = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<AudioSource>();

        if (audio.clip == null) {
            audio.clip = MusicToPlay;
            audio.Play();
        }
	}
}
