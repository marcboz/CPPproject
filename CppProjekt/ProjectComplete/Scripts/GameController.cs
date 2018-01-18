using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private GameObject[] units;
	private UnitNavigation selectedPlayerUnit;
	private UnitNavigation autoSelectedPlayerUnit;
	private SpawnShip selectedPlayerFactory;
	private GameObject smallFactoryPanel;
	private GameObject largeFactoryPanel;
	private GameObject basePanel;
	private GameObject gameOverPanel;
	private AINavigation nearestEnemy;
	private AINavigation selectedEnemy;
	private int currentSelectedUnitId;
	private int currentSelectedFactoryId;

	public int minerals;

	void Start () {
		currentSelectedUnitId = -1;
		currentSelectedFactoryId = -1;
		minerals = 3500;
		smallFactoryPanel = GameObject.FindGameObjectWithTag ("SmallFactoryPanel");
		largeFactoryPanel = GameObject.FindGameObjectWithTag ("LargeFactoryPanel");
		basePanel = GameObject.FindGameObjectWithTag ("BasePanel");
		smallFactoryPanel.SetActive (false);
		largeFactoryPanel.SetActive (false);
		basePanel.SetActive (false);
		gameOverPanel = GameObject.FindGameObjectWithTag ("GameOver");
		gameOverPanel.SetActive (false);
	}

	void FixedUpdate () {
		if (currentSelectedFactoryId != -1) {
			if (selectedPlayerFactory.isSmallFactory) {
				smallFactoryPanel.SetActive (true);
			} else {
				largeFactoryPanel.SetActive (true);
			}
		} else {
			smallFactoryPanel.SetActive (false);
			largeFactoryPanel.SetActive (false);
		}
		if (currentSelectedUnitId != -1 && selectedPlayerUnit.isBase) {
			basePanel.SetActive (true);
		} else {
			basePanel.SetActive (false);
		}

	}

	public void newObjectSelected(int id){
		units = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject unit in units) {
			UnitNavigation selectedUnit = unit.GetComponent<UnitNavigation> ();
			if (selectedUnit.getId () == id) {
				selectedUnit.setAsSelected (true);
				if (selectedPlayerUnit == autoSelectedPlayerUnit) {
					autoSelectedPlayerUnit = null;
				}
				selectedPlayerUnit = selectedUnit;
				currentSelectedUnitId = id;
				currentSelectedFactoryId = -1;
			} else {
				selectedUnit.setAsSelected (false);
			}
		}
	}
	public void newEnemySelected(int id){
		units = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject unit in units) {
			AINavigation selectedUnit = unit.GetComponent<AINavigation> ();
			if (selectedUnit.getId () == id && !selectedUnit.isFactory) {
				selectedUnit.setEnemyAsSelected (true);
				selectedEnemy = selectedUnit;
				if (selectedPlayerUnit != null) {
					selectedPlayerUnit.targetSet (selectedUnit.getId ());
				}
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
				if (selectedPlayerUnit != null) {
					selectedPlayerUnit.targetSet (selectedUnit.getId ());
				}
			} else {
				selectedUnit.setEnemyAsSelected (false);
			}
		}
	}
	public void newFactorySelected(int id){
		units = GameObject.FindGameObjectsWithTag ("Factory");
		foreach (GameObject unit in units) {
			SpawnShip selectedUnit = unit.GetComponent<SpawnShip> ();
			if (selectedUnit.getId () == id) {
				selectedUnit.setAsSelected (true);
				selectedPlayerFactory = selectedUnit;
				currentSelectedFactoryId = id;
				currentSelectedUnitId = -1;
			} else {
				selectedUnit.setAsSelected (false);
			}
		}
	}
	public void deselectObjects(){
		if (currentSelectedUnitId != -1 && !basePanel.activeSelf) {
			units = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject unit in units) {
				UnitNavigation selectedUnit = unit.GetComponent<UnitNavigation> ();
				if (selectedUnit.getId () == currentSelectedUnitId) {
					selectedUnit.setAsSelected (false);
					selectedPlayerUnit = selectedUnit;
					currentSelectedUnitId = -1;
				} else {
					selectedUnit.setAsSelected (false);
				}
			}
		}
	}
	public void AutotargetEnemy(){
		units = GameObject.FindGameObjectsWithTag ("Enemy");
		float minDist = Mathf.Infinity;
		foreach (GameObject unit in units) {
			float distance = Vector3.Distance (unit.transform.position, autoSelectedPlayerUnit.transform.position);
			if(distance < minDist){
				nearestEnemy = unit.GetComponent<AINavigation> ();
				minDist = distance;
			}
		}
		if(Vector3.Distance (nearestEnemy.transform.position, autoSelectedPlayerUnit.transform.position) < autoSelectedPlayerUnit.range){
			nearestEnemy.setEnemyAsSelected (true);
			autoSelectedPlayerUnit.targetSet (nearestEnemy.getId ());
		}
	}
	public int GetMinerals(){return minerals;}
	public void SubstractMinerals(int amount){
		minerals -= amount;
	}
	public void AddMinerals(int amount){
		minerals += amount;
	}
	public void PassButtonSelectedShip(int selectedId){
		selectedPlayerFactory.CreateShip (selectedId);
		currentSelectedFactoryId = -1;
	}
	public void PassButtonSelectedFactory(int selectedId){
		selectedPlayerUnit.GetComponent<SpawnFactory> ().CreateFactory (selectedId);
		currentSelectedUnitId = -1;
	}
	public void GameOver(){
		gameOverPanel.SetActive (true);
	}
}
