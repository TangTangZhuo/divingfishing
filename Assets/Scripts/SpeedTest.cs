using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedTest : MonoBehaviour {
	Slider slider;
	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
	}
	


	public void ChangeMaxSpeed(){
		SubmarineController.Instance.maxSpeed = slider.value;
		SubmarineController.Instance.moveSpeed = slider.value;
	}
}
