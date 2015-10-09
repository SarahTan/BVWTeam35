using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public static bool start = true;   //start
	public float gameTime = 60f;		//how long our game should be
	public Text timerText;              //text object
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (start == true) {
			gameTime -= Time.deltaTime;
			timerText.text = "Time Left : " + gameTime;
			if (gameTime == 0){
				//call GameManager
			}
		}
	}
}
