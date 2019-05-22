using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimerCreater : MonoBehaviour {

	public GameObject CloneTemp;
	public static bool IsHaveUsed = false;
	private GameObject clone;  

	public GameObject TTT;
	private GameObject ttt;

	// Use this for initialization
	void Awake () {
		if(!IsHaveUsed)
		{
			clone = Instantiate(CloneTemp, transform.position, transform.rotation) as GameObject;
			IsHaveUsed = true;
			DontDestroyOnLoad(clone.transform.gameObject);

			ttt = Instantiate(TTT, transform.position, transform.rotation) as GameObject;
			DontDestroyOnLoad(ttt.transform.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
