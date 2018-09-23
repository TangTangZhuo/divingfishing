using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestPerformance : MonoBehaviour {
	private Text maxFish;
	private Text bestFish;
	// Use this for initialization

	void Start () {
		maxFish = transform.Find ("MaxFish").GetComponent<Text> ();
		bestFish = transform.Find ("BestFish").GetComponent<Text> ();

		GetBestPerformance ();
	}
	
	void GetBestPerformance(){
		if (PlayerPrefs.GetInt ("Level", 1) == 1) {
			maxFish.text = PlayerPrefs.GetInt ("maxFish-1", 0).ToString();
			bestFish.text = PlayerPrefs.GetInt ("bestFish-1", 0).ToString ();
		}else if (PlayerPrefs.GetInt ("Level", 1) == 2) {
			maxFish.text = PlayerPrefs.GetInt ("maxFish-2", 0).ToString();
			bestFish.text = PlayerPrefs.GetInt ("bestFish-2", 0).ToString ();
		}else if (PlayerPrefs.GetInt ("Level", 1) == 3) {
			maxFish.text = PlayerPrefs.GetInt ("maxFish-3", 0).ToString();
			bestFish.text = PlayerPrefs.GetInt ("bestFish-3", 0).ToString ();
		}
	}


}
