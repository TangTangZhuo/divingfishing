using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReBirthGuide : MonoBehaviour {

	public void OnBackBtn(){
		gameObject.SetActive (false);
		PlayerPrefs.SetInt ("ReBirthGuide", 1);
	}
}
