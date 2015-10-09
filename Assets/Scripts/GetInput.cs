using UnityEngine;
using System.Collections;

public class GetInput : MonoBehaviour {

	public GameManager gameManager;

	int selected = -1;
	int previous = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.gameStarted) {
			if (Input.GetKeyDown ("w")) {
				Debug.Log (Input.inputString);
				selected = 0;

			} else if (Input.GetKeyDown ("a")) {
				Debug.Log (Input.inputString);
				selected = 1;

			} else if (Input.GetKeyDown ("s")) {
				Debug.Log (Input.inputString);
				selected = 2;

			} else if (Input.GetKeyDown ("d")) {
				Debug.Log (Input.inputString);
				selected = 3;

			} else if (Input.GetKeyDown ("f")) {
				Debug.Log (Input.inputString);
				selected = 4;

			} else if (Input.GetKeyDown ("g")) {
				Debug.Log (Input.inputString);
				selected = 5;

			} else if (Input.GetKeyDown ("up")) {
				Debug.Log ("Up");
				selected = 6;

			} else if (Input.GetKeyDown ("down")) {
				Debug.Log ("Down");
				selected = 7;

			} else if (Input.GetKeyDown ("left")) {
				Debug.Log ("Left");
				selected = 8;

			} else if (Input.GetKeyDown ("right")) {
				Debug.Log ("Right");
				selected = 9;

			} else if (Input.GetKeyDown ("space")) {
				Debug.Log ("Space");
				selected = 10;

			} else if (Input.GetKeyDown ("mouse 1")) {
				Debug.Log ("Right click");
				selected = 11;


				// change these once we get the makey makey to make it less sensitive
			} else if (Input.GetAxis ("Mouse X") > 0.5) {
				Debug.Log ("Mouse moving right");
				selected = 12;

			} else if (Input.GetAxis ("Mouse X") < -0.5) {
				Debug.Log ("Mouse moving left");
				selected = 13;

			} else if (Input.GetAxis ("Mouse Y") > 0.5) {
				Debug.Log ("Mouse moving up");
				selected = 14;
			
			} else if (Input.GetAxis ("Mouse Y") < -0.5) {
				Debug.Log ("Mouse moving down");
				selected = 15;
			}
		}
	}

	void SendInput () {		
		if (selected != previous) {
			previous = selected;
			gameManager.FruitObtained(selected);
		}
	}
}