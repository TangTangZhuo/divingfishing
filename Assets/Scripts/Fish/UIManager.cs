using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public GameObject startButton;
	public GameObject upgrading;
	public int diveDepth;
	public Text goldT;
	public long offlineGold;

	private static UIManager instance;
	public static UIManager Instance{
		get{return instance;}
	}

	public static string UnitChange(long value){
		if (value >= 1000000000000000) {
			if (value / 1000000000000 > 1000) {
				return (value / 1000000000000).ToString ().Insert ((value / 1000000000000000).ToString ().Length, ",") + "T";
			} else {
				return value/1000000000000+"T";
			}
		}
		else if (value >= 1000000000000) {
			if (value / 1000000000 > 1000) {
				return (value / 1000000000).ToString ().Insert ((value / 1000000000000).ToString ().Length, ",") + "B";
			} else {
				return value/1000000000+"B";
			}
		}
		else if (value >= 1000000000) {
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
		

	void Awake(){
		instance = this;	
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
