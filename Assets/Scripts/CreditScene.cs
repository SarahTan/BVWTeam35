using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditScene : MonoBehaviour {
    public RawImage Artist;
    public RawImage SoundDesigner;

	float displayDuration = 3f;

	// Use this for initialization
	void Start () {
		Invoke("ArtPicture", displayDuration);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ArtPicture() {
		Artist.gameObject.SetActive(true);
		Invoke("SoundPicture", displayDuration);
    }

    void SoundPicture() {
		SoundDesigner.gameObject.SetActive(true);
		Invoke("RestartGame", displayDuration);
    }

	void RestartGame () {
		Application.LoadLevel ("Start");
	}
}
