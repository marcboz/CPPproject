using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFactory : MonoBehaviour {

	private int minerals;
	private GameController gameController;

	public GameObject[] factories;
	public int[] price;
	// Use this for initialization
	void Start () {
		minerals = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().GetMinerals ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void CreateFactory(int factoryId){
		if (price [factoryId] <= minerals) {
			GameObject newFactory = Instantiate (factories[factoryId], Input.mousePosition , Quaternion.identity);
			gameController.SubstractMinerals (price [factoryId]);
		}
	}
}
