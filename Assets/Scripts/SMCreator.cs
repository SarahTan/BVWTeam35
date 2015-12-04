using UnityEngine;
using System.Collections;

public class SMCreator : Singleton <SMCreator> {

	// Use this for initialization
	void Awake () {		
		GameObject sm = Resources.Load<GameObject> ("SoundManager");
		Instantiate (sm, Vector3.zero, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
