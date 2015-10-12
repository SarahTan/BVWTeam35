using UnityEngine;
using System.Collections;

public class IntroScene : MonoBehaviour {
    public Animator CameraMove;
    public AudioSource IntroMusic;
    bool NextScene = false;
	// Use this for initialization
	void Start () {
        IntroMusic.Play();
	}
	
	// Update is called once per frame
	void Update () {
        SceneControll();
    }

    void SceneControll() {
        Debug.Log("nextmove :" + StartScene.nextMove + " nextScene :" + NextScene);
        if (Input.GetKeyUp(KeyCode.S)) {
            StartScene.nextMove = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && StartScene.nextMove == true && NextScene == false)
        {
            CameraMove.SetFloat("CameraMove", 1f);
            StartScene.nextMove = false;
            NextScene = true;
        }
        if (Input.GetKeyDown(KeyCode.S) && StartScene.nextMove == true && NextScene == true)
        {
            Debug.Log("goto level2");
            Application.LoadLevel(2);
        }

    }


}
