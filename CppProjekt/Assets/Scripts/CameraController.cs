using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float cameraSpeed;
	private Camera camera;

	void Start () {
		camera = GetComponent<Camera> ();
	}

	void Update () {
		if(Input.GetKey(KeyCode.W)){
			transform.position = transform.position + new Vector3(Time.deltaTime * cameraSpeed,0f,0f);
		}
		if(Input.GetKey(KeyCode.S)){
			transform.position = transform.position + new Vector3(Time.deltaTime * -cameraSpeed,0f,0f);
		}
		if(Input.GetKey(KeyCode.A)){
			transform.position = transform.position + new Vector3(0f,0f,Time.deltaTime * -cameraSpeed);
		}
		if(Input.GetKey(KeyCode.D)){
			transform.position = transform.position + new Vector3(0f,0f,Time.deltaTime * cameraSpeed);
		}
	}
}
