using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class HelpButton : MonoBehaviour {

	private Button button;
	private GameObject help;
	private int helpSwitch;

	void Start () {
		button = GetComponent<Button> ();
		help = GameObject.FindGameObjectWithTag ("Help");
		button.onClick.AddListener (TaskOnClick);
		help.SetActive (false);
		helpSwitch = -1;
	}

	void Update() {
		if (helpSwitch == -1) {
			help.SetActive (false);
		} else {
			help.SetActive (true);
		}
	}
	void TaskOnClick () {
		helpSwitch *= -1;
	}

}
