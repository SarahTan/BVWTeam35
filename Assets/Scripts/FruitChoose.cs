using UnityEngine;
using System.Collections;

public class FruitChoose : MonoBehaviour {

	int[] prevAssignedNum = new int[2];
    int[] assignedNum = new int[2];
	int badFruit;
    int maxButtons = 12;
 
    void Awake() {
  		prevAssignedNum [0] = prevAssignedNum [1] = -1;
        assignedNum[0] = assignedNum[1] = -1;
		badFruit = -1;
    }

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update() {

    }

    /* HOW THE GAME WORKS
       *     1. At the start, assign a fruit to both players (AssignFruit will be called twice by another script)
       * 	 2. When a correct fruit is obtained, assign that player a new fruit (called by the other script). The other player STILL HAS THE SAME FRUIT.
       * 
       *     AssignFruit function should assign a fruit to the player listed in the parameter, and return the fruit assigned.
       *     You probably want to store the fruit which each player is currently assigned, and maybe the previous one as well so those don't get called immediately again. 
       */

    public int AssignFruit(int player) {
		prevAssignedNum[player] = assignedNum[player];
		while (assignedNum[player] == prevAssignedNum[player] ||	// get a different number
		       assignedNum[player] == assignedNum[(player+1)%2] ||	// number different from other player's
		       assignedNum[player] == badFruit) {					// its not a bad fruit
			assignedNum[player] = Random.Range(0, maxButtons);
        }
		Debug.Log("player0: " + assignedNum[player] + ", player1: " + assignedNum[(player+1)%2]);

		return assignedNum[player];
	}

	public int AssignBadFruit () {
		do {
			badFruit = Random.Range (0, maxButtons);
		} while (badFruit == assignedNum[0] || badFruit == assignedNum[1]);

		return badFruit;
	}
	

}
