using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {


	public AudioSource pedestrianHitSource;
	public AudioSource bodyBuilderHitSource;
	public AudioSource rockHitSource;
	public AudioSource houseHitSource;
	public AudioSource shonenSource;
	public AudioSource musicSource;
	public AudioSource shonenOpeningSource;
	public AudioSource skierHitSource;

	public static SoundManager instance = null;

	public float lowPitchRange = .95f;
	public float highPitchRange = 1.05f;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);


	}


	public void pauseBGMusic(){
		musicSource.Pause ();
	}


	public void unpauseBGMusic(){
		musicSource.UnPause ();
	}


	public void playPedestrian(){
		RandomizeSfx (pedestrianHitSource);
	}

	public void playSkier(){
		RandomizeSfx (skierHitSource);
	}

	public void playBodyBuilder(){
		RandomizeSfx (bodyBuilderHitSource);
	}

	public void playRock(){
		rockHitSource.Play();
	}

	public void playShonen(){
		shonenSource.Play();
	}

	public void playOpeningShonen(){
		shonenOpeningSource.Play ();
	}

	public void stopShonen(){
		shonenSource.Stop();
	}

	public void playHouse(){
		houseHitSource.Play();
	}

	public void RandomizeSfx(AudioSource clip){
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);
		clip.pitch = randomPitch;
		clip.Play ();

	}



}
