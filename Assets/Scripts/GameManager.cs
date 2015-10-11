﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool gameInProgress = false;
	public bool halfWayMark = false;

	public FruitChoose fruitChoose;
	public Timer timer;
	public SoundManager soundManager;
	public Animator[] anim = new Animator[2];

	public GameObject[] badArrows = new GameObject[2];
	public GameObject badFruitsParent;

	public Player cat;
	public Player dog;

	int winner = -1;
	int loser = -1;
	
	SpriteRenderer[] badFruits = new SpriteRenderer[12];
	int badFruit;
	float badFruitTime = 5f;
	int saboAnimal;

	Color transparent = new Color (1f, 1f, 1f, 0f);

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 12; i++) {
			badFruits[i] = badFruitsParent.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
		}
		StartGame ();
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

		Debug.Log ("Cat: " + cat.currentFruit + ", dog: " + dog.currentFruit);
	}

	void FruitObtained (Player player) {
		Debug.Log ("Player" + player + " collected");
		soundManager.PlayCollectFruit (true);
		soundManager.PlayAnimalSpeak (player.animalNum, SoundManager.SPEECH.RIGHT_FRUIT);
		anim [player.animalNum].SetTrigger ("Happy");

		player.ChangeCupLevel(true);

		// switch to next cup
		if (player.cupLevel == player.maxCupLevel) {	
			StartCoroutine(ChangeCups(player));
		}
		
		player.SetCurrentFruit(fruitChoose.AssignFruit (player.animalNum));
		Debug.Log ("Cat: " + cat.currentFruit + ", dog: " + dog.currentFruit);		
	}
	
	
	void ToggleBadArrow (bool on, Color color) {
		badArrows [saboAnimal].SetActive (on);
		badFruits [badFruit].color = color;
	}


	IEnumerator ChangeCups (Player player) {
		yield return new WaitForSeconds (1f);	// change this to right fruit sound duration
		
		soundManager.PlayBlender();
		soundManager.PlayAnimalSpeak(player.animalNum, SoundManager.SPEECH.CUP_FILLED);
		
		player.ChangeCupLevel (false);
		
		// cupcount increase anim
		
		player.IncreaseScore();
	}


	IEnumerator GetBadFruit () {
		yield return new WaitForSeconds (Random.Range (15, 25));

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
		if (gameInProgress) {
			if (fruit == cat.currentFruit) {
				FruitObtained (cat);

			} else if (fruit == dog.currentFruit) {
				FruitObtained (dog);

			} else if (fruit == badFruit) {
				if (saboAnimal == 0) {
					cat.ChangeCupLevel(false);
				} else {
					dog.ChangeCupLevel(false);
				}
				ToggleBadArrow(false, transparent);
				badFruit = -1;

			} else {
				soundManager.PlayCollectFruit(false);
				cat.ChangeCupLevel(false);
				dog.ChangeCupLevel(false);
			}
		}
	}

	public void TimesUp () {
		gameInProgress = false;
		StopAllCoroutines ();
		soundManager.PlayEndGame ();

		if (cat.score > dog.score) {
			winner = 0;
			loser = 1;
		} else {
			winner = 1;
			loser = 0;
		}

		soundManager.PlayAnimalSpeak(winner, SoundManager.SPEECH.WIN);
		anim [winner].SetBool ("Win", true);
		// you win text

		soundManager.PlayAnimalSpeak(loser, SoundManager.SPEECH.LOSE);
		anim [loser].SetBool ("Win", false);
		// you lose text
	}
}
