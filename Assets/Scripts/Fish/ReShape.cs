using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReShape : MonoBehaviour {
	Cloth readyCloth;
	public GameObject runCloth;
	// Use this for initialization
	void Start () {
		readyCloth = GetComponent<Cloth> ();
	}

	public void ChangeShape(){
		readyCloth.SetEnabledFading (false);
		Invoke ("ShowReal", 0.5f);
	}

	void ShowReal(){
		runCloth.SetActive (true);
		GameObject.Destroy (gameObject);
	}
}
