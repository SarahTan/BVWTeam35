using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {

    public static bool nextMove = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S)){
            nextMove = false;
            Application.LoadLevel(1);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            nextMove = true;
            Application.LoadLevel(1);
        }
    }
}
