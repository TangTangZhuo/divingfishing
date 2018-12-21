using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickAudio : MonoBehaviour {
	Button button;
	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		button.onClick.AddListener (OnClick);
	}

	public void OnClick(){
		if (PlayerPrefs.GetInt ("Audio", 1) == 1) {
			GameObject go = Instantiate (AudioManager.Instance.clickAudio);
			Destroy (go, 1f);
		}
	}
}
