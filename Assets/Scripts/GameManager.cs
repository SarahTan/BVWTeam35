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

	IEnumerator FruitObtained (Player player, int num) {
		soundManager.PlayCollectFruit (true);
		soundManager.PlayAnimalSpeak (num, SoundManager.SPEECH.RIGHT_FRUIT);
		// play happy anim

		player.cupLevel++;
		float dist = Player.cupHeight/player.maxLevel;	// raise by this height
		float speed = dist / 0.5f;
		while (player.cursor.transform.position.y < Player.fullPos) {
			player.cursor.transform.position = new Vector3(player.cursor.transform.position.x,
			                                               player.cursor.transform.position.y + speed*Time.deltaTime,
			                                               player.cursor.transform.position.z);
			yield return new WaitForEndOfFrame();
		}

		if (player.cupLevel == player.maxLevel) {	
			yield return new WaitForSeconds (3f);	// change this to right fruit sound duration

			soundManager.PlayBlender();
			soundManager.PlayAnimalSpeak(num, SoundManager.SPEECH.CUP_FILLED);

			// cup empty anim
			// decrease arrow height
			// cupcount increase anim

			player.cupLevel = 0;
			player.score++;

			yield return new WaitForSeconds(3f);	// change this to animal speak duration
		}

		player.currentFruit = fruitChoose.AssignFruit (num);

		yield return null;
	}

	public void ReceiveInput (int fruit) {
		if (fruit == cat.currentFruit) {
			StartCoroutine (FruitObtained (cat, 0));
		} else if (fruit == dog.currentFruit) {
			StartCoroutine (FruitObtained (dog, 1));
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
