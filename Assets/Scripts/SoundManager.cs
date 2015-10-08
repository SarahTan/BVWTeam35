using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip[] bgmClips = new AudioClip[2];
	public AudioSource bgm;

	public AudioClip correctFruit;
	public AudioClip wrongFruit;
	public AudioClip blendFruit;
	public AudioSource collectFruit;

	public AudioClip[] catSpeech = new AudioClip[4];
	public AudioClip[] dogSpeech = new AudioClip[4];
	public AudioSource animalSpeech;

	float fadeSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		bgm.clip = bgmClips[0];
		bgm.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayCollectFruit (bool correct) {
		if (correct) {
			collectFruit.clip = correctFruit;
		} else {
			collectFruit.clip = wrongFruit;
		}
		collectFruit.Play ();
	}

	public void PlayBlender () {
		collectFruit.clip = blendFruit;
		collectFruit.Play ();
	}

	public void PlayAnimalSpeak (int animal, int speech) {
		if (animal == 0) {
			animalSpeech.clip = catSpeech[speech];
		} else if (animal == 1) {
			animalSpeech.clip = dogSpeech[speech];
		}
		animalSpeech.Play ();
	}

	public void PlayEndGame () {
		StartCoroutine ("EndGame");
	}

	IEnumerator EndGame () {
		while (bgm.volume > 0) {
			bgm.volume -= fadeSpeed * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		bgm.clip = bgmClips[1];
		bgm.Play ();

		while (bgm.volume < 1) {
			bgm.volume += fadeSpeed * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

}
