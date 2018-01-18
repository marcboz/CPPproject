using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitNavigation : MonoBehaviour {

	private Camera camera;
	private Transform target;
	private Ray cameraRay;
	private RaycastHit floorHit;
	private LayerMask floorMask;
	private Quaternion lookRotation;
	private Rigidbody rb;
	private NavMeshAgent navAgent;
	private GameController gameController;
	private FireControl fireControl;
	private AINavigation selectedUnit;
	private AsteroidBehavior selectedAsteroid;
	private GameObject [] units;
	private Vector3 currentDest;
	private EnemyHealthControl selectedUnitHC;
	private float cameraRayLenght;
	private int thisId;
	private bool selected;
	private bool targetIsSelected;
	private bool selectedByAi;

	public float rotationSpeed;
	public float range;
	public float moveDistance;
	public bool isBase;
	public bool isHarvester;

	public static int Id = 0;

	void Start () {
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		navAgent = GetComponent<NavMeshAgent>();
		floorMask = LayerMask.GetMask ("Floor");
		cameraRayLenght = 100f;
		selected = false;
		thisId = Id++;
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		fireControl = GetComponentInChildren<FireControl> ();
		targetIsSelected = false;
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		if (Input.GetMouseButtonDown (1) && selected) {
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				navAgent.isStopped = false;
				navAgent.SetDestination (hit.point);
				currentDest = hit.point;
				targetIsSelected = false;
				Vector3 lookDest = (currentDest - transform.position).normalized;
				lookRotation = Quaternion.LookRotation (lookDest);
			}
		}
		if (transform.rotation != lookRotation) {
			transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation * Quaternion.Euler (0, 0, 0), Time.deltaTime * rotationSpeed);
		} else {
			transform.rotation = transform.rotation;
		}

		if (targetIsSelected && selectedUnitHC != null && Vector3.Distance(transform.position, selectedUnit.transform.position)<range ) {
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
		if (selectedUnit == null && selectedUnitHC == null) {
			targetIsSelected = false;
		}
		if (isBase && GetComponent<HealthControl> ().GetHealth () <= 0f) {
			gameController.GameOver ();
		}
	}
	void OnMouseDown(){
		if (Input.GetMouseButton (0)) {
			gameController.newObjectSelected (thisId);
		}
	}
	public int getId(){return thisId;}
	public void setAsSelected(bool state){
		selected = state;
	}
	public void setEnemyAsSelected(bool state) {
		selectedByAi = state;
	}
	public void targetSet(int targetId) {
		if (isHarvester) {
			units = GameObject.FindGameObjectsWithTag ("Asteroid");
			foreach (GameObject unit in units) {
				AsteroidBehavior temp_selectedUnit = unit.GetComponent<AsteroidBehavior> ();
				if (temp_selectedUnit.getId () == targetId) {
					selectedAsteroid = temp_selectedUnit;
					navAgent.SetDestination (unit.transform.position);
					currentDest = unit.transform.position;
					Vector3 lookDest = (currentDest - transform.position).normalized;
					lookRotation = Quaternion.LookRotation (lookDest);
					targetIsSelected = true;
					Debug.Log (selectedAsteroid);
				}
			}
		} else {
			units = GameObject.FindGameObjectsWithTag ("Enemy");
			foreach (GameObject unit in units) {
				AINavigation temp_selectedUnit = unit.GetComponent<AINavigation> ();
				EnemyHealthControl temp_selectedUnitHC = unit.GetComponent<EnemyHealthControl> ();
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
	public void MoveForward() {
		navAgent.SetDestination (new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance));
	}
}
