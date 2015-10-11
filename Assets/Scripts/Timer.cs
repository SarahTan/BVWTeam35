using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public GameManager gameManager;
	public Text timerText;              //text object

	int duration = 100;	// in seconds
	int timeLeft;	

	// Use this for initialization
	void Start () {
		timeLeft = duration;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void StartTimer () {
		StartCoroutine ("Countdown");
	}

	IEnumerator Countdown () {
		while (timeLeft > 0) {
			if (timeLeft == duration/2) {
				gameManager.halfWayMark = true;
			}
			timeLeft--;
			timerText.text = "Time Left: " + timeLeft/60 + ":" + (timeLeft%60).ToString("00");
			yield return new WaitForSeconds(1f);
		}
		gameManager.TimesUp();
	}
}
