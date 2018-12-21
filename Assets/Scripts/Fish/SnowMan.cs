using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnowMan : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (snowman());
	}
	
	IEnumerator snowman(){
		while (true) {
			transform.DOShakePosition(2,new Vector3(0.2f,0.2f,0.2f),1,90,false,true);
			yield return new WaitForSeconds (2);
		}
	}
}
