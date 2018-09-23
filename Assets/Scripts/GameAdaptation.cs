using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAdaptation : MonoBehaviour {
	
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
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
