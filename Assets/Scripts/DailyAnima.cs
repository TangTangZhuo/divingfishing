using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DailyAnima : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		StartCoroutine (ShakeDaily ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ShakeDaily(){
		while (true) {
			float time = 5f;
			transform.DOShakeRotation (time, new Vector3(0,0,80), 20,15,true);
			yield return new WaitForSeconds (time+1);
		}
	}
}
