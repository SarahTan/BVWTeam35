using UnityEngine;
using System.Collections;

public class FruitChoose : MonoBehaviour {

	public GameObject[] platform;   
	public GameObject player0;			//start point for player1
	public GameObject player1;			//start point for player2

	int count = 0; 						//time count, determine time gap between every move
	int first, second;					//first is the next step for player1,second is the next step for player2


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
	

	/*	HOW THE GAME WORKS
	 * 	1. At the start, assign a fruit to both players (AssignFruit will be called twice by another script)
	 *  2. When a correct fruit is obtained, assign that player a new fruit (called by the other script). The other player STILL HAS THE SAME FRUIT.
	 * 
	 * 	AssignFruit function should assign a fruit to the player listed in the parameter, and return the fruit assigned.
	 * 	You probably want to store the fruit which each player is currently assigned, and maybe the previous one as well so those don't get called immediately again. 
	 */

	// TODO: Fix this to reflect the above
	public int AssignFruit (int player) {
		first = Random.Range(0, 15);     	//generate random number
		second = Random.Range(0, 15);		
		while(second == first ) {
			second = Random.Range(0, 15);	//in case two numbers are same
		}

		if(Intersect(player0,platform[first],player1,platform[second])){    //determine if first and second are right choice
			if((player0.transform.position == platform[second].transform.position)||
			   (platform[first].transform.position == player1.transform.position)) {  //if two route only intersect at the begin point or end point, continue
				//continue;
			} else {
				player0 = platform[first];                                       //set next available route
				player1 = platform[second];
				Debug.LogWarning("player0 :" + player0.transform.position);
				Debug.LogWarning("player1 :" + player1.transform.position);
				count = 0;                                                       //reset count
				//break;
			}
		}

		return 0;
	}

	//cross product
	double Mult(GameObject a, GameObject b, GameObject c) {  
		return ((a.transform.position.x-c.transform.position.x)*(b.transform.position.y-c.transform.position.y)) -
			((b.transform.position.x-c.transform.position.x)*(a.transform.position.y-c.transform.position.y));  
	}  

	//return true if intersect
	bool Intersect(GameObject PointA0, GameObject PointA1, GameObject PointB0, GameObject PointB1) {  
		if (Mathf.Max (PointA0.transform.position.x, PointA1.transform.position.x) < Mathf.Min (PointB0.transform.position.x, PointB1.transform.position.x) ||
			Mathf.Max (PointA0.transform.position.y, PointA1.transform.position.y) < Mathf.Min (PointB0.transform.position.y, PointB1.transform.position.y) ||
			Mathf.Max (PointB0.transform.position.x, PointB1.transform.position.x) < Mathf.Min (PointA0.transform.position.x, PointA1.transform.position.x) ||
			Mathf.Max (PointB0.transform.position.y, PointB1.transform.position.y) < Mathf.Min (PointA0.transform.position.y, PointA1.transform.position.y) ||
			Mult (PointB0, PointA1, PointA0) * Mult (PointA1, PointB1, PointA0) < 0 || Mult (PointA0, PointB1, PointB0) * Mult (PointB1, PointA1, PointB0) < 0) {  
			return false;  
		} else {
			return true;  
		}
	}  





}
