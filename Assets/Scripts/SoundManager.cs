using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip[] bgmClips = new AudioClip[3];
	public AudioSource bgm;

	public AudioClip correctFruit;
	public AudioClip wrongFruit;
	public AudioSource collectFruit;	
	public AudioSource blender;

	public enum SPEECH {RIGHT_FRUIT = 0, CUP_FILLED = 1, WIN = 2, LOSE = 3};
	public AudioClip[] catSpeeches = new AudioClip[4];
	public AudioClip[] dogSpeeches = new AudioClip[4];
	public AudioSource catSpeech;
	public AudioSource dogSpeech;
	//TODO: 2 SEPARATE SOURCES FOR EACH ANIMAL

	float fadeSpeed = 2f;

	void Awake () {
		DontDestroyOnLoad (gameObject);

	}

	// Use this for initialization
	void Start () {
		StartCoroutine (FadeBGM (0));
		Cursor.visible = false;
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
		blender.Play ();
	}

	public void PlayAnimalSpeak (int animal, SPEECH speech) {
		if (animal == 0) {
			if(speech == SPEECH.LOSE) {
				Debug.Log("playing cat lose");
			}
			catSpeech.clip = catSpeeches[(int)speech];
			catSpeech.Play ();
		} else if (animal == 1) {
			dogSpeech.clip = dogSpeeches[(int)speech];
			dogSpeech.Play();
		}
	}

	public void PlayEndGame () {
		StartCoroutine (FadeBGM (2));
	}

	public void PlayStartGame () {
		StartCoroutine (FadeBGM (1));
	}

	IEnumerator FadeBGM (int clip) {
		while (bgm.volume > 0) {
			bgm.volume -= fadeSpeed * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		bgm.clip = bgmClips[clip];
		bgm.Play ();

		while (bgm.volume < 1) {
			bgm.volume += fadeSpeed * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

}
