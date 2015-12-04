using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {

    public GameObject startButton;

	bool readyToStart = false;
	SoundManager soundManager;

	// Use this for initialization
	void Start () {
		SMCreator temp = SMCreator.Instance;	// Create SM for the first time
		soundManager = GameObject.Find ("SoundManager(Clone)").GetComponent<SoundManager> ();
		soundManager.ResetGame ();
		startButton.SetActive (false);
        Invoke("buttonAppear", 3);
	}
	
	// Update is called once per frame
	void Update () {
		// Skip button
        if (Input.GetKeyDown(KeyCode.Return)){
            Application.LoadLevel("Main");
        }

        if (Input.GetKeyUp(KeyCode.S) && readyToStart) {
            Application.LoadLevel("Instructions");
        }
    }

    void buttonAppear() {
		readyToStart = true;
        startButton.SetActive(true);
    }

}
