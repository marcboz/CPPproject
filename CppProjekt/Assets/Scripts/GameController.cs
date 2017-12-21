using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private GameObject[] units;
	private UnitNavigation selectedPlayerUnit;

	void Start () {
		
	}

	void Update () {
		
	}

	public void newObjectSelected(int id){
		units = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject unit in units) {
			UnitNavigation selectedUnit = unit.GetComponent<UnitNavigation> ();
			if (selectedUnit.getId () == id) {
				selectedUnit.setAsSelected (true);
				selectedPlayerUnit = selectedUnit;
			} else {
				selectedUnit.setAsSelected (false);
			}
		}
	}
	public void newEnemySelected(int id){
		units = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject unit in units) {
			EnemyHealthControl selectedUnit = unit.GetComponent<EnemyHealthControl> ();
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
}
