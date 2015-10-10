using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static float cupHeight = 11f;
	public static float fullPos = 8f;
	public static float emptyPos = -3f;

	public float nextPos;
	public int score = 0;
	public int currentFruit = -1;
	public int cupLevel = 0;
	public int maxLevel = 3;
	public GameObject cursor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetScore () {

	}
}
