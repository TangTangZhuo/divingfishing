using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAdaptation : MonoBehaviour {
	public Transform[] uis;
	float devWidth = 10.8f;
	 
	// Use this for initialization
	void Start () {		 
		float orthographicSize = this.GetComponent<Camera> ().orthographicSize * 1.92f;
		float aspectRatio = Screen.width * 1.0f / Screen.height; 
		float cameraWidth = orthographicSize * 2 * aspectRatio;
		if (cameraWidth < devWidth) {
			orthographicSize = devWidth / (2 * aspectRatio);
			this.GetComponent<Camera> ().orthographicSize = orthographicSize / 1.92f;
		}	

		if (Mathf.Abs(((Screen.currentResolution.height/(Screen.currentResolution.width*1f))-2.16f))<0.1f) {
			for (int i = 0; i < uis.Length; i++) {
				uis[i].localPosition -= new Vector3 (0, Screen.currentResolution.height*0.06f, 0);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
