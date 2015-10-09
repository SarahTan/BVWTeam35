using UnityEngine;
using System.Collections;

public class fruitChoose : MonoBehaviour {

	public GameObject[] platform;   
	public GameObject Player0;			//start point for player1
	public GameObject Player1;			//start point for player2

	int count = 0; 						//time count, determine time gap between every move
	int first, second;					//first is the next step for player1,second is the next step for player2


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Timer.start == true) {                      //game start

			if(count == 120){		                    //call this function every 2seconds

				while(true){			                //find next available route
				    first = Random.Range(0, 15);     	//generate random number
					second = Random.Range(0, 15);		
					while(second == first ){
						second = Random.Range(0, 15);	//in case two numbers are same
					}
					if(intersect(Player0,platform[first],Player1,platform[second])){    //determine if first and second are right choice
						if((Player0.transform.position == platform[second].transform.position)||(platform[first].transform.position== Player1.transform.position))  //if two route only intersect at the begin point or end point, continue
							continue;
						else{
							Player0 = platform[first];                                       //set next available route
							Player1 = platform[second];
							Debug.LogWarning("player0 :" + Player0.transform.position);
							Debug.LogWarning("player1 :" + Player1.transform.position);
							count = 0;                                                       //reset count
							break;
						}
					}
				}
			}
			count++;
		}

	}

	double mult(GameObject a, GameObject b, GameObject c)  //cross product
	{  
		return (a.transform.position.x-c.transform.position.x)*(b.transform.position.y-c.transform.position.y)-(b.transform.position.x-c.transform.position.x)*(a.transform.position.y-c.transform.position.y);  
	}  

	bool intersect(GameObject PointA0, GameObject PointA1, GameObject PointB0, GameObject PointB1)   //return true if intersect
	{  
		if ( Mathf.Max(PointA0.transform.position.x, PointA1.transform.position.x)<Mathf.Min(PointB0.transform.position.x, PointB1.transform.position.x) )  
		{  
			return false;  
		}  
		if ( Mathf.Max(PointA0.transform.position.y, PointA1.transform.position.y)<Mathf.Min(PointB0.transform.position.y, PointB1.transform.position.y) )  
		{  
			return false;  
		}  
		if ( Mathf.Max(PointB0.transform.position.x, PointB1.transform.position.x)<Mathf.Min(PointA0.transform.position.x, PointA1.transform.position.x) )  
		{  
			return false;  
		}  
		if ( Mathf.Max(PointB0.transform.position.y, PointB1.transform.position.y)<Mathf.Min(PointA0.transform.position.y, PointA1.transform.position.y) )  
		{  
			return false;  
		}  
		if ( mult(PointB0, PointA1, PointA0)*mult(PointA1, PointB1, PointA0)<0 )  
		{  
			return false;  
		}  
		if ( mult(PointA0, PointB1, PointB0)*mult(PointB1, PointA1, PointB0)<0 )  
		{  
			return false;  
		}  
		return true;  
	}  





}
