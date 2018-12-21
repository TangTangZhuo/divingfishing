using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChristmasGuide : MonoBehaviour {
	public GameObject guideMask;
	public GameObject map;
	public GameObject map4;

	public GameObject vip;
	public GameObject noads;

	public Transform mapBtn;
	// Use this for initialization
	void Start () {
		StartGuide ();
	}
	
	void StartGuide(){
		int depth = PlayerPrefs.GetInt ("valueDepth", UIManager.Instance.diveDepth);
		int christmasGuid = PlayerPrefs.GetInt ("christmasGuid", 0);
		print ("christmasGuid"+christmasGuid);
		if (christmasGuid == 0) {
			if (depth <= -77) {
				if (!vip.activeSelf && !noads.activeSelf) {					
					guideMask.SetActive (true);
					map.SetActive (true);
					map.transform.position = mapBtn.position;
					map.GetComponent<Button> ().onClick.AddListener (() => {
						map.SetActive(false);
						map4.SetActive(true);
						map4.GetComponent<Button>().onClick.AddListener(()=>{
							map4.SetActive(false);
							guideMask.SetActive (false);
						});
					});
				}
			}
		}
	}
		
}
