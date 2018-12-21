using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeuI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Shake ());
	}
	
	IEnumerator Shake(){
		while (true) {
			transform.DOPunchRotation (new Vector3 (0, 0, 5), 1, 5, 1);
			yield return new WaitForSeconds (1);
		}
	}
}
