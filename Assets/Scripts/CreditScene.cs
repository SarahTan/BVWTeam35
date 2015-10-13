using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditScene : MonoBehaviour {
    public RawImage Artist;
    public RawImage SoundDesigner;
	// Use this for initialization
	void Start () {
        Invoke("ArtPicture", 2);
        Invoke("SoundPicture", 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void ArtPicture() {
        Artist.gameObject.SetActive(true);
    }
    void SoundPicture() {
        SoundDesigner.gameObject.SetActive(true);
    }
}
