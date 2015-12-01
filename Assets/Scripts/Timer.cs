using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public GameManager gameManager;
	public Text timerText;              //text object

	int duration = 70;	// in seconds
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

	public void RemoveTimer () {
		timerText.text = "";
		//StartCoroutine (FadeTimer ());
	}

//	IEnumerator FadeTimer () {
//		while (timerText.color != Color.clear) {
//			timerText.color = Color.Lerp(timerText.color, Color.clear, 2f*Time.deltaTime);
//			yield return null;
//		}
//	}

	IEnumerator Countdown () {
		while (timeLeft > 0) {
			yield return new WaitForSeconds(1f);

			if (timeLeft == duration/2) {
				gameManager.halfWayMark = true;
			}
			timeLeft--;
			timerText.text = "Time Left: " + timeLeft/60 + ":" + (timeLeft%60).ToString("00");
		}
		Debug.Log ("time: " + timeLeft);
		gameManager.TimesUp();
	}
}
