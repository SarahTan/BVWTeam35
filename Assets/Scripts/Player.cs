using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static float cupHeight = 7.5f;
	public static float fullPos = 11.5f;
	public static float emptyPos = 4f;

	public int animalNum;
	public float nextPos;
	public int score = 0;
	public int currentFruit = -1;
	public int cupLevel = 0;
	public float maxCupLevel = 3;
	public GameObject cursor;
	public GameObject wholeFruits;

	public GameObject fruitLevel3;
	public GameObject fruitLevel4; 
	public GameObject fruitLevel5;

	ArrayList choppedFruitsList = new ArrayList();
	ArrayList fadeFruitsThreads = new ArrayList();
	GameObject[] wholeFruitsList = new GameObject[12];
	GameObject fruitLevel;
	float cursorMoveSpeed;
	float cursorMoveTime = 0.5f;
	Color transparent = new Color(1f, 1f, 1f, 0f);

	// Use this for initialization
	void Start () {
		fruitLevel = fruitLevel3;
		for (int i = 0; i < 12; i++) {
			wholeFruitsList[i] = wholeFruits.transform.GetChild(i).gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator IncreaseCursor () {
		float dist = Player.cupHeight/maxCupLevel;	// raise by this height
		cursorMoveSpeed = dist / cursorMoveTime;
		while (cursor.transform.localPosition.y < nextPos) {
			   cursor.transform.localPosition = new Vector3(cursor.transform.localPosition.x,
			                                                cursor.transform.localPosition.y + cursorMoveSpeed*Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator DecreaseCursor () {
		cursorMoveSpeed = cupHeight / cursorMoveTime;
		while (cursor.transform.localPosition.y > emptyPos) {
			cursor.transform.localPosition = new Vector3(cursor.transform.localPosition.x,
			                                             cursor.transform.localPosition.y - cursorMoveSpeed*Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		// To make sure it goes back to this exact point
		cursor.transform.localPosition = new Vector3(cursor.transform.localPosition.x, emptyPos);
	}

	IEnumerator FadeFruit (GameObject fruit, Color startColor, Color targetColor) {
		SpriteRenderer sprite = fruit.GetComponent<SpriteRenderer> ();
		sprite.color = startColor;

		while (sprite.color != targetColor) {
			sprite.color = Color.Lerp (sprite.color, targetColor, 3f*Time.deltaTime);
			yield return null;
		}
	}

	public void SetCurrentFruit (int fruit) {
		currentFruit = fruit;

		wholeFruitsList [fruit].GetComponent<SpriteRenderer> ().color = Color.white;
	}

	public void ChangeCupLevel (bool increase) {
		if (increase) {
			wholeFruitsList [currentFruit].GetComponent<SpriteRenderer> ().color = transparent;

			// Fade the fruits in
			GameObject tempFruit = fruitLevel.transform.GetChild (cupLevel).GetChild (currentFruit).gameObject;
			choppedFruitsList.Add (tempFruit);
			IEnumerator tempFadeFruit = FadeFruit(tempFruit, transparent, Color.white);
			StartCoroutine(tempFadeFruit);
			fadeFruitsThreads.Add (tempFadeFruit);

			cupLevel++;
			nextPos = emptyPos + cupLevel*(cupHeight / maxCupLevel);
			StartCoroutine (IncreaseCursor ());
		} else {
			// Stop all running FadeFruit threads
			foreach (IEnumerator thread in fadeFruitsThreads) {
				StopCoroutine(thread);
			}
			fadeFruitsThreads.Clear();

			// Fade all fruits away at the same time
			foreach(GameObject fruit in choppedFruitsList) {
				StartCoroutine(FadeFruit(fruit, Color.white, transparent));
			}
			choppedFruitsList.Clear();

			nextPos = emptyPos + cupHeight / maxCupLevel;			
			cupLevel = 0;
			StartCoroutine(DecreaseCursor());
		}
	}

	public void IncreaseScore () {
		score++;
		cupLevel = 0;

		if (score == 3) {
			maxCupLevel = 4;
			fruitLevel = fruitLevel4;
		} else if (score > 6) {
			maxCupLevel = 5;
			fruitLevel = fruitLevel5;
		}
		Debug.Log ("Score: " + score);
		Debug.Log ("maxLevel: " + maxCupLevel);
	}
}
