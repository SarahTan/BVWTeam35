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
	public float maxLevel = 3;
	public GameObject cursor;
	
	float cursorMoveSpeed;
	float cursorMoveTime = 0.5f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator IncreaseCursor () {
		float dist = Player.cupHeight/maxLevel;	// raise by this height
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
	}
	
	public void ChangeCupLevel (bool increase) {
		if (increase) {
			cupLevel++;
			nextPos = emptyPos + cupLevel*(cupHeight / maxLevel);
			StartCoroutine (IncreaseCursor ());
		} else {
			nextPos = emptyPos + cupHeight / maxLevel;			
			cupLevel = 0;
			StartCoroutine(DecreaseCursor());
		}
	}

	public void IncreaseScore () {
		score++;
		cupLevel = 0;

		switch (score) {
		case 1:
			maxLevel = 3;
			break;

		case 2:
			maxLevel = 3;
			break;
			
		case 3:
			maxLevel = 4;
			break;
			
		case 4:
			maxLevel = 4;
			break;
			
		case 5:
			maxLevel = 4;
			break;
			
		case 6:
			maxLevel = 4;
			break;
			
		default:
			maxLevel = 5;
			break;
		}
	}
}
