using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UpdateState ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTapticClick(){
		int taptic = PlayerPrefs.GetInt ("Taptic", 1);
		if (taptic == 0) {
			PlayerPrefs.SetInt ("Taptic", 1);
		}
		if (taptic == 1) {
			PlayerPrefs.SetInt ("Taptic", 0);
		}
		UpdateState ();

	}

	void UpdateState(){
		int taptic = PlayerPrefs.GetInt ("Taptic", 1);
		if (taptic == 0) {
			transform.GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
		}if (taptic == 1) {
			transform.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		}
	}
		
}
