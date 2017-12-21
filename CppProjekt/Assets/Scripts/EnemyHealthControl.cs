using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControl : MonoBehaviour {

	private GameController gameController;																			//will be moved to ai navigation srcipt once its made
	private int thisId;																								//will be moved to ai navigation srcipt once its made
	private float currentHealth;
	private bool selected;
	private ParticleSystem particles;
	float timer = 0;

	public float startingHealth;


	public static int Id = 0;																						//will be moved to ai navigation srcipt once its made

	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();		//will be moved to ai navigation srcipt once its made
		thisId = Id++;																								//will be moved to ai navigation srcipt once its made
		currentHealth = startingHealth;
		particles = GetComponent<ParticleSystem> ();
	}

	void Update () {
		if (currentHealth <= 0f) {
			particles.Play ();
			timer += Time.deltaTime;
		}
		if (timer >= 0.8f) {
			Destroy (this.gameObject);
		}
	}
	void OnMouseOver(){																				//will be moved to ai navigation srcipt once its made
		if (Input.GetMouseButton (1)) {
			gameController.newEnemySelected (thisId);
			Debug.Log ("click received");
		}
	}
	public void damaged(float amount) {
		currentHealth -= amount;
	}
	public void setEnemyAsSelected(bool state) {
		selected = state;
	}
	public int getId(){return thisId;}
}
