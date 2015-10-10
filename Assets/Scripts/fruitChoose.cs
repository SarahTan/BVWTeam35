using UnityEngine;
using System.Collections;

public class FruitChoose : MonoBehaviour
{

    public GameObject[] buttons;

    // we use arrays so we don't have to write separate code for each player -- we just use their index
    GameObject[] currentChoice = new GameObject[2];
    GameObject[] prevChoice = new GameObject[2];
    int[] assignedNum = new int[2];     // this used to be called first and second
    int[] count = new int[2];
    int gameStart = 0;
    int temp;

    // Use this for initialization
    void Start() {
        assignedNum[0] = assignedNum[1] = -1;       // init the array so it isn't = 0
        count[0] = count[1] = 0;
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


    /*
    gameStart is to assign two fruit at random for player0 and player1
    After gameStart, the "else" part is to calculate two number for player0 and player1
    After next two numbers are decided, using count to know if it is the first time that player call the funtion. 
    If it's the first time, assign the value we got in previous "else" part to player. If not, do the normal work
    */

    public int AssignFruit(int player) {
        if (gameStart < 2) {                              //assign random fruit two times

            assignedNum[player] = Random.Range(0, 15);
            while (assignedNum[0] == assignedNum[1]) {
                assignedNum[player] = Random.Range(0, 15);  //in case two numbers are same
            }
            currentChoice[player] = buttons[assignedNum[player]];
            prevChoice[player] = buttons[assignedNum[player]];
            gameStart++;

        } else {
            if (gameStart == 3) {
				gameStart++;

                while (true) {                
					for(int i = 0; i < 2; i++) {
						temp = Random.Range(0, 15);  
						while (assignedNum[i] == temp || assignedNum[0] == assignedNum[1]) {
							temp = Random.Range(0, 15);
						}
						assignedNum[i] = temp;
					}

					// Check if the 2 numbers intersect
                    if (Intersect(currentChoice[0], buttons[assignedNum[0]],
                                  currentChoice[1], buttons[assignedNum[1]])) {
						for(int i = 0; i < 2; i++) {
							prevChoice[i] = currentChoice[i];
							currentChoice[i] = buttons[assignedNum[i]];
						}
                        break;
                    }
                }
            }

			// Called once per player
            if (count[player] == 0) {
				count[player]++;
			} else {
                while (true) {
                    temp = Random.Range(0, 15);
                    while (assignedNum[player] == temp) {
                        temp = Random.Range(0, 15);
                    }

					if (Intersect(currentChoice[player], buttons[assignedNum[player]],
					              currentChoice[(player+1)%2], prevChoice[(player+1)%2])) {
						prevChoice[player] = currentChoice[player];
						currentChoice[player] = buttons[assignedNum[player]];
						break;
					}
                }
            }
        }
		
		return assignedNum[player];
	}
	
	//cross product
    double Mult(GameObject a, GameObject b, GameObject c)
    {
        return ((a.transform.position.x - c.transform.position.x) * (b.transform.position.y - c.transform.position.y)) -
            ((b.transform.position.x - c.transform.position.x) * (a.transform.position.y - c.transform.position.y));
    }

    //return true if intersect
    bool Intersect(GameObject PointA0, GameObject PointA1, GameObject PointB0, GameObject PointB1)
    {
        if (Mathf.Max(PointA0.transform.position.x, PointA1.transform.position.x) < Mathf.Min(PointB0.transform.position.x, PointB1.transform.position.x) ||
            Mathf.Max(PointA0.transform.position.y, PointA1.transform.position.y) < Mathf.Min(PointB0.transform.position.y, PointB1.transform.position.y) ||
            Mathf.Max(PointB0.transform.position.x, PointB1.transform.position.x) < Mathf.Min(PointA0.transform.position.x, PointA1.transform.position.x) ||
            Mathf.Max(PointB0.transform.position.y, PointB1.transform.position.y) < Mathf.Min(PointA0.transform.position.y, PointA1.transform.position.y) ||
            Mult(PointB0, PointA1, PointA0) * Mult(PointA1, PointB1, PointA0) < 0 || Mult(PointA0, PointB1, PointB0) * Mult(PointB1, PointA1, PointB0) < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
