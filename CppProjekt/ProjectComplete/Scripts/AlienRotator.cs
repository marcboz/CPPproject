using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienRotator : MonoBehaviour {

	public float rotationSpeed = 0;

	void Start () {
	}

	void Update () {
		transform.Rotate( new Vector3(rotationSpeed, 0f, 0f) * Time.deltaTime);
	}
}