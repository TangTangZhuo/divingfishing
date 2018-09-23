using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	public static Rotate Instance{
		get{ return _instance;}
	}

	private static Rotate _instance;

	// Use this for initialization
	void Awake () {
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator RotateTrans(Transform trans,Vector3 v3){
		for (float time = 0; time < 2; time += Time.deltaTime) {
			trans.rotation = Quaternion.Euler (new Vector3 (0, 0, 360 * time));
			print (trans.rotation);
			if (Mathf.Abs (time - 2) < 0.0001f) {
				continue;
			}
		}
		yield return 0;
	}

}
