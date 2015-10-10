using UnityEngine;
using System.Collections;

public class FruitChoose : MonoBehaviour {

	public GameObject[] buttons;   

	// we use arrays so we don't have to write separate code for each player -- we just use their index
	GameObject[] currentChoice = new GameObject[2];
	GameObject[] prevChoice = new GameObject[2];	
	int[] assignedNum = new int[2];		// this used to be called first and second

	int gameStart = 0;           
	int firstChoice = 0;

	// Use this for initialization
	void Start () {
		assignedNum [0] = assignedNum [1] = -1;		// init the array so it isn't = 0
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

	public int AssignFruit(int player){

		// The if part is done, no need to touch it
		if (gameStart < 2) {	                           //assign random fruit two times
			gameStart ++;

			assignedNum[player] = Random.Range (0, 15);
			while (assignedNum[0] == assignedNum[1]) {
				assignedNum[player] = Random.Range (0, 15);	//in case two numbers are same
			}

			currentChoice[player] = buttons[assignedNum[player]];
			prevChoice[player] = buttons[assignedNum[player]];


		// TODO: You see after I start using arrays, there's a lot of redundancy and repeated code
		// Please fix this, thanks (:
		} else {
			if (firstChoice < 2) {                         						//assign random fruits after initial part
				firstChoice++;

				if(firstChoice ==1){                       
					while(true){
						assignedNum[0] = Random.Range(0, 15);     						//generate random number
						assignedNum[1] = Random.Range(0, 15);		
						while(assignedNum[0] == assignedNum[1]) {
							assignedNum[1] = Random.Range(0, 15);						//in case two numbers are same
						}
						if(Intersect(currentChoice[0], buttons[assignedNum[0]],
						             currentChoice[1], buttons[assignedNum[1]])){    //determine if first and second are right choice

							/*if((player0.transform.position == platform[second].transform.position)||
					 	  	(platform[first].transform.position == player1.transform.position)) {  //if two route only intersect at the begin point or end point, continue
								//continue;
							} else {*/

							prevChoice[0] = currentChoice[0];
							prevChoice[1] = currentChoice[1];
							currentChoice[0] = buttons[assignedNum[0]];                                          //set next available route
							currentChoice[0] = buttons[assignedNum[1]];

								//Debug.LogWarning("player0 :" + player0.transform.position);
								//Debug.LogWarning("player1 :" + player1.transform.position);
							break;
							//}
						}
					}
				}
			} else {                                                     //assign fruit function
				if (player == 0) {										 //player0
					while (true) {
						assignedNum[0] = Random.Range (0, 15);
						if (Intersect (currentChoice[0], buttons [assignedNum[0]],
						               currentChoice[0], prevChoice[1])) {    
							prevChoice[0] = currentChoice[0];
							currentChoice[0] = buttons [assignedNum[0]];
							break;
						}
					}
				} else {												//player1
					while (true) {
						assignedNum[1] = Random.Range (0, 15);
						if (Intersect (prevChoice[0], currentChoice[0],
						               currentChoice[1], buttons [assignedNum[1]])) {
							prevChoice[1] = currentChoice[1];
							currentChoice[1] = buttons [assignedNum[1]];
							break;
						}
					}
				}
			}
		}
		return assignedNum[player];
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
