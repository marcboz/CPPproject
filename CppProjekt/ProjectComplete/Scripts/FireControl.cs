using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour {

	private UnitNavigation unitNavigation;
	private bool fire;
	private EnemyHealthControl target;
	private AsteroidBehavior asteroidTarget;
	private float timer;
	private LineRenderer[] lasers;
	private float laserLifetime;
	private float laserTimer;
	private int index = 0;

	public float damage;
	public int harvestAmount;
	public float fireCooldown = 1f;
	public Transform[] laserPositions;

	void Start () {
		lasers = GetComponentsInChildren<LineRenderer> ();
		unitNavigation = GetComponentInParent<UnitNavigation> ();
		laserLifetime = 0.5f;
		foreach (LineRenderer laser in lasers) {
			laser.enabled = false;
		}
		fire = false;
	}

	void Update () {
		timer += Time.deltaTime;
		laserTimer += Time.deltaTime;

		if (asteroidTarget != null) {
			foreach (LineRenderer laser in lasers) {
				laser.SetPosition (0, laserPositions[index].transform.position);
				laser.SetPosition (1, asteroidTarget.transform.position);
				index++;
			}
			index = 0;
			if (fire && timer >= fireCooldown) {
				Shoot ();
			}

			if (laserTimer <= laserLifetime && fire) {
				foreach (LineRenderer laser in lasers) {
					laser.enabled = true;
				}
			} else {
				foreach (LineRenderer laser in lasers) {
					laser.enabled = false;
				}
			}
		}
		if (target != null) {
			foreach (LineRenderer laser in lasers) {
				laser.SetPosition (0, laserPositions[index].transform.position);
				laser.SetPosition (1, target.transform.position);
				index++;
			}
			index = 0;
			if (fire && timer >= fireCooldown) {
				Shoot ();
			}
			if (laserTimer <= laserLifetime && fire) {
				foreach (LineRenderer laser in lasers) {
					laser.enabled = true;
				}
			} else {
				foreach (LineRenderer laser in lasers) {
					laser.enabled = false;
				}
			}
		}
	
	}
	public void startFire(bool state, EnemyHealthControl selectedTarget){
		fire = state;
		target = selectedTarget;
	}
	public void startHarvest(bool state, AsteroidBehavior selectedTarget){
		fire = state;
		asteroidTarget = selectedTarget;
	}
	void Shoot() {
		timer = 0f;
		laserTimer = 0f;

		if (unitNavigation.isHarvester) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().AddMinerals (harvestAmount);
		} else {
			target.damaged (damage * lasers.Length);
		}
	}
}
