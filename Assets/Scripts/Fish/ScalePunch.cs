using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScalePunch : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		StartCoroutine (PunchSelf ());
	}
	
	IEnumerator PunchSelf(){
		while (true) {
			transform.DOPunchScale (Vector3.one * 0.2f, 0.3f, 10, 1);
			yield return new WaitForSeconds (2f);
		}
	}
}
