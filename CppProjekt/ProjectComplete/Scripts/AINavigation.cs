using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavigation : MonoBehaviour {

	private GameController gameController;
	private AIController aiController;
	private Quaternion lookRotation;
	private Rigidbody rb;
	private NavMeshAgent navAgent;
	private AIFireControl fireControl;
	private UnitNavigation selectedUnit;
	private AsteroidBehavior selectedAsteroid;
	private GameObject [] units;
	private Vector3 currentDest;
	private HealthControl selectedUnitHC;
	private int thisId;	
	private bool targetIsSelected;
	private bool selected;

	public float rotationSpeed;
	public float range;
	public float moveDistance;
	public bool isBase;
	public bool isHarvester;
	public bool isMilitary;
	public bool isFactory;

	public static int Id = 0;

	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		aiController = GameObject.FindGameObjectWithTag ("AIController").GetComponent<AIController> ();
		navAgent = GetComponent<NavMeshAgent>();
		fireControl = GetComponentInChildren<AIFireControl> ();
		targetIsSelected = false;
		rb = GetComponent<Rigidbody>();
		thisId = Id++;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (targetIsSelected && selectedUnit != null && Vector3.Distance(transform.position, selectedUnit.transform.position)<range ) {
			fireControl.startFire (true, selectedUnitHC);
			if (Vector3.Distance (transform.position, selectedUnit.transform.position) < range / 2) {
				navAgent.isStopped = true;
			}
		}
		if (targetIsSelected && selectedAsteroid != null && Vector3.Distance (transform.position, selectedAsteroid.transform.position) < range) {
			fireControl.startHarvest (true, selectedAsteroid);
			if (Vector3.Distance (transform.position, selectedAsteroid.transform.position) < range / 2) {
				navAgent.isStopped = true;
			}
		}
		if (isBase && GetComponent<EnemyHealthControl> ().GetHealth () <= 0f) {
			gameController.GameOver ();
		}
	}
	void OnMouseOver(){
		if (Input.GetMouseButton (1)) {
			gameController.newEnemySelected (thisId);
			Debug.Log ("click received");
		}
	}
	public void setEnemyAsSelected(bool state) {
		selected = state;
	}
	public int getId(){return thisId;}
	public void targetSet(int targetId) {
		if (isHarvester) {
			units = GameObject.FindGameObjectsWithTag ("Asteroid");
			foreach (GameObject unit in units) {
				AsteroidBehavior temp_selectedUnit = unit.GetComponent<AsteroidBehavior> ();
				if (temp_selectedUnit.getId () == targetId) {
					selectedAsteroid = temp_selectedUnit;
					navAgent.isStopped = false;
					navAgent.SetDestination (unit.transform.position);
					currentDest = unit.transform.position;
					Vector3 lookDest = (currentDest - transform.position).normalized;
					lookRotation = Quaternion.LookRotation (lookDest);
					targetIsSelected = true;
					Debug.Log (selectedAsteroid);
				}
			}
		} else if(isMilitary) {
			units = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject unit in units) {
				UnitNavigation temp_selectedUnit = unit.GetComponent<UnitNavigation> ();
				HealthControl temp_selectedUnitHC = unit.GetComponent<HealthControl> ();
				if (temp_selectedUnit.getId () == targetId) {
					selectedUnitHC = temp_selectedUnitHC;
					selectedUnit = temp_selectedUnit;
					navAgent.SetDestination (unit.transform.position);
					currentDest = unit.transform.position;
					Vector3 lookDest = (currentDest - transform.position).normalized;
					lookRotation = Quaternion.LookRotation (lookDest);
					targetIsSelected = true;
					Debug.Log (selectedUnit);
				}
			}
		}
	}
}
