using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool gameInProgress = false;
	public bool halfWayMark = false;

	public FruitChoose fruitChoose;
	public Timer timer;
	public Animator[] anim = new Animator[2];

	public GameObject[] badArrows = new GameObject[2];
	public GameObject badFruitsParent;

	public Player cat;
	public Player dog;

	public GameObject creditsButton;

	SoundManager soundManager;
	SpriteRenderer[] badFruits = new SpriteRenderer[12];
	int badFruit;
	float badFruitTime = 5f;
	int saboAnimal;
	bool readyForCredits = false;

	Color transparent = new Color (1f, 1f, 1f, 0f);

	// Use this for initialization
	void Start () {
		soundManager = GameObject.Find ("SoundManager(Clone)").GetComponent<SoundManager>();
		soundManager.PlayStartGame ();
		for (int i = 0; i < 12; i++) {
			badFruits[i] = badFruitsParent.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
		}
		Invoke ("StartGame", 3f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void StartGame () {
		gameInProgress = true;
		timer.StartTimer ();
		cat.SetCurrentFruit(fruitChoose.AssignFruit (0));
		dog.SetCurrentFruit(fruitChoose.AssignFruit (1));

		StartCoroutine (GetBadFruit());
		StartCoroutine (AnimalMove ());

		Debug.Log ("Cat: " + cat.currentFruit + ", dog: " + dog.currentFruit);
	}

	void FruitObtained (Player player) {
		soundManager.PlayCollectFruit (true);
		soundManager.PlayAnimalSpeak (player.animalNum, SoundManager.SPEECH.RIGHT_FRUIT);
		anim [player.animalNum].SetTrigger ("Happy");
		player.ChangeCupLevel(true);

		// switch to next cup
		if (player.cupLevel == player.maxCupLevel) {	
			StartCoroutine (ChangeCups (player));
		} else {		
			player.SetCurrentFruit (fruitChoose.AssignFruit (player.animalNum));
			Debug.Log ("Cat: " + cat.currentFruit + ", dog: " + dog.currentFruit);	
		}
	}
	
	
	void ToggleBadArrow (bool on, Color color) {
		badArrows [saboAnimal].SetActive (on);
		badFruits [badFruit].color = color;
	}

	IEnumerator AnimalMove () {
		while (gameInProgress) {
			yield return new WaitForSeconds (Random.Range (1f, 2f));

			if (Random.Range (0, 2) == 0) {
				anim [Random.Range (0, 2)].SetTrigger ("Move");
			} else {
				anim [Random.Range (0, 2)].SetTrigger ("Blink");
			}
		}
	}

	IEnumerator ChangeCups (Player player) {
		soundManager.PlayBlender();
		soundManager.PlayAnimalSpeak(player.animalNum, SoundManager.SPEECH.CUP_FILLED);

		yield return new WaitForSeconds (1f);	// change this to right fruit sound duration

		player.ChangeCupLevel (false);		
		player.IncreaseScore();
		player.SetCurrentFruit (fruitChoose.AssignFruit (player.animalNum));

		Debug.Log ("Cat: " + cat.currentFruit + ", dog: " + dog.currentFruit);	
	}


	IEnumerator GetBadFruit () {
		yield return new WaitForSeconds (Random.Range (8, 15));

		while (gameInProgress) {
			saboAnimal = Random.Range (0, 2);
			badFruit = fruitChoose.AssignBadFruit ();
			Debug.Log("Bad fruit: " + badFruit);
			ToggleBadArrow (true, Color.white);
			yield return new WaitForSeconds (badFruitTime);

			// Nobody collected it
			if (badFruit != -1) {
				ToggleBadArrow (false, transparent);
			}
			if (halfWayMark) {
				yield return new WaitForSeconds (Random.Range (3, 5));
			} else {
				yield return new WaitForSeconds (Random.Range (7, 10));
			}
		}
	}


	public void ReceiveInput (int fruit) {
		Debug.Log ("Fruit: " + fruit);
		if (gameInProgress) {
			if (fruit == cat.currentFruit && !cat.blending) {
				FruitObtained (cat);

			} else if (fruit == dog.currentFruit && !dog.blending) {
				FruitObtained (dog);

			} else if (fruit == badFruit) {
				if (saboAnimal == 0) {
					cat.ChangeCupLevel (false);
				} else {
					dog.ChangeCupLevel (false);
				}
				ToggleBadArrow (false, transparent);
				badFruit = -1;

			} else {
				soundManager.PlayCollectFruit (false);
				//cat.ChangeCupLevel(false);
				//dog.ChangeCupLevel(false);
			}
		} else if (readyForCredits) {
			// banana
			if (fruit == 2) {
				Application.LoadLevel ("Credit");
			}
		}
	}

	public void TimesUp () {
		gameInProgress = false;
		StopAllCoroutines ();
		StartCoroutine (EndScene ());

	}

	IEnumerator EndScene () {
		yield return new WaitForSeconds (1f);
		soundManager.PlayEndGame ();

		if (cat.score > dog.score) {
			cat.winner = true;
			soundManager.PlayAnimalSpeak(0, SoundManager.SPEECH.WIN);
			soundManager.PlayAnimalSpeak(1, SoundManager.SPEECH.LOSE);
			anim[0].SetBool("Win", true);
			anim[1].SetBool("Lose", true);
		} else if (cat.score < dog.score) {
			dog.winner = true;
			soundManager.PlayAnimalSpeak(0, SoundManager.SPEECH.LOSE);
			soundManager.PlayAnimalSpeak(1, SoundManager.SPEECH.WIN);
			anim[0].SetBool("Lose", true);
			anim[1].SetBool("Win", true);
		} else {
			cat.winner = true;
			dog.winner = true;
			soundManager.PlayAnimalSpeak(0, SoundManager.SPEECH.WIN);
			soundManager.PlayAnimalSpeak(1, SoundManager.SPEECH.WIN);			
			anim[0].SetBool("Win", true);
			anim[1].SetBool("Win", true);
		}

		timer.RemoveTimer ();
		cat.EndGame ();
		dog.EndGame ();
		
		yield return new WaitForSeconds (3f);
		foreach (Transform child in transform) {
			child.gameObject.SetActive(false);
		}
		creditsButton.SetActive (true);
		readyForCredits = true;
	}
}
