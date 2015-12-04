using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour {
    
	public AudioSource instructionMusic;
	public GameObject instructions1;
	public GameObject instructions2;
	public GameObject continueButton;
	public GameObject startButton;

	bool firstButton = false;
	bool secondButton = false;

	// Use this for initialization
	void Start () {
		instructions1.SetActive (true);
		instructions2.SetActive (false);
		continueButton.SetActive (false);
		startButton.SetActive (false);
        Invoke("FirstButtonAppear", 3f);
	}
	
	// Update is called once per frame
	void Update () {
		// Skip button
		if (Input.GetKeyDown(KeyCode.Return)){
			Application.LoadLevel("Main");
		}


		if (Input.GetKeyUp (KeyCode.S)) {
			if (secondButton) {
				Application.LoadLevel("Main");
			} else if (firstButton) {
				instructions1.SetActive (false);
				continueButton.SetActive (false);
				instructions2.gameObject.SetActive(true);
				Invoke ("SecondButtonAppear", 3f);
			}
		}
    }

    void FirstButtonAppear() {
		continueButton.SetActive (true);
        firstButton = true;
    }

    void SecondButtonAppear() {		
		startButton.SetActive (true);
        secondButton = true;
    }


}
