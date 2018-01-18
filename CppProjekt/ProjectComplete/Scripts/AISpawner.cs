using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour {

	private int minerals;
	private AIController aiController; 
	private int spawnedSmallShipIndex;
	private int spawnedLargeShipIndex;

	public GameObject[] factories;
	public GameObject[] ships;
	public int[] factoryPrice;
	public int[] shipPrice;

	void Start () {
		minerals = GameObject.FindGameObjectWithTag ("AIController").GetComponent<AIController> ().GetMinerals ();
		aiController = GameObject.FindGameObjectWithTag ("AIController").GetComponent<AIController> ();
		spawnedLargeShipIndex = 0;
		spawnedSmallShipIndex = 0;
	}

	void Update () {
		
	}
	public void SpawnFactory(int factoryId){
		if (factoryId == 0 && factoryPrice[factoryId]<=aiController.GetMinerals()) {
			GameObject newFactory = Instantiate (factories [factoryId], transform.position + new Vector3 (-100f, 0f, 100f), Quaternion.identity);
			aiController.SubstractMinerals (factoryPrice [factoryId]);
			aiController.smallFactoryAmount++;
		} 
		if(factoryId==1 && factoryPrice[factoryId]<=aiController.GetMinerals()){
			GameObject newFactory = Instantiate (factories [factoryId], transform.position - new Vector3 (100f, 0f, 50f), Quaternion.identity);
			aiController.SubstractMinerals (factoryPrice [factoryId]);
			aiController.largeFactoryAmount++;
		}
	}
	public void SpawnShip(int shipId){
		if (shipId == 0 && shipPrice[shipId]<=aiController.GetMinerals()) {
			GameObject newShip = Instantiate (ships [shipId], transform.position + new Vector3 (-100f, 0f, 60f), Quaternion.identity);
			spawnedSmallShipIndex++;
			aiController.SubstractMinerals (shipPrice [shipId]);
			aiController.harvesterAmount++;
			aiController.addToHarvesterUnits (newShip);
		}
		if (shipId == 1 && shipPrice[shipId]<=aiController.GetMinerals()) {
			GameObject newShip = Instantiate (ships [shipId], transform.position + new Vector3 (-50-10*spawnedSmallShipIndex, 0f, 60f), Quaternion.identity);
			spawnedSmallShipIndex++;
			aiController.SubstractMinerals (shipPrice [shipId]);
			aiController.fighterAmount++;
			aiController.addToMilitaryUnits (newShip);
		}
		if (shipId == 2 && shipPrice[shipId]<=aiController.GetMinerals()) {
			GameObject newShip = Instantiate (ships [shipId], transform.position - new Vector3 (100f+20f*spawnedLargeShipIndex, 0f, 100f), Quaternion.identity);
			spawnedLargeShipIndex++;
			aiController.SubstractMinerals (shipPrice [shipId]);
			aiController.corveteAmount++;
			aiController.addToMilitaryUnits (newShip);
		}
		if (shipId == 3 && shipPrice[shipId]<=aiController.GetMinerals()) {
			GameObject newShip = Instantiate (ships [shipId], transform.position - new Vector3 (50f*spawnedLargeShipIndex, 0f, 100f), Quaternion.identity);
			spawnedLargeShipIndex++;
			aiController.SubstractMinerals (shipPrice [shipId]);
			aiController.destroyerAmount++;
			aiController.addToMilitaryUnits (newShip);
		}
	}
	public void resetIndexing(){
		spawnedSmallShipIndex = 0;
		spawnedLargeShipIndex = 0;
	}
}
