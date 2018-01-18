using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour {

	private Button button;
	private GameController gameController;

	public int id;
	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		button.onClick.AddListener (TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void TaskOnClick(){
		if (transform.parent.tag == "BasePanel") {
			gameController.PassButtonSelectedFactory (id);
		} else {
			gameController.PassButtonSelectedShip (id);
		}
	}
}
