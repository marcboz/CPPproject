using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

	private List<GameObject> aiHarvesterUnits = new List<GameObject> ();
	private List<GameObject> aiMilitaryUnits=new List<GameObject>();
	private GameObject[] aiUnits;
	private GameObject[] playerUnits;
	private GameObject[] asteroids;
	private GameObject[] units;
	private AISpawner aiSpawner;
	private AINavigation currentAIUnit;
	private UnitNavigation selectedPlayer;
	private AsteroidBehavior selectedAsteroid;
	private int stageIndex;
	private int shipsExpectedAmount;
	private int[] smallFactoryExpAmount;
	private int[] largeFactoryExpAmount;
	private int[] harvesterExpAmount;
	private int[] fighterExpAmount;
	private int[] corveteExpAmount;
	private int[] destroyerExpAmount;

	public int minerals;
	public int smallFactoryAmount;
	public int largeFactoryAmount;
	public int harvesterAmount;
	public int fighterAmount;
	public int corveteAmount;
	public int destroyerAmount;

	void Start () {
		aiUnits = GameObject.FindGameObjectsWithTag ("Enemy");
		smallFactoryExpAmount = new int[5]{ 1, 1, 1, 1, 2 };
		largeFactoryExpAmount = new int[5]{ 0, 0, 0, 1, 1 };
		harvesterExpAmount = new int[5]{ 3, 5, 7, 9, 11 };
		fighterExpAmount = new int[5]{ 0, 3, 8, 15, 30 };
		corveteExpAmount = new int[5]{ 0, 0, 0, 4, 8 };
		destroyerExpAmount = new int[5]{ 0, 0, 0, 1, 4 };
		smallFactoryAmount = 0;
		largeFactoryAmount = 0;
		harvesterAmount = 0;
		fighterAmount = 0;
		corveteAmount = 0;
		destroyerAmount = 0;
		stageIndex = 0;
		shipsExpectedAmount = 4;
		aiUnits = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject unit in aiUnits) {
			Debug.Log (unit);
			if (unit.GetComponent<AINavigation> ().isMilitary) {
				aiMilitaryUnits.Add (unit);
			}
			if (unit.GetComponent<AINavigation> ().isHarvester) {
				aiHarvesterUnits.Add (unit);
				FindNearestAsteroid (unit.transform.position);
			}
			if (unit.GetComponent<AINavigation> ().isBase) {
				aiSpawner = unit.GetComponent<AISpawner> ();
			}
		}
	}

	void FixedUpdate () {
		aiUnits = GameObject.FindGameObjectsWithTag ("Enemy");
		shipsExpectedAmount = smallFactoryExpAmount [stageIndex] + largeFactoryExpAmount [stageIndex] + harvesterExpAmount [stageIndex] + fighterExpAmount [stageIndex] + corveteExpAmount [stageIndex] + destroyerExpAmount [stageIndex];
		foreach (GameObject unit in aiHarvesterUnits) {
			currentAIUnit = unit.GetComponent<AINavigation> ();
			FindNearestAsteroid (unit.transform.position);
			newAsteroidSelected (selectedAsteroid.getId ());
		}
		foreach (GameObject unit in aiMilitaryUnits) {
			if (currentAIUnit != null && unit != null) {
				currentAIUnit = unit.GetComponent<AINavigation> ();
				FindNearestPlayer ();
				if (Vector3.Distance (currentAIUnit.transform.position, selectedPlayer.transform.position) < currentAIUnit.range) {
					newEnemySelected (selectedPlayer.getId ());
				}
			}
		}
		if (aiUnits.Length -1== shipsExpectedAmount) {
			FindNearestPlayer ();
			aiUnits = GameObject.FindGameObjectsWithTag ("Enemy");
			foreach (GameObject unit in aiMilitaryUnits) {
				currentAIUnit = unit.GetComponent<AINavigation> ();
				newEnemySelected (selectedPlayer.getId ());
			}
			stageIndex++;
			aiSpawner.resetIndexing ();
		}
		aiStages (smallFactoryExpAmount[stageIndex], largeFactoryExpAmount[stageIndex], harvesterExpAmount[stageIndex], fighterExpAmount[stageIndex], corveteExpAmount[stageIndex], destroyerExpAmount[stageIndex]);
	}
	public void aiStages(int sfn,int lfn, int hn,int fn, int cn,int dn){
		if (sfn > smallFactoryAmount) {
			aiSpawner.SpawnFactory (0);
		}
		if (lfn > largeFactoryAmount) {
			aiSpawner.SpawnFactory (1);
		} 
		if (hn > harvesterAmount) { 
			aiSpawner.SpawnShip (0);
		}
		if (fn > fighterAmount) {
			aiSpawner.SpawnShip (1);
		}
		if (cn > corveteAmount && largeFactoryAmount != 0) { 
			aiSpawner.SpawnShip (2);
		}
		if (dn > destroyerAmount && largeFactoryAmount != 0) { 
			aiSpawner.SpawnShip (3);
		}
	}
	public void newEnemySelected(int id){
		units = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject unit in units) {
			UnitNavigation selectedUnit = unit.GetComponent<UnitNavigation> ();
			if (selectedUnit.getId () == id) {
				selectedUnit.setEnemyAsSelected (true);
				currentAIUnit.targetSet (selectedUnit.getId ());

			} else {
				selectedUnit.setEnemyAsSelected (false);
			}
		}
	}
	public void newAsteroidSelected(int id){
		units = GameObject.FindGameObjectsWithTag ("Asteroid");
		foreach (GameObject unit in units) {
			AsteroidBehavior selectedUnit = unit.GetComponent<AsteroidBehavior> ();
			if (selectedUnit.getId () == id) {
				selectedUnit.setEnemyAsSelected (true);
				currentAIUnit.targetSet (selectedUnit.getId ());
			} else {
				selectedUnit.setEnemyAsSelected (false);
			}
		}
	}
	public void FindNearestPlayer(){
		playerUnits = GameObject.FindGameObjectsWithTag ("Player");
		float minDist = Mathf.Infinity;
		Vector3 currentPosition = transform.position;
		foreach (GameObject unit in playerUnits) {
			float distance = Vector3.Distance (unit.transform.position, currentPosition);
			if(distance < minDist){
				selectedPlayer = unit.GetComponent<UnitNavigation> ();
				minDist = distance;
			}
		}
	}
	public void FindNearestAsteroid(Vector3 currentUnitPosition){
		asteroids = GameObject.FindGameObjectsWithTag ("Asteroid");
		float minDist = Mathf.Infinity;
		Vector3 currentPosition = currentUnitPosition;
		foreach (GameObject unit in asteroids) {
			float distance = Vector3.Distance (unit.transform.position, currentPosition);
			if(distance < minDist){
				selectedAsteroid = unit.GetComponent<AsteroidBehavior> ();
				minDist = distance;
			}
		}
	}
	public int GetMinerals(){return minerals;}
	public void SubstractMinerals(int amount){
		minerals -= amount;
	}
	public void AddMinerals(int amount){
		minerals += amount;
	}
	public void setSmallFactoryAmount(int amount){
		smallFactoryAmount += amount;
	}
	public void setLargeFactoryAmount(int amount){
		largeFactoryAmount += amount;
	}
	public int getSmallFactoryAmount(){
		return smallFactoryAmount;
	}
	public int getLargeFactoryAmount(){
		return largeFactoryAmount;
	}
	public void addToMilitaryUnits(GameObject addedUnit){
		aiMilitaryUnits.Add (addedUnit);
	}
	public void addToHarvesterUnits(GameObject addedUnit){
		aiHarvesterUnits.Add (addedUnit);
	}

}
