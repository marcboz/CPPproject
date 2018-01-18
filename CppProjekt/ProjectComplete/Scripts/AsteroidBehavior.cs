using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {

	private GameController gameController;
	private int thisId;
	private bool selected;
	float timer = 0;

	public static int Id = 0;

	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();		//will be moved to ai navigation srcipt once its made
		thisId = Id++;	
	}

	void Update () {
		
	}
	void OnMouseOver(){																				//will be moved to ai navigation srcipt once its made
		if (Input.GetMouseButton (1)) {
			gameController.newAsteroidSelected (thisId);
		}
	}
	public void setEnemyAsSelected(bool state) {
		selected = state;
	}
	public int getId(){return thisId;}
}
