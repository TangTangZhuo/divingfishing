using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTManager : MonoBehaviour {

	string appid = "161170";
	string channel = "AppStore";
	string appname = "DivingFishing";

	void Awake () {
		Debug.Log ("unity_TTInit");
		TTT.TTInit (appid, channel, appname);
	}

}
