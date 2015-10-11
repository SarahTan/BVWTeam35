using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public GameManager gameManager;
	public Text timerText;              //text object

	int timeLeft = 200;	// in seconds

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void StartTimer () {
		StartCoroutine ("Countdown");
	}

	IEnumerator Countdown () {
		while (timeLeft > 0) {
			timeLeft--;
			timerText.text = "Time Left: " + timeLeft/60 + ":" + (timeLeft%60).ToString("00");
			yield return new WaitForSeconds(1f);
		}
		gameManager.TimesUp();
	}
}
