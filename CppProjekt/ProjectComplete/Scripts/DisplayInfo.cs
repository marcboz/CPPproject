using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInfo : MonoBehaviour {

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		GameObject.FindGameObjectWithTag("MineralInfo").GetComponent<Text> ().text = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().GetMinerals ().ToString();
	}
}
