using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {

	private GameController gameController;

	void Start (){
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	void Update () {}

	void OnMouseDown(){
		if (Input.GetMouseButton (0)) {
			gameController.deselectObjects();
		}
	}
}
