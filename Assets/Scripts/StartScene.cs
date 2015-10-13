using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {

    public RawImage StartButton;
    public static bool nextMove = true;
    bool gameEnable = false;
	// Use this for initialization
	void Start () {
        Invoke("buttonAppear", 3);
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKeyDown(KeyCode.S)){
            nextMove = false;
            Application.LoadLevel(1);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            nextMove = true;
            Application.LoadLevel(1);
        }*/
        if ((gameEnable == true) && Input.GetKeyDown(KeyCode.S)) {
            nextMove = false;
            Application.LoadLevel("Instructions");
        }
    }

    void buttonAppear() {
        StartButton.gameObject.SetActive(true);
        gameEnable = true;
    }

}
