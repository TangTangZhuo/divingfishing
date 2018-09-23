using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shake : MonoBehaviour {
	float time;
	Vector3 curPos;

	bool stopShake;
	// Use this for initialization
	void Start () {
		time = 7;
		stopShake = false;
		curPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (ProgressManager.Instance.isReady) {
			time += Time.deltaTime;


			if (time > 7f) {
				transform.DOShakePosition(2,new Vector3(0.2f,0.2f,0.2f),1,90,false,false).SetLoops(2).timeScale = 0.3f;
				time = 0;
			}

		}
		if (ProgressManager.Instance.isRunning) {
			if (!stopShake) {
				transform.DOKill (false);
				stopShake = true;
			}
		}
	}	
}
