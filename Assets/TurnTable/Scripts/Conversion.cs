using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversion : MonoBehaviour {

	public static string UnitChange(int value){
		if (value >= 1000000000) {
			if (value / 1000000 > 1000) {
				return (value / 1000000).ToString ().Insert ((value / 1000000000).ToString ().Length, ",") + "M";
			} else {
				return value/1000000+"M";
			}
		}
		else if (value >= 1000000) {
			if (value / 1000 > 1000) {
				return (value / 1000).ToString ().Insert ((value / 1000000).ToString ().Length, ",") + "K";
			} else {
				return value/1000+"K";
			}
		} else {
			if (value > 1000) {
				return value.ToString ().Insert((value/1000).ToString().Length,",");
			} else {
				return value.ToString ();
			}
		}
	}

}
