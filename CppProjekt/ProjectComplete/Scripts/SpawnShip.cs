using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShip : MonoBehaviour {

	private Camera camera;
	private int minerals;
	private GameController gameController;
	private int thisId;
	private bool selected;
	private bool isBeingInstantiated;

	public GameObject[] ships;
	public int[] price;
	public bool isSmallFactory;

	public static int Id = 0;

	void Start () {
		thisId = Id++;
		minerals = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().GetMinerals ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		isBeingInstantiated = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isBeingInstantiated){
			Ray ray = camera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				transform.position = hit.point;
				if (Input.GetMouseButton (0)) {
					isBeingInstantiated = false;
				}
			}
		}
	}

	public void CreateShip(int shipId){
		float distance;
		if (isSmallFactory) {
			distance = 25f;
		} else {
			distance = 75f;
		}
		minerals = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().GetMinerals ();
		if (price [shipId] <= minerals) {
			GameObject newShip = Instantiate (ships[shipId], transform.position - new Vector3(0.0f,0.0f,distance), Quaternion.identity);
			gameController.SubstractMinerals (price [shipId]);
		}
	}
	void OnMouseDown(){
		if (Input.GetMouseButton (0)) {
			gameController.newFactorySelected (thisId);
		}
	}
	public int getId(){return thisId;}
	public bool isSelected(){return selected;}
	public void setAsSelected(bool state){
		selected = state;
	}
}
