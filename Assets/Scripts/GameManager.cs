using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool gameInProgress = false;

	public FruitChoose fruitChoose;
	public Timer timer;
	public SoundManager soundManager;
	public Animator[] anim = new Animator[2];

	public Player cat;
	public Player dog;

	int winner = -1;
	int loser = -1;

	// Use this for initialization
	void Start () {
		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void StartGame () {
		gameInProgress = true;
		timer.StartTimer ();
		cat.currentFruit = fruitChoose.AssignFruit (0);
		dog.currentFruit = fruitChoose.AssignFruit (1);

		Debug.Log ("Cat: " + cat.currentFruit + ", dog: " + dog.currentFruit);
	}

	IEnumerator FruitObtained (Player player, int num) {
		Debug.Log ("Player" + player + " collected");
		soundManager.PlayCollectFruit (true);
		soundManager.PlayAnimalSpeak (num, SoundManager.SPEECH.RIGHT_FRUIT);
		// play happy anim

		player.cupLevel++;
		float dist = Player.cupHeight/player.maxLevel;	// raise by this height
		float speed = dist / 0.5f;
		while (player.cursor.transform.position.y < Player.fullPos) {
			player.cursor.transform.position = new Vector3(player.cursor.transform.position.x,
			                                               player.cursor.transform.position.y + speed*Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}

		if (player.cupLevel == player.maxLevel) {	
			yield return new WaitForSeconds (3f);	// change this to right fruit sound duration

			soundManager.PlayBlender();
			soundManager.PlayAnimalSpeak(num, SoundManager.SPEECH.CUP_FILLED);

			// cup empty anim

			while (player.cursor.transform.position.y > Player.emptyPos) {
				player.cursor.transform.position = new Vector3(player.cursor.transform.position.x,
				                                               player.cursor.transform.position.y - speed*Time.deltaTime);
				yield return new WaitForEndOfFrame();
			}

			// cupcount increase anim

			player.cupLevel = 0;
			player.score++;

			yield return new WaitForSeconds(3f);	// change this to animal speak duration
		}
		player.currentFruit = fruitChoose.AssignFruit (num);
		Debug.Log ("Cat: " + cat.currentFruit + ", dog: " + dog.currentFruit);
	}

	public void ReceiveInput (int fruit) {
		if (gameInProgress) {			
			Debug.Log("fruit: " + fruit + ", cat.currentFruit: " + cat.currentFruit);
			if (fruit == cat.currentFruit) {
				StartCoroutine (FruitObtained (cat, 0));
			} else if (fruit == dog.currentFruit) {
				StartCoroutine (FruitObtained (dog, 1));
			} else {
				soundManager.PlayCollectFruit(false);
				// punish players
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
