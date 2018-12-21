using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptIpad : MonoBehaviour {


	void Start () {
		if (Mathf.Abs(((Screen.height/(Screen.width*1f))-1.33f))<0.1f) {
			if (transform.name == "bigBG_1") {
				transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
				transform.position -= new Vector3 (0, 1.5f, 0);
			}
			if (transform.name == "bgq_1") {
				transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
				transform.position -= new Vector3 (0, 2.8f, 0);
			}
			if (transform.name == "bigBG_2") {
				transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
				transform.position -= new Vector3 (0, 0.5f, 0);
			}
			if (transform.name == "bgq_2") {
				transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
				transform.position -= new Vector3 (0, 1.6f, 0);
			}
			if (transform.name == "bigBG_3") {
				transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
				transform.position -= new Vector3 (0, 0.4f, 0);
			}
			if (transform.name == "bgq_3") {
				transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
				transform.position -= new Vector3 (0, 3.2f, 0);
			}
			if (transform.name == "bigBG_4") {
				transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
				transform.position -= new Vector3 (0, 0.4f, 0);
			}
			if (transform.name == "bgq_4") {
				transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
				transform.position -= new Vector3 (0, 2.2f, 0);
			}
			if (transform.name == "shengdan1") {
				transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
				transform.position -= new Vector3 (1, -0.2f, 0);
			}
			if (transform.name == "shengdan2") {
				transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
				transform.position -= new Vector3 (-0.55f, -0.7f, 0);
			}
		}
	}

}
