using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
	public AudioSource audios;
	public GameObject epicAudio;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			GameObject go = Instantiate (epicAudio);
			Destroy (go, 1f);
		}
		

	}
}
