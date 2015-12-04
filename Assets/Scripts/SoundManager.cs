using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	protected SoundManager () {}
	
	public enum SPEECH {RIGHT_FRUIT = 0, CUP_FILLED = 1, WIN = 2, LOSE = 3};
	
	public AudioSource bgm;
	public AudioSource correctFruit;
	public AudioSource wrongFruit;
	public AudioSource blender;
	public AudioSource catSpeech;
	public AudioSource dogSpeech;
	
	AudioClip[] bgmClips = new AudioClip[3];
	AudioClip[] catSpeeches = new AudioClip[4];
	AudioClip[] dogSpeeches = new AudioClip[4];

	float fadeSpeed = 2f;
	float musicIncrease1 = 11f;
	float musicIncrease2 = 54f;
	float musicVol0 = 0.15f;
	float musicVol1 = 0.3f;
	float musicVol2 = 0.7f;
	

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		DontDestroyOnLoad (gameObject);
		InitGame ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitGame () {
		bgmClips = Resources.LoadAll<AudioClip> ("Audio/BGM");
		catSpeeches = Resources.LoadAll<AudioClip> ("Audio/cat");
		dogSpeeches = Resources.LoadAll<AudioClip> ("Audio/dog");
	}

	// Called by StartScene
	public void ResetGame () {
		wrongFruit.volume = musicVol0;
		correctFruit.volume = 0.5f;
		StartCoroutine (FadeBGM (0));
	}

	public void PlayCollectFruit (bool correct) {
		if (correct) {
			correctFruit.Play();
		} else {
			wrongFruit.Play();
		}
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

	// Called by GM
	public void PlayEndGame () {
		StartCoroutine (FadeBGM (2));
	}

	// Called by GM
	public void PlayStartGame () {
		StartCoroutine (FadeBGM (1));

		StartCoroutine (ChangeSoundVol (wrongFruit, musicIncrease1, musicVol1));
		StartCoroutine (ChangeSoundVol (correctFruit, musicIncrease1, 0.8f));

		StartCoroutine (ChangeSoundVol (wrongFruit, musicIncrease2, musicVol2));
		StartCoroutine (ChangeSoundVol (correctFruit, musicIncrease1, 1f));
	}

	IEnumerator ChangeSoundVol (AudioSource source, float time, float vol) {
		yield return new WaitForSeconds (time);
		source.volume = vol;
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
