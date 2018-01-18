using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusRotator : MonoBehaviour {

	public float rotationSpeed = 0;

	void Start () {
	}

	void Update () {
		transform.Rotate( new Vector3(0f, 0f, rotationSpeed) * Time.deltaTime);
	}
}
