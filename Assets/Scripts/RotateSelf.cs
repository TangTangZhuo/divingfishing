using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RotateSelf : MonoBehaviour {

	public GameObject turnTable;
	public Text CountDown;

	// Use this for initialization
	void OnEnable () {
		StartCoroutine (Rotate ());
		Invoke ("GetCountDownText", 0.1f);

	}

	void GetCountDownText(){
		Timer.Instance.GetCountDownText(CountDown);
		turnTable.GetComponent<TurnTable> ().UpdateBoost ();
	}
	
	IEnumerator Rotate(){
		yield return new WaitForSeconds (2);
		while (true) {
			transform.DORotate (transform.eulerAngles + new Vector3 (0,0,1720), 2f, RotateMode.FastBeyond360);
			yield return new WaitForSeconds (4);
		}
	}

	public void OnTurnBtn(){
		turnTable.SetActive (true);
	}
}
