using UnityEngine;
using System.Collections;

public class FruitChoose : MonoBehaviour
{

    public GameObject[] buttons;

	int[] prevAssignedNum = new int[2];
    int[] assignedNum = new int[2];     // this used to be called first and second
    int maxButtons = 11;
 
    // Use this for initialization
    void Awake() {
  		prevAssignedNum [0] = prevAssignedNum [1] = -1;
        assignedNum[0] = assignedNum[1] = -1; 
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
		while (assignedNum[player] == prevAssignedNum[player] ||
		       assignedNum[player] == assignedNum[(player+1)%2]) {
			assignedNum[player] = Random.Range(0, maxButtons);
        }
		return assignedNum[player];
	}
	
	//cross product
    double Mult(GameObject a, GameObject b, GameObject c) {
        return ((a.transform.position.x - c.transform.position.x) * (b.transform.position.y - c.transform.position.y)) -
            ((b.transform.position.x - c.transform.position.x) * (a.transform.position.y - c.transform.position.y));
    }

    //return true if intersect
    bool Intersect(GameObject PointA0, GameObject PointA1, GameObject PointB0, GameObject PointB1) {
        if (Mathf.Max(PointA0.transform.position.x, PointA1.transform.position.x) < Mathf.Min(PointB0.transform.position.x, PointB1.transform.position.x) ||
            Mathf.Max(PointA0.transform.position.y, PointA1.transform.position.y) < Mathf.Min(PointB0.transform.position.y, PointB1.transform.position.y) ||
            Mathf.Max(PointB0.transform.position.x, PointB1.transform.position.x) < Mathf.Min(PointA0.transform.position.x, PointA1.transform.position.x) ||
            Mathf.Max(PointB0.transform.position.y, PointB1.transform.position.y) < Mathf.Min(PointA0.transform.position.y, PointA1.transform.position.y) ||
            Mult(PointB0, PointA1, PointA0) * Mult(PointA1, PointB1, PointA0) < 0 || Mult(PointA0, PointB1, PointB0) * Mult(PointB1, PointA1, PointB0) < 0) {
            return false;
        } else {
            return true;
        }
    }
}
