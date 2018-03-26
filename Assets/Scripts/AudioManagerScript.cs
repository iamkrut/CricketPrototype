using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour {

	public static AudioManagerScript instance;

	public GameObject bounceAudioHolder;
	public GameObject batHitAudioHolder;

	private AudioSource bounceAudioSource;
	private AudioSource batHitAudioSource;

	void Awake(){
		instance = this;
	}

	void Start(){
		bounceAudioSource = bounceAudioHolder.GetComponent<AudioSource> ();
		batHitAudioSource = batHitAudioHolder.GetComponent<AudioSource> ();
	}

	// Play the ball bounce audio
	public void PlayBounceAudio(){
		bounceAudioSource.Play ();
	}

	// Play the ball hit by bat audio
	public void PlayBatHitAudio(){
		batHitAudioSource.Play ();
	}
}
