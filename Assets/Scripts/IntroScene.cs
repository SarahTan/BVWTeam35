using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour {
    public AudioSource IntroMusic;
    public RawImage Introduction2;
    bool NextScene = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        SceneControl();
    }

    void SceneControl() {
        
        if (Input.GetKeyUp(KeyCode.S)) {
            StartScene.nextMove = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && StartScene.nextMove == true && NextScene == false)
        {
            StartScene.nextMove = false;
            NextScene = true;
            Introduction2.gameObject.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.S) && StartScene.nextMove == true && NextScene == true)
        {
            Application.LoadLevel("Main");
        }
    }


}
