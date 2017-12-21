using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour {

	private UnitNavigation unitNavigation;
	private bool fire;
	private EnemyHealthControl target;
	private float timer;
	private ParticleSystem particles;

	public float fireCooldown = 1f;

	void Start () {
		particles = GetComponentInChildren<ParticleSystem> ();
		unitNavigation = GetComponentInParent<UnitNavigation> ();
		fire = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (target != null && fire && timer >= fireCooldown) {
			Shoot ();
		}
	}
	public void startFire(bool state, EnemyHealthControl selectedTarget){
		fire = state;
		target = selectedTarget;
	}
	void Shoot() {
		timer = 0f;

		target.damaged (20);
		particles.Play ();

	}
}
