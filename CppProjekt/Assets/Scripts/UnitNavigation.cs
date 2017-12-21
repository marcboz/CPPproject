using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitNavigation : MonoBehaviour {

	private Camera camera;
	private Transform target;
	private Ray cameraRay;
	private RaycastHit floorHit;
	private float cameraRayLenght;
	private LayerMask floorMask;
	private Quaternion lookRotation;
	private int thisId;
	private GameController gameController;
	private FireControl fireControl;
	private GameObject [] units;
	private EnemyHealthControl selectedUnit;
	private bool targetIsSelected;

	public bool selected;
	public Vector3 currentDest;
	public NavMeshAgent navAgent;
	public float rotationSpeed;
	public float range;

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
	}

	void FixedUpdate () {
		if (Input.GetMouseButtonDown (1) && selected) {
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				navAgent.SetDestination (hit.point);
				currentDest = hit.point;
				targetIsSelected = false;
			}
		}
		Vector3 lookDest = (currentDest - transform.position).normalized;
		lookRotation = Quaternion.LookRotation (lookDest);
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation * Quaternion.Euler(0,0,0), Time.deltaTime * rotationSpeed);

		if (targetIsSelected && selectedUnit != null && Vector3.Distance(transform.position, selectedUnit.transform.position)<range ) {
			fireControl.startFire (true, selectedUnit);
			if (Vector3.Distance (transform.position, selectedUnit.transform.position) < range / 2) {
				navAgent.isStopped = true;
			}
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
	public void targetSet(int targetId) {
		units = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject unit in units) {
			EnemyHealthControl temp_selectedUnit = unit.GetComponent<EnemyHealthControl> ();
			if (temp_selectedUnit.getId () == targetId) {
				selectedUnit = temp_selectedUnit;
				navAgent.SetDestination (unit.transform.position);
				currentDest = unit.transform.position;
				targetIsSelected = true;
				Debug.Log (selectedUnit);
			}
		}
	}
}
