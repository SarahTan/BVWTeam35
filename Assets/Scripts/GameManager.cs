using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool gameStarted = false;

	public FruitChoose fruitChoose;
	public Timer timer;
	public SoundManager soundManager;
	public Animator[] anim = new Animator[2];

	public Player cat;
	public Player dog;
	
	int[] playerScores = new int[2];
	int winner = -1;
	int loser = -1;

	// Use this for initialization
	void Start () {
		playerScores [0] = playerScores [1] = 0;
		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void StartGame () {
		gameStarted = true;
		timer.StartTimer ();
		cat.currentFruit = fruitChoose.AssignFruit (0);
		dog.currentFruit = fruitChoose.AssignFruit (1);
	}


	public void FruitObtained (int fruit) {
		if (fruit == cat.currentFruit) {
			cat.score++;
			cat.prevFruit = cat.currentFruit;
			cat.currentFruit = fruitChoose.AssignFruit(0);
		} else if (fruit == dog.currentFruit) {
			dog.score++;
			dog.prevFruit = dog.currentFruit;
			dog.currentFruit = fruitChoose.AssignFruit(1);
		}
	}

	public void TimesUp () {
		gameStarted = false;
		soundManager.PlayEndGame ();

		if (playerScores [0] > playerScores [1]) {
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
