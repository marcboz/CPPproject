using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControl : MonoBehaviour {

	private float currentHealth;
	float timer = 0;

	public float startingHealth;

	void Start () {
		currentHealth = startingHealth;
	}
	void Update () {
		if (currentHealth <= 0f) {
			timer += Time.deltaTime;
		}
		if (timer >= 0.8f) {
			Destroy (this.gameObject);
		}
	}
	public void damaged(float amount) {
		currentHealth -= amount;
	}
	public float GetHealth(){
		return currentHealth;
	}
}
