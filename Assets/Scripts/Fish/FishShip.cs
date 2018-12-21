using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishShip : MonoBehaviour {
	public Transform head;
	public Transform chin;

	public void MouthOC(){
		StartCoroutine (MouthOpenClose ());
	}

	public void BodyReady(){
		transform.DORotate (Vector3.zero, 0.5f);
	}

	public void BodyStart(){
		transform.DORotate (new Vector3 (0, 0, -30), 0.5f);
	}

	IEnumerator MouthOpenClose(){
		while (true) {
			head.DOLocalRotate (head.localEulerAngles+new Vector3(0,0,-10), 1, RotateMode.Fast);
			chin.DOLocalRotate (head.localEulerAngles+new Vector3(0,0,10), 1, RotateMode.Fast).OnComplete(()=>{
				head.DOLocalRotate (head.localEulerAngles+new Vector3(0,0,10), 1, RotateMode.Fast);
				chin.DOLocalRotate (head.localEulerAngles+new Vector3(0,0,-10), 1, RotateMode.Fast);
			});
			yield return new WaitForSeconds (2);
		}
	}
}
