using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	private Button button;

	void Start () {
		button = GetComponent<Button> ();
		button.onClick.AddListener (TaskOnClick);
	}

	void TaskOnClick () {
		SceneManager.LoadScene("SideSelection", LoadSceneMode.Single);
	}

}
