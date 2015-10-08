using UnityEngine;
using System.Collections;

public class GetInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("w")) {
			Debug.Log(Input.inputString);

		} else if (Input.GetKeyDown ("a")) {
			Debug.Log(Input.inputString);

		} else if (Input.GetKeyDown ("s")) {
			Debug.Log(Input.inputString + " here");

		} else if (Input.GetKeyDown ("d")) {
			Debug.Log(Input.inputString + " there");

		} else if (Input.GetKeyDown ("f")) {
			Debug.Log(Input.inputString);

		} else if (Input.GetKeyDown ("g")) {
			Debug.Log(Input.inputString);

		} else if (Input.GetKeyDown ("up")) {
			Debug.Log("Up");

		} else if (Input.GetKeyDown ("down")) {
			Debug.Log("Down");

		} else if (Input.GetKeyDown ("left")) {
			Debug.Log("Left");

		} else if (Input.GetKeyDown ("right")) {
			Debug.Log("Right");

		} else if (Input.GetKeyDown ("space")) {
			Debug.Log("Space");

		} else if (Input.GetKeyDown ("mouse 0")) {
			Debug.Log("Left click");

		} else if (Input.GetKeyDown ("mouse 1")) {
			Debug.Log("Right click");


		// change these once we get the makey makey to make it less sensitive
		} else if (Input.GetAxis("Mouse X") > 0) {
			Debug.Log("Mouse moving right");

		} else if (Input.GetAxis("Mouse X") < 0) {
			Debug.Log("Mouse moving left");

		} else if (Input.GetAxis("Mouse Y") > 0) {
			Debug.Log("Mouse moving up");
			
		} else if (Input.GetAxis("Mouse Y") < 0) {
			Debug.Log("Mouse moving down");
		}
	}
}

//6 keyboard key inputs (W, A, S, D, F, G)
//4 keyboard arrow input in four directions
//1 keyboard Space key
//4 mouse movement input in four directions
//2 mouse Left click input
//1 mouse Right click input