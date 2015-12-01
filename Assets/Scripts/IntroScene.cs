using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour {
    public AudioSource IntroMusic;
    public RawImage Introduction2;
    bool NextScene = false;
    bool GotoNext = false;

	// Use this for initialization
	void Start () {
        Invoke("FinishFirst", 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S) && NextScene == true)
        {
            Introduction2.gameObject.SetActive(true);
            Invoke("FinishSecond", 1.5f);
        }
        if (Input.GetKeyDown(KeyCode.S) && GotoNext == true)
        {
            Application.LoadLevel("Main");
        }
    }

    void FinishFirst() {
        NextScene = true;
    }
    void FinishSecond() {
        GotoNext = true;
    }


}
